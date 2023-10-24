using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class S_CerntainDeathBoltController : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DisableAfterTime", 0.5f);

    }

    void DisableAfterTime()
    {
        gameObject.SetActive(false);
    }

}
