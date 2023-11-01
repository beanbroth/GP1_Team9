using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class IntroCutscene : MonoBehaviour
{
    [SerializeField] private GameObject[] Scenes;
    
    [SerializeField] GameObject[] Texts;

    [SerializeField] private float[] Scene1Times;
    
    [SerializeField] GameObject Camera;
    
    int SceneIndex = 0;
    float SceneTimer = 0;
    float SceneTimeTarget = 0;
    bool SceneDone = false;
    
    [FormerlySerializedAs("CameraTransitionSpeed")] [SerializeField] float CameraTransitionTime = 0.1f;
    float CameraTransitionTimer = 0;
    float CameraTransitionTarget = 0;
    bool StartTransition = false;
    
    
    private Vector3[] SceneCameraPosistions = new Vector3[3];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Scenes.Length; i++)
        {
            SceneCameraPosistions[i] = new Vector3(Scenes[i].transform.position.x, Scenes[i].transform.position.y, Camera.transform.position.z);
            if (i!=0)
            {
                Scenes[i].SetActive(false);
                Texts[i].SetActive(false);
            }
            
        }
        SceneTimeTarget = Scene1Times[SceneIndex];
        CameraTransitionTarget = CameraTransitionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTransition)
        {
            CameraTransitionTimer += Time.deltaTime;
            if (CameraTransitionTimer >= CameraTransitionTarget)
            {
                StartTransition = false;
                CameraTransitionTimer = 0;
                //CameraTransitionTarget = 0;
                Scenes[SceneIndex-1].SetActive(false);
                Texts[SceneIndex-1].SetActive(false);
                SceneTimer = 0;
                SceneTimeTarget = Scene1Times[SceneIndex];
                Scenes[SceneIndex].SetActive(true);
                Texts[SceneIndex].SetActive(true);
                Camera.transform.position = SceneCameraPosistions[SceneIndex];
                return;
            }
            CameraTransition();
            return;
        }
        SceneTimer += Time.deltaTime;
        if (SceneTimer >= SceneTimeTarget)
        {
            SceneDone = true;
        }
        //Debug.Log(SceneTimer);

        if (Input.GetKeyDown(KeyCode.Space) && SceneDone)
        {
            SceneDone = false;
            
            SceneIndex++;
            if (SceneIndex >= Scenes.Length)
            {
                SceneManager.LoadScene(3);
                return;
            }
            StartTransition = true;
        }
    }
    void CameraTransition()
    {
        Camera.transform.position = Vector3.Lerp(SceneCameraPosistions[SceneIndex-1], SceneCameraPosistions[SceneIndex], CameraTransitionTimer / CameraTransitionTarget);
    }
}
