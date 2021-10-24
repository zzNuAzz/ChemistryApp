using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        // Start is called before the first frame update
        protected GUI.InGameInterface gui;
        protected bool pauseGame = false;

        [SerializeField] private int m_life = 3;
        
        [SerializeField] private int m_score = 0;

        private void Awake()
        {
            Question.QuestionManager.Instance.MakeInstance();
            gui =
                GameObject.Find("/GameUI").GetComponent<GUI.InGameInterface>();
        }

        // Start is called before the first frame update
        protected void Start()
        {
            
        }

        // Update is called once per frame
        protected void Update()
        {
            // render Score 
            gui.scoreUI.GameScore = m_score;
            gui.lifeUI.lifeCount = m_life;
            CheckGameOver();
        }

        protected virtual void SubmitAnswerCallback(bool isCorrect) {

        }

        public void GameOver() {
            if(pauseGame == false) {
                var audioController = GameObject.FindObjectOfType<Audio.AudioController>();
                if(audioController != null) {
                    if(m_life == 0) {
                    audioController.PlayOneShot(audioController.lose);
                    } else {
                        audioController.PlayOneShot(audioController.win);
                    }
                }
               
            }
            pauseGame = true;
            gui.ShowGameOverPanel(true);
            gui.clock.Pause();
        }

        public void SubmitAnswer(bool isCorrect)
        {
            if (isCorrect)
            {
                gui.ShowQuestionPanel(false, false);
                AddScore(10);
            }
            else
            {
                gui.ShowQuestionPanel(false, false);
                AddLife(-1);
            }
            SubmitAnswerCallback(isCorrect);
            pauseGame = false;
        }

        private void AddLife(int count) {
            m_life = m_life + count >= 0 ? m_life + count : 0;
        }

        private void CheckGameOver() {
            if(m_life == 0) {
                GameOver();
            }
        }

        protected void AddScore(int score)
        {
            m_score += score;
        }

        private void OnValidate() {
            if(gui == null) {
                gui = GameObject.Find("/GameUI").GetComponent<GUI.InGameInterface>();
            }
            gui.scoreUI.GameScore = m_score;
        }

        public void ShowQuestion(bool state) {
            if(state == true) {
                gui.ShowQuestionPanel(true, true);
                pauseGame = true;
            } else {
                gui.ShowQuestionPanel(false, false);
                pauseGame = false;
            }
        }
    }
}
