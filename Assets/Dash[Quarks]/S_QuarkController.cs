using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_QuarkController : MonoBehaviour
{
    private static Transform player;
    [SerializeField] private SO_QuarkManager quarkManager;

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject.transform.root;
        }

        //move towards player
        transform.position = Vector3.MoveTowards(transform.position, player.position, 10f * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            quarkManager.AddQuarks(1);
            S_ObjectPoolManager.Instance.ReturnObject(gameObject);
        }
    }
}