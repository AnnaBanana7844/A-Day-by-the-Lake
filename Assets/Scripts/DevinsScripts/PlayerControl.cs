using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour
{
    [Header("-----Model-----")]
    [SerializeField] GameObject playerModel;
    [SerializeField] CharacterController chrController;
    [SerializeField] GameObject hpBar;
    [SerializeField] GameObject fishingPole;

    [Header("-----Stats-----")]
    [SerializeField] int hp;//player's base hp
    int currentHP;//how much hp the player currently has
    [SerializeField] int walkSpeed;//how fast the player moves
    [SerializeField] int sprint;//how much faster the player gets upon sprinting
    [SerializeField] int jumpV;//velocity of the player when jumping. 
    [SerializeField] int jumpMax;//how many times is the player allowed to jump before touching the ground
    int jumpCount;//how many times the player has jumped in succession. resets when they hit they ground
    [SerializeField] int gravity;//how fast you come back down when you jump

    //controls whether the player is in the air, or gravity is pulling them down
    Vector3 moveD;//the direction of player's movement.
    Vector3 playerV;//something about gravity???

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = hp;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sprint();
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

        Jump();
        chrController.Move(playerV * Time.deltaTime);
        if (!chrController.isGrounded)
            playerV.y -= gravity * Time.deltaTime;
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            walkSpeed *= sprint;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            walkSpeed /= sprint; // to do hold to sprint
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            playerV.y = jumpV;
            jumpCount++;
        }
    }
    void GetPole()
    {
        if (Input.GetButtonDown("Enter"))
        {
            fishingPole.gameObject.SetActive(false);
        }
    }
}
