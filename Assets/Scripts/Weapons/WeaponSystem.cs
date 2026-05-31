using UnityEngine;

        public class WeaponSystem : MonoBehaviour
{
    public Transform gunTransform;
    public Transform hipPos, adsPos;
    public float adsSpeed = 10f;
    public Animator GunAnamation;

    [Header("Melee Settings")]
    public float meleeRange;
    public int meleeDamage = 15;
    public LayerMask hitLayer;
    public GameObject meleeHitEffect;

    // Update is called once per frame
    void Update()
    {
        Transform targetPos = Input.GetMouseButton(1) ? adsPos : hipPos;
        gunTransform.position = Vector3.Lerp(gunTransform.position, targetPos.position, Time.deltaTime * adsSpeed);

        if (Input.GetKeyDown(KeyCode.V))
        {
            GunAnamation.SetTrigger("GunMelee");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GunAnamation.SetTrigger("Punch");
        }
    }

    public void DealMeleeDamage()
    {
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out RaycastHit hit, meleeRange, hitLayer))
        {
            Debug.Log("meleeRange Hit:" + hit.transform.name);
        }
    }
}
