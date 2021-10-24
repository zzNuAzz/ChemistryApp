using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GUI
{
    public class TimerClock : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text textTimer;

        private float m_timer = 0.0f;

        private bool m_isTimer = true;

        [SerializeField] private bool m_isTimerCountdown = false;
        [Tooltip("Timer set on start clock (second)")]
        [SerializeField] private float m_timeStartWhenCountdown;

        [SerializeField] private UnityEvent OnTimeout;

        // Update is called once per frame
        void Update()
        {
            // Debug.Log(m_timer);
            if (m_isTimer)
            {   
                m_timer += Time.deltaTime;
                DisplayTime();
            }
            if(m_isTimer) 
            {
                if(m_isTimerCountdown && m_timer > m_timeStartWhenCountdown) {
                    Pause();
                    OnTimeout.Invoke();
                }
            }
        }

        void DisplayTime()
        {
            float _timeRemaining = m_timer;
            if(m_isTimerCountdown) {
                _timeRemaining = m_timeStartWhenCountdown - m_timer;
            }
            if(_timeRemaining <= 0) {
                _timeRemaining = 0;
            }
            int minutes = (int) Mathf.Floor(_timeRemaining / 60.0f);
            int seconds = (int) Mathf.Floor(_timeRemaining - minutes * 60.0f);
            textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        void start()
        {
            m_isTimer = true;
        }

        public void Pause()
        {
            m_isTimer = false;
        }

        public void Reset()
        {
            m_timer = 0.0f;
        }

        public void SetTimeCountDown(float seconds) {
            m_isTimerCountdown = true;
            m_timeStartWhenCountdown = seconds;
        }

        public void AddTime(float bonus) {
            m_timeStartWhenCountdown += bonus;
        }

    }
}
