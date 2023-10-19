using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class S_DamageTrailObject : MonoBehaviour
{
    public float destoryTime;

    private void Awake()
    {
        Invoke("Destroy", destoryTime);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    //On Collision for damage output
}
