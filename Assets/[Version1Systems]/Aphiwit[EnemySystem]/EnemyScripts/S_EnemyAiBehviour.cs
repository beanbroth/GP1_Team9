using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_EnemyAiBehviour : MonoBehaviour
{
    // I'm currently reviewing the tutorials use of publics and the necessity, if needed; I'll cahnge it to a private [SerializedField] and add 'm_' to them.

    [SerializeField] NavMeshAgent navMeshAgent;

    [SerializeField] Transform playerTransform;

    [SerializeField] LayerMask groundLayerMask, playerLayerMask;

    // Patroling and chasing
    [SerializeField] float walkPointRange;
    private Vector3 walkPoint;
    [SerializeField] bool walkPointSet;


    // States
    public float detectionRange, agroRange;
    public bool playerInDetectionRange, playerInAgroRange;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for player in range & chase player when they are in agro range
        playerInDetectionRange = Physics.CheckSphere(transform.position, detectionRange, playerLayerMask);
        playerInAgroRange = Physics.CheckSphere(transform.position, agroRange, playerLayerMask);

       
    }

    private void FixedUpdate()
    {
        // Add check to see if navagent is on field
        // Add intervall
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
        if (!Physics.Raycast(walkPoint, -transform.up, 2f, groundLayerMask))
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, agroRange);
    }

}
