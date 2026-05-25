using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private TimedSpikes parentTrap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentTrap = GetComponentInParent<TimedSpikes>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (parentTrap != null)
        {
            parentTrap.ChildTriggeredEntered(other);
        }
    }

}
