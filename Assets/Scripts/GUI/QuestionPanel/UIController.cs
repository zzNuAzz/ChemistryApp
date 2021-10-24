using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.QuestionPanel
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private bool m_showing;
        [SerializeField]
        private Text m_questionText;
        [SerializeField]
        private AnswerButton[] m_answerButtons;

        public bool showing {
            get {
                return m_showing;
            }
            set {
                if(m_showing != value) {
                    m_showing = value;
                    UpdateVisibility();
                }
            }
        }

        private void Awake()
        {

        }
        

        public void SubmitAnswer(AnswerButton ansBtn) {
            bool isCorrect = ansBtn.IsCorrect();
            
            Game.GameController gc = GameObject.Find("/GameController").GetComponent<Game.GameController>();
            gc.SubmitAnswer(isCorrect);

            VFX.VFXController vfxc = GameObject.Find("VFX").GetComponent<VFX.VFXController>();
            vfxc.ShowVFX(isCorrect ? "yesDuckSign" : "noDuckSign");
        }

        public void RenderQuestion(Question.Question question) {
            // shuffle answers
            string[] answers = { question.wrongAnswer1, question.wrongAnswer2, question.wrongAnswer3, question.expectedAnswer };
            // Knuth shuffle algorithm [https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle]
            int length = answers.Length;
            for (int t = 0; t < length; t++)
            {
                int r = Random.Range(t, length);
                string tmp = answers[t];
                answers[t] = answers[r];
                answers[r] = tmp;
            }
            
            // render to UI
            m_questionText.text = question.question;
            for(int i = 0; i < m_answerButtons.Length; i++) {
                if (m_answerButtons[i] == null) continue; // rare condition
                m_answerButtons[i].tag = (answers[i] == question.expectedAnswer) ? "TrueAnsButton" : "FalseAnsButton";
                m_answerButtons[i].setText(answers[i]);
            }
        }

        private void OnValidate() {
            UpdateVisibility();
        }

        private void UpdateVisibility() {
            if(gameObject != null) {
                if(m_showing) {
                    gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
                } else {
                    gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
                }
            }
        }
    }
}
