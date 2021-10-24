using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.QuestionPanel
{
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField]
        private Text answerText;
        [SerializeField]
        private UIController controller;
        public void setText(string text) {
            if(answerText) {
                answerText.text = text;
            }
        }

        public bool IsCorrect() {
            if(gameObject && gameObject.tag == "TrueAnsButton") {
                return true;
            } else {
                return false;
            }
        }

        public void SubmitAnswer() {
            controller.SubmitAnswer(this);
        }

    }
}
