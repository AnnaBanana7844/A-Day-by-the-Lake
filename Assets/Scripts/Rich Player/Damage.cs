using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject dmgDealer;
    public int damageAmount;


    public Player playerController;

    private void OnTriggerEnter(Collider other)
    {
        playerController.AlterHealth(damageAmount);
    }
}
