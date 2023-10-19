using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class S_WinTimer : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    [SerializeField] float currentTime = 600f;
    [SerializeField] float timeLimit = 0f;
    [SerializeField] private bool countUp;
 


    // Update is called once per frame
    void Update()
    {
        currentTime = countUp ? currentTime += Time.deltaTime : currentTime -= Time.deltaTime;

        if ((countUp && currentTime >= timeLimit) || (!countUp && currentTime <= timeLimit))
        {
            currentTime = timeLimit;
            TimerText();
            timeText.color = Color.green;
            enabled = false;
            SceneManager.LoadScene("Victory");
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
