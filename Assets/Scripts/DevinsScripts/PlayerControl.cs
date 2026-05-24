using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour
{
    [Header("-----Model-----")]
    [SerializeField] GameObject playerModel;
    [SerializeField] CharacterController chrController;
    [SerializeField] GameObject hpBar;
    [SerializeField] GameObject fishingPole;
    [SerializeField] Animator animator;
    string currAnim;

    [Header("-----Stats-----")]

    int fishCount;//how many fish the player currently has

    [SerializeField] float hp;//player's base hp
    float currentHP;//how much hp the player currently has
    [SerializeField] int walkSpeed;//how fast the player moves
    [SerializeField] int sprint;//how much faster the player gets upon sprinting
    [SerializeField] int jumpV;//velocity of the player when jumping. 
    int jumpCount;//how many times the player has jumped in succession. resets when they hit they ground
    [SerializeField] int jumpMax;//how many times is the player allowed to jump before touching the ground
    [SerializeField] int gravity;//how fast you come back down when you jump

    bool sprinting;
    bool walking;
    bool idle;


    //controls whether the player is in the air, or gravity is pulling them down
    Vector3 moveD;//the direction of player's movement.
    Vector3 playerV;//something about gravity???
    bool hasPole;//if this is false, player cannot activate fishing minigame, and instead is prompted to pull out their pole
    public static bool uiOpen;
    public PlayerDeath DeathUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = hp;
        fishingPole.SetActive(false);
        hasPole = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryUI.instance.toggleInventory();
            return;
        }

        if (GetComponent<PlayerFishingController>().isFishing)
            return;

        if (uiOpen)
            return;

        Movement();
        Sprint();
        CheckAnim();
    }
    void CheckAnim()
    {
        if (walking == true && sprinting == false)
        {
            if (currAnim != "Walk") { animator.Play("Walk"); currAnim = "Walk"; }
        }
        if (sprinting == true)
        {
            if(currAnim != "Run"){ animator.Play("Run"); currAnim = "Run"; Debug.Log("sprinting"); }
        }
        else if(idle == true || sprinting == false && walking == false)
        {
            if (currAnim != "Idle") { animator.Play("Idle"); currAnim = "Idle"; Debug.Log("idle"); }
        }

    }
    void Movement()
    {
        
        if (chrController.isGrounded)
        {
            jumpCount = 0;
            if (playerV.y < 0) // Not needed but I think might make it nicer
                playerV.y = 0;
        }

        moveD = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        chrController.Move(moveD.normalized * walkSpeed * Time.deltaTime);
        
        if(moveD.y != 0 || moveD.x != 0) { idle = false; walking = true; } else { idle = true; walking = false; };

            Jump();
        GetPole();
        chrController.Move(playerV * Time.deltaTime);
        if (!chrController.isGrounded)
            playerV.y -= gravity * Time.deltaTime;
    }

    void Sprint()
    {
        
        if (Input.GetButtonDown("Sprint"))
        {
            sprinting = true;
            walkSpeed *= sprint;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            sprinting = false;
            walkSpeed /= sprint; // to do hold to sprint
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            //jumping = true;
            playerV.y = jumpV;
            jumpCount++;
        }
    }
    void GetPole()
    {
        if (Input.GetButtonDown("PoleToggle"))
        {
            fishingPole.gameObject.SetActive(!fishingPole.activeSelf);
            hasPole = !hasPole;
        }
    }

    public void AlterHealth(float value)//alters the health of the player
    {
        hp += value;
        Debug.Log("Health Altered. Current HP: " + hp);
        if (hp <= 0)
        {
            Time.timeScale = 0;
            DeathUI.playRandomDeathSound();
            GameManager.instance.youLose();
        }
    }
    public void AlterFish(int value)//alters the health of the player
    {
        fishCount += value;
    }
}
