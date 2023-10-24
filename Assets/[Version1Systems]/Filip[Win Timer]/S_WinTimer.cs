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
    public TextMeshProUGUI timeText;

    [SerializeField] float currentTime = 600f;
    [SerializeField] float timeLimit = 0f;
    private bool countUp = false; //if true, change the maxtime current time calculation for the phase slider

    [Header("Phase Management")]
    [SerializeField] Slider phaseSlider;
    public static Action<int> newPhase;
    [SerializeField] float timePerPhase = 120f;
    private float timeSinceLastPhase;
    private float maxTime;
    public int currentPhase = 1;

    private void Start()
    {
        maxTime = currentTime;
    }


    // Update is called once per frame
    void Update()
    {
        currentTime = countUp ? currentTime += Time.deltaTime : currentTime -= Time.deltaTime;

        if ((countUp && currentTime >= timeLimit) || (!countUp && currentTime <= timeLimit))
        {
            currentTime = timeLimit;
            TimerText();
            enabled = false;
            SceneManager.LoadScene("Victory");
        }

        TimerText();

        timeSinceLastPhase += Time.deltaTime;
        if (timeSinceLastPhase > timePerPhase)
        {
            currentPhase++;
            Debug.Log("New phase: " + currentPhase);
            if(newPhase != null)
            {
                newPhase.Invoke(currentPhase);
            }
            timeSinceLastPhase = 0f;
        }
        UpdatePhaseSlider((maxTime-currentTime)/maxTime);
    }

    private void UpdatePhaseSlider(float sliderPercent)
    {
        phaseSlider.value = sliderPercent;
    }

    private void TimerText()
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
