using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Bear : MonoBehaviour
{
    [Header("-----References-----")]
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent meshAgent;
    [SerializeField] Transform playerTrans;

    [Header("-----Layers-----")]
    [SerializeField] LayerMask terrainLayer;
    [SerializeField] LayerMask playerLayerMask;

    [Header("-----Roam-----")]
    [SerializeField] float roamDist = 2;//"roam distance" how far the bear can roam at once
    [SerializeField] float roamRad = 10;//"roam radius" roam range. kinda like a sphere
    Vector3 currentPoint;
    bool hasPoint;

    [Header("-----Attack-----")]
    [SerializeField] float attackCooldown;
    [SerializeField] float attackDamage;
    bool onCooldown;

    [Header("-----Detection-----")]
    [SerializeField] float sightRange = 20;
    [SerializeField] float engagementRange = 5;
    bool visiblePlayer;
    bool playerInRange;


    //void Start()// Start is called once before the first execution of Update after the MonoBehaviour is created
    //{}

    void Update()// Update is called once per frame
    {
        DetectPlayer();
        if (visiblePlayer && playerInRange)
        {
            //Debug.Log("Player seen and in range!");
            Attack();
        }
        else if (!visiblePlayer && !playerInRange)
        {
            //Debug.Log("Player not seen and not in range!");
            Roam();
        }
        else if (visiblePlayer && !playerInRange)
        {
            //Debug.Log("Player seen and not in range!");
            Pursue();
        }
    }

    private void OnDrawGizmosSelected()//this is so helpful!! now I can use this method to visualize different ranges
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, engagementRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    void DetectPlayer()//checks the vision and range spheres to find out if the player is inside them
    {
        visiblePlayer = Physics.CheckSphere(transform.position, sightRange, playerLayerMask);
        playerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask);
    }

    void Attack()
    {
        //Debug.Log("Bear Attacking!");
        meshAgent.SetDestination(transform.position);//stop
        if (playerTrans != null)
        {
            transform.LookAt(playerTrans.position);//face player. yet another helpful command!
        }
        if (onCooldown == false && playerInRange)
        {
            EnvironmentCtrl.Game.player.AlterHealth(-attackDamage);
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    void FindPoint()//how the bear locates it's next roaming destination
    {
        float randX = Random.Range(-roamRad, roamRad);
        float randZ = Random.Range(-roamRad, roamRad);//note for my future self: yes it IS Z, Z and not Y because we dont want the bear to roam into the SKY.

        Vector3 potentialPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);

        if (Physics.Raycast(potentialPoint, -transform.up, roamDist, terrainLayer))
        {
            currentPoint = potentialPoint;
            hasPoint = true;
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        //an animation will also play here eventually
        onCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        onCooldown = false;
    }

    void Roam()
    {
        if (hasPoint == false)
        {
            FindPoint();
        }
        if (hasPoint)
        {
            meshAgent.SetDestination(currentPoint);
        }
        if (Vector3.Distance(transform.position, currentPoint) < 1f)//because in this case you wouldnt really be moving
        {
            hasPoint = false;
        }
    }

    void Pursue()
    {
        if (playerTrans != null)
        {
            meshAgent.SetDestination(playerTrans.position);
        }
    }
}