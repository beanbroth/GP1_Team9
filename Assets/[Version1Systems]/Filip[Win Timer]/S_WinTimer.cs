using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class S_WinTimer : MonoBehaviour
{
    [Header("Time Management")]
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] float currentTime = 600f;
    [SerializeField] float timeLimit = 0f;
    private bool countUp = false; //if true, change the maxtime current time calculation for the phase slider

    [Header("Phase Management")]
    [SerializeField] Slider phaseSlider;
    [SerializeField] GameObject[] phasesObjects;
    public static Action<int> newPhase;
    [SerializeField] float timePerPhase = 120f;
    private float timeSinceLastPhase;
    private float maxTime;
    public int currentPhase = 1;

    [Header("Enemy Spawning")]
    [SerializeField] float enemySpawnInterval = 60;
    public static Action<int> enemySpawnerUpdate;
    private float timeSinceLastSpawn = 0;
    private int enemySpawnerPhase = 1;

    [Header("Music Management")]
    [SerializeField] float[] timesForMusicSwitchs;
    int currentMusicIndex = 0;
    float timeUntilNextMusicSwitch;
    S_MusicManager musicManager;

    S_SceneTransition sceneTransitionManager;
    private void Awake()
    {
        sceneTransitionManager = FindFirstObjectByType<S_SceneTransition>();
        musicManager = FindFirstObjectByType<S_MusicManager>();
        timeUntilNextMusicSwitch = timesForMusicSwitchs[0];
    }

    private void Start()
    {
        maxTime = currentTime;
        int phaseObjectIndex = 0;
        foreach (var phasesObject in phasesObjects)
        {
            if (phaseObjectIndex == 0)
                phasesObject.SetActive(true);
            else
                phasesObject.SetActive(false);
            phaseObjectIndex++;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (PauseManager.IsPaused)
            return;

        //Current time
        currentTime = countUp ? currentTime += Time.deltaTime : currentTime -= Time.deltaTime;

        //Win timer
        if ((countUp && currentTime >= timeLimit) || (!countUp && currentTime <= timeLimit))
        {
            currentTime = timeLimit;
            TimerText();
            enabled = false;
            sceneTransitionManager.SceneFadeOutAndLoadScene(Color.white, sceneEnum.outroCutScene);
        }
        TimerText();

        //Music track management
        if (currentTime < timeUntilNextMusicSwitch)
        {
            currentMusicIndex++;
            if (currentMusicIndex >= timesForMusicSwitchs.Length)
            {
                musicManager.SwitchToNextTrack();
                timeUntilNextMusicSwitch = -1;
            }
            else
            {
                timeUntilNextMusicSwitch = timesForMusicSwitchs[currentMusicIndex];
                musicManager.SwitchToNextTrack();
            }
        }

        //Enemy spawner timer
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > enemySpawnInterval)
        {
            enemySpawnerPhase++;
            timeSinceLastSpawn = 0f;
            if (enemySpawnerUpdate != null)
            {
                enemySpawnerUpdate.Invoke(enemySpawnerPhase);
            }
        }

        //Phase timer
        timeSinceLastPhase += Time.deltaTime;
        if (timeSinceLastPhase > timePerPhase)
        {
            currentPhase++;
            timeSinceLastPhase = 0f;
            //Debug.Log("New phase: " + currentPhase);
            if (newPhase != null)
            {
                newPhase.Invoke(currentPhase);
            }
            //ActivatePhaseObject(currentPhase - 1);
        }
        UpdatePhaseSlider((maxTime - currentTime) / maxTime);
    }

    private void UpdatePhaseSlider(float sliderPercent)
    {
        phaseSlider.value = sliderPercent;
    }
    public void ActivatePhaseObject(int objectIndex, bool status = true, bool bounce = true)
    {
        phasesObjects[objectIndex].SetActive(status);
        if (bounce)
        {
            phasesObjects[objectIndex].GetComponent<UIScaleBounce>().PerformBounceAnimation();
        }
    }

    public string TimerText()
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        string formattedText = string.Format("{0:00}:{1:00}", minutes, seconds);

        timeText.text = formattedText;
        return formattedText;
    }
    public int GetCurrentPhase()
    {
        return currentPhase;
    }
}
