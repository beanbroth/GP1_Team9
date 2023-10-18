using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_EnemyAiBehviour : MonoBehaviour
{
    // This behaviour script will be used for simple enemy AI movement and behaviour
    // I'm currently reviewing the tutorials use of publics and the necessity, if needed; I'll cahnge it to a private [SerializedField] and add 'm_' to them.
    // Properties and such will also be utilized for the stats off the enemies.

    public NavMeshAgent navMeshAgent;

    public Transform playerTransform;

    public LayerMask groundLayerMask, playerLayerMask;

    // Patroling and chasing
    public Vector3 walkPoint;
    public float walkPointRange;
    [SerializeField] bool walkPointSet;


    // States
    public float detectionRange, agroRange;
    public bool playerInDetectionRange, playerInAgroRange;

    private void Awake()
    {
        // [Improvement; Get the player from a game manager.]
        // This script currently uses the 'GameObject.Find' to locate the player and their position (this is data heavy).

        playerTransform = GameObject.Find("TestPlayer").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for player in range & chase player when they are in agro range
        playerInDetectionRange = Physics.CheckSphere(transform.position, detectionRange, playerLayerMask);
        playerInAgroRange = Physics.CheckSphere(transform.position, agroRange, playerLayerMask);

        if (!playerInDetectionRange && !playerInAgroRange)
        {
            Patroling();
        }

        if (playerInDetectionRange && !playerInAgroRange) 
        { 
            FollowPlayer();
        }

        if (playerInDetectionRange && playerInAgroRange)
        {
            ChasePlayer();
        }
    }

    private void Patroling()
    {
        // Searching for walkpoint
         if (!walkPointSet)
        {
            SearchWalkPoint();
        }

         if (walkPointSet)
        {
            navMeshAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

         // Walkpoint reached
         if (distanceToWalkPoint.magnitude <= 1f)
        {
            walkPointSet = false;
        }


    }

    private void SearchWalkPoint()
    {
        // Generate a random position to walk to
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if point generated exists
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayerMask))
        {
            walkPointSet = true;
        }
    }

    private void FollowPlayer()
    {
        navMeshAgent.SetDestination(playerTransform.position);

        // Edit enemy speed variable
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(playerTransform.position);

        // Edit enemy speed variable
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, agroRange);
    }

}
