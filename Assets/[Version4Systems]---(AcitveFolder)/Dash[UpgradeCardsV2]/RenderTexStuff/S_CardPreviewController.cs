using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class S_CardPreviewController : MonoBehaviour
{
    [SerializeField] List<GameObject> cameraPods = new List<GameObject>();
    [SerializeField] List<GameObject> dummyPrefabs = new List<GameObject>();

    private void Awake()
    {
        DisableAllCardPods();
        PauseManager.OnPauseStateChange += PauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseManager.OnPauseStateChange -= PauseStateChanged;
    }


    void PauseStateChanged(bool isPuaused)
    {
        if (!isPuaused)
        {
            DisableAllCardPods();
        }
    }
    
    public void SetUpCardPreview(GameObject weaponPrefab, int ix)
    {
        //enable the correct dummy prefab
        dummyPrefabs[ix].SetActive(true);
        
        //enable the correct camera pod
        cameraPods[ix].SetActive(true);
        
        //instantiate the weapon prefab as a child of the dummy prefab
        GameObject weapon = Instantiate(weaponPrefab, dummyPrefabs[ix].transform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        
    }

    public void DisableAllCardPods()
    {
        //destroy all children of dummy prefabs
        foreach(GameObject dummy in dummyPrefabs)
        {
            foreach (Transform child in dummy.transform)
            {
                Destroy(child.gameObject);
            }
            dummy.SetActive(false);
        }
        
        foreach (GameObject pod in cameraPods)
        {
            pod.SetActive(false);
        }
    }
}
