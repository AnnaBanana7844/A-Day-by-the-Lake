using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static WeaponController instance;

    [Header("Weapon Data")]
    public gunStats currentGunStats;
    public Transform weaponHandAttachPoint; // Your "AttachPoint" game object inside the rigged right hand

    [Header("ADS Reference Positions")]
    public Transform hipPos;
    public Transform adsPos;
    public float adsSpeed = 10f;

    [Header("Animations")]
    public Animator playerAnimator;

    [Header("Bear Freeze Mechanic")]
    [SerializeField] private int hitsRequiredToLock = 8;
    private int successfulHitCount = 0;

    private GameObject spawnedGunModel;
    private int currentAmmo;
    private float nextTimeToShoot = 0f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitializeGun();
    }

    void Update()
    {
        HandleADS();
        HandleShooting();
        HandleMeleeInput();
    }

    public void InitializeGun()
    {
        // Clear out any old gun model before spawning a new one
        if (spawnedGunModel != null) Destroy(spawnedGunModel);

        if (currentGunStats != null && currentGunStats.gunModel != null)
        {
            // Spawn the gun from your gunStats asset and lock it to the hand attach point
            spawnedGunModel = Instantiate(currentGunStats.gunModel, weaponHandAttachPoint);
            spawnedGunModel.transform.localPosition = Vector3.zero;
            spawnedGunModel.transform.localRotation = Quaternion.identity;

            currentAmmo = currentGunStats.ammoMax;
        }
    }

    void HandleADS()
    {
        if (spawnedGunModel == null) return;

        // Choose target based on Right-Click hold
        Transform targetPos = Input.GetMouseButton(1) ? adsPos : hipPos;

        // Smoothly move the entire hand attach point toward the reference positions
        weaponHandAttachPoint.position = Vector3.Lerp(weaponHandAttachPoint.position, targetPos.position, Time.deltaTime * adsSpeed);
        weaponHandAttachPoint.rotation = Quaternion.Lerp(weaponHandAttachPoint.rotation, targetPos.rotation, Time.deltaTime * adsSpeed);
    }

    void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToShoot && currentAmmo > 0)
        {
            nextTimeToShoot = Time.time + currentGunStats.shootRate;
            ShootWeapon();
        }
    }

    void ShootWeapon()
    {
        if (currentGunStats == null) return;
        currentAmmo--;

        // Raycast forward from the center of the screen
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        // Use modern C# inline declaration "out RaycastHit hit" to completely avoid variable passing errors
        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out RaycastHit hit, currentGunStats.shootDistance))
        {
            ProcessPotentialBearHit(hit.transform);

            if (currentGunStats.hitEffect != null)
            {
                Instantiate(currentGunStats.hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }

        // Play weapon audio
        if (currentGunStats.shootSound.Length > 0)
        {
            int randomIndex = Random.Range(0, currentGunStats.shootSound.Length);
            AudioSource.PlayClipAtPoint(currentGunStats.shootSound[randomIndex], transform.position, currentGunStats.shootSoundVol);
        }
    }

    void HandleMeleeInput()
    {
        if (playerAnimator == null) return;

        if (Input.GetKeyDown(KeyCode.Q)) // Quick Punch
        {
            playerAnimator.SetTrigger("Punch");
        }
        if (Input.GetKeyDown(KeyCode.V)) // && if the weapon is in hand // Gun Butt strike melee
        {
            playerAnimator.SetTrigger("GunMelee");
        }
    }

    // MANDATORY STEP: Call this exact function name via a Unity Animation Event when the punch/melee strike lands!
    public void ExecuteMeleeDamageCheck()
    {
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        float meleeRange = 2.5f;

        // Inline declaration prevents "cannot be passed" errors
        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out RaycastHit hit, meleeRange))
        {
            Debug.Log("Melee impact on: " + hit.transform.name);
            ProcessPotentialBearHit(hit.transform);
        }
    }

    private void ProcessPotentialBearHit(Transform hitObject)
    {
        // Look for the Bear script component on whatever object was struck
        if (hitObject.TryGetComponent<Bear>(out Bear bearScript))
        {
            successfulHitCount++;
            Debug.Log($"Hit Bear! Total hits: {successfulHitCount} / {hitsRequiredToLock}");

            if (successfulHitCount >= hitsRequiredToLock)
            {
                LockBearInPlace(bearScript, hitObject);
                successfulHitCount = 0; // Reset counter
            }
        }
    }

    private void LockBearInPlace(Bear bear, Transform bearTransform)
    {
        Debug.Log("8 Hits reached! Applying bear trap lock mechanics.");

        // 1. Disable the Bear AI script so Update() loops stop running
        bear.enabled = false;

        // 2. Safely stop and disable its movement NavMeshAgent
        if (bearTransform.TryGetComponent<UnityEngine.AI.NavMeshAgent>(out UnityEngine.AI.NavMeshAgent agent))
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        // 3. Force the bear's Animator to lock into an Idle state
        if (bearTransform.TryGetComponent<Animator>(out Animator bearAnim))
        {
            bearAnim.Play("Idle");
        }

        // 4. Snap the bear's physical position to where your weapon hand/player is
        bearTransform.position = weaponHandAttachPoint.position;

        // 5. Parent the bear to your player to lock it in place completely
        bearTransform.SetParent(transform);
    }
}
