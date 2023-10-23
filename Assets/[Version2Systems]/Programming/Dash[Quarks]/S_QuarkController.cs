using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_QuarkController : MonoBehaviour
{
    private static Transform player;
    [SerializeField] private SO_QuarkManager quarkManager;
    [SerializeField] private float startingMoveSpeed = 5f;
    [SerializeField] private float maxSpeedMult = 8f;
    [SerializeField] private float pickupRange = 7f;
    [SerializeField] private AnimationCurve accelerationCurve;

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject.transform.root;
        }

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Move towards the player if within pickup range
        if (distanceToPlayer <= pickupRange)
        {
            float maxSpeed = startingMoveSpeed * maxSpeedMult;
            float speedFactor = 1 - (distanceToPlayer / pickupRange); // Calculate the speed factor based on the distance to the player
            float curveValue = accelerationCurve.Evaluate(speedFactor); // Sample the acceleration curve based on the speed factor
            float currentSpeed = Mathf.Lerp(startingMoveSpeed, maxSpeed, curveValue); // Interpolate between startingMoveSpeed and maxSpeed based on curveValue
            float moveSpeedMult = currentSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeedMult);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.tag == "Player")
        {
            quarkManager.AddQuarks(1);
            ObjectPoolManager.ReturnObject(gameObject);
        }
    }
}