using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_QuarkController : MonoBehaviour
{
    private static Transform player;
    [SerializeField] private float startingMoveSpeed = 5f;
    [SerializeField] private float maxSpeedMult = 8f;
    [SerializeField] private static float pickupRange = 7f;
    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] private float lifetime = 30f;
    private float timer;

    private bool isDelayComplete = false;
    [SerializeField] private float delayDuration = 0.3f;
    private float delayTimer = 0.0f;

    private void OnEnable()
    {
        isDelayComplete = false;
        delayTimer = 0.0f;
        timer = lifetime;

    }

    private void Update()
    {
        // If the delay is not complete, increment the delay timer
        if (!isDelayComplete)
        {
            delayTimer += Time.deltaTime;

            // If the delay timer exceeds the delay duration, mark the delay as complete
            if (delayTimer >= delayDuration)
            {
                isDelayComplete = true;
                timer = lifetime;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ObjectPoolManager.ReturnObject(gameObject);
            }

            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").gameObject.transform.root;
            }

            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer <= pickupRange)
            {
                float maxSpeed = startingMoveSpeed * maxSpeedMult;
                float speedFactor = 1 - (distanceToPlayer / pickupRange);
                float curveValue = accelerationCurve.Evaluate(speedFactor);
                float currentSpeed = Mathf.Lerp(startingMoveSpeed, maxSpeed, curveValue);
                float moveSpeedMult = currentSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeedMult);
            }
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.tag == "Player")
        {
            QuarkManager.AddQuarks(1);
            AudioManager.Instance.PlaySound3D("QuarkPickup", transform.position);
            ObjectPoolManager.ReturnObject(gameObject);
        }
    }

    public void SetPickupRange(float newRange)
    {
        pickupRange = newRange;
    }
    public float GetPickupRange()
    {
        return pickupRange;
    }
    public void AddPickupRange(float extraRange)
    {
        pickupRange += extraRange;
    }

}