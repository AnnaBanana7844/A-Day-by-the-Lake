using UnityEngine;
using System.Collections;

public class TimedSpikes : MonoBehaviour, PlayerControl
{

    [Header("Movement Settings")]
    public float lowerHeight = -1f;
    public float upperHeight = 1.5f; 
    public float TransitionSpeed = 5f;

    [Header("Timing Settings")]
    public float downTime = 3f;
    public float upTime = 2f;

    [Header("Damage Settings")]
    public int DamageAmount = 10;

    private Transform[] capsules;
    private bool isUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gather all child capsules
        capsules = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            capsules[i] = transform.GetChild(i);

            // Optional: Ensure capsules have a Trigger Collider to detect the player
            Collider col = capsules[i].GetComponent<Collider>();
            if (col != null) col.isTrigger = true;
        }

        // Start the infinite timing loop
        StartCoroutine(TrapRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        float targetY = isUp ? upperHeight : lowerHeight;

        foreach (Transform capsule in capsules)
        {
            Vector3 targetPos = new Vector3(capsule.localPosition.x, targetY, capsule.localPosition.z);
            capsule.localPosition = Vector3.Lerp(capsule.localPosition, targetPos, Time.deltaTime * TransitionSpeed);
        }
    }

    IEnumerator TrapRoutine()
    {
        while(true)
        {
            isUp = false;
            yield return new WaitForSeconds(downTime);

            isUp = true;
            yield return new WaitForSeconds(upTime);

        }
    }

    public void ChildTriggeredEntered(Collider other)
    {
        if (isUp) return;

        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.AlterHealth(-DamageAmount);
        }
    }

}
