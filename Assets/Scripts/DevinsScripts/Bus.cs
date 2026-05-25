using UnityEngine;

public class Bus : MonoBehaviour
{
    [SerializeField] SphereCollider activeField;
    [SerializeField] int partThresh;//"part threshold" how many bus parts player needs for win condition

    private void OnTriggerEnter(Collider other)
    {
        if(EnvironmentCtrl.Game.player.partCount >= partThresh) 
        { 
          Debug.Log("you win!!");
          GameManager.instance.statePause(); 
        }
    }
}
