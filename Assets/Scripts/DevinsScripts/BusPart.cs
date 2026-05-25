using UnityEngine;

public class BusPart : MonoBehaviour
{
    [SerializeField] GameObject model;
    [SerializeField] SphereCollider aoe;//"area of effect"  when youre in here you get the part

    private void OnTriggerEnter(Collider other)
    {
        EnvironmentCtrl.Game.player.AlterParts(1);
        Destroy(model);
        Debug.Log(EnvironmentCtrl.Game.player.partCount);
    }
}
