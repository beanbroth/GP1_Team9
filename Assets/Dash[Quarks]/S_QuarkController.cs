using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class S_QuarkController : MonoBehaviour
{
    private static Transform player;
    [SerializeField] private SO_QuarkManager quarkManager;
    private float moveSpeedMult;
    [SerializeField] private float moveSpeedMultMax;
    [SerializeField] private float moveSpeedMultMin;

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject.transform.root;
        }

        //move towards player
        moveSpeedMult *= 1 / Mathf.Max(Vector3.Distance(player.transform.position, transform.position) / 5f, .5f);
        moveSpeedMult = Mathf.Clamp(moveSpeedMult, moveSpeedMultMin, moveSpeedMultMax);
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeedMult * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.tag == "Player")
        {
            quarkManager.AddQuarks(1);
            S_ObjectPoolManager.Instance.ReturnObject(gameObject);
        }
    }
}