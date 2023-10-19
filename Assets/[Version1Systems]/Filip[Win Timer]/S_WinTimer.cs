using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_WinTimer : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    public float currentTime = 0f;
    public float timeLimit = 600f;


    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime += Time.deltaTime;

        if (currentTime >= timeLimit)
        {
            currentTime = timeLimit;
            TimerText();
            timeText.color = Color.green;
            enabled = false;
            Time.timeScale = 0;
        }

        TimerText();
    }

    private void TimerText()
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
