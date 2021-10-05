using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerClock : MonoBehaviour
{
    public TMP_Text textTimer;
    
    private float timer = 0.0f;
    private bool isTimer = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimer) {
            timer += Time.deltaTime;
            DisplayTime();
        }
    }

    void DisplayTime() {
        int minutes = (int)Mathf.Floor(timer / 60.0f);
        int seconds = (int)Mathf.Floor(timer - minutes * 60.0f);
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void start() {
        isTimer = true;
    }
    
    void stop() {
        isTimer = false;
    }

    void reset() {
        timer = 0f;
    }
}
