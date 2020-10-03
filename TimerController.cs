using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    public Text timerText;
    public float time;
    public float startTime;
    public float prePauseTime;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        timerText.enabled = false;
        isPaused = false;
        prePauseTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerText.enabled && !isPaused) {
            time = Time.time - startTime + prePauseTime;
            timerText.text = time.ToString("F1");
        }
    }

    public void startTimer() {

        time = 0;
        prePauseTime = 0;
        startTime = Time.time;
        timerText.enabled = true;

    }

    public void stopTimer() {

        timerText.enabled = false;
        timerText.text = "0.0";

    }

    public void pauseTimer() {

        isPaused = true;
        prePauseTime = time;

    }

    public void unpauseTimer() {

        isPaused = false;
        startTime = Time.time;

    }

}
