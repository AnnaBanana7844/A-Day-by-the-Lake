using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static bool uiOpen = false;

    [SerializeField] CharacterController controller;
    [SerializeField] LayerMask ignoreLayer;

    [Range(1, 10)][SerializeField] int HP;
    [Range(3, 7)][SerializeField] int speed;
    [Range(2, 5)][SerializeField] int sprintMod;
    [Range(5, 25)][SerializeField] int jumpSpeed;
    [Range(1, 3)][SerializeField] int jumpMax;
    [Range(15, 50)][SerializeField] int gravity;

    int jumpCount;
    int HPOrig;
    
    Vector3 moveDir;
    Vector3 playerVel;

    public PlayerDeath deathUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HPOrig = HP;
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameManager.instance.isPaused)

            if (GetComponent<PlayerFishingController>().isFishing)
        {
            return;
        }
        if(uiOpen)
        {
            return;
        }

        movement();
    }

   

    void movement()
    {
        // Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * gunList[gunListPos].shootDist, Color.yellow);

        if (controller.isGrounded)
        {
            jumpCount = 0;
            playerVel.y = 0;
        }

        moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        controller.Move(moveDir * speed * Time.deltaTime);

        jump();
        controller.Move(playerVel * Time.deltaTime);
        playerVel.y -= gravity * Time.deltaTime;

        
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            playerVel.y = jumpSpeed;
            jumpCount++;
        }
    }

    public void takeDamage(int amount)
    {
        HP -= amount;
        if(HP <= 0)
        {
            deathUI.playRandomDeathSound();
            GameManager.instance.youLose();
        }
    }
}
