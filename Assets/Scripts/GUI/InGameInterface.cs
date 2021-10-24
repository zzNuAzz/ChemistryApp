using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class InGameInterface : MonoBehaviour
    {
        public GUI.TimerClock clock;
        [SerializeField]
        private GUI.QuestionPanel.UIController questionPanel;
        public GUI.ScoreDisplay scoreUI;
        public GUI.Life lifeUI;
        public GUI.GamePanel gameOverUI;
        public GUI.GamePanel gameVictoryUI;
        public GUI.GameInfo gameInfoUI;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ShowQuestionPanel(bool state, bool resetQuestion) {
            if(state && resetQuestion) {
                Question.Question question = Question.QuestionManager.Instance.getRandomQuestion();
                if(question != null) {
                    questionPanel.RenderQuestion(question);
                    questionPanel.showing = true;
                } else {
                    Debug.LogWarning("Get question failed");
                }
            } else {
                 questionPanel.showing = state;
            }
        }

        public void ShowGameOverPanel(bool state) {
            if(gameOverUI) {
                gameOverUI.showing = state;
            }
        }

        public void ShowGameInfoPanel(bool state) {
            if(gameOverUI) {
                gameInfoUI.showing = state;
            }
        }

        public void ShowGameVictoryPanel(bool state) {
             if(gameVictoryUI) {
                gameVictoryUI.showing = state;
            }
        }

        private void OnTimeout() {
            var _gameObject = GameObject.Find("/GameController");
            if(_gameObject != null) {
                var gameController = _gameObject.GetComponent<Game.GameController>();
                gameController.GameOver();
            }
        }

    }
}
