using UnityEngine;

public class BearTrapController : MonoBehaviour
{

    private Animator animator;
    private bool isSet = true;

    [Header("Setup")]
    [SerializeField] private Transform trapCenterPivot;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trap is active and the object has the Bear script
        if (isSet && other.TryGetComponent<Bear>(out Bear bearScript))
        {
            animator.SetTrigger("IsTriggered");
            isSet = false;

            LockBearInPlace(bearScript);
        }
    }

    void LockBearInPlace(Bear bear)
    {
        // 1. Disable the Bear AI script so Update() stops running entirely
        bear.enabled = false;

        // 2. Safely stop and disable the NavMeshAgent
        if (bear.TryGetComponent<UnityEngine.AI.NavMeshAgent>(out UnityEngine.AI.NavMeshAgent agent))
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        // 3. Force the bear's Animator to stop running and stay Idle
        if (bear.TryGetComponent<Animator>(out Animator bearAnim))
        {
            bearAnim.Play("Idle");
        }

        // 4. Snap the bear's position to the center of the trap
        bear.transform.position = trapCenterPivot.position;

        // 5. Parent the bear to the trap to anchor it completely
        bear.transform.SetParent(transform);
    }
}
