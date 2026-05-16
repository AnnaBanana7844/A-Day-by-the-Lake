using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject dmgDealer;
    public int damageAmount;


    public PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        playerController.takeDamage(damageAmount);
    }
}
