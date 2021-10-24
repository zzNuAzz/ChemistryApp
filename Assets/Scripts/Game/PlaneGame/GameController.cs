using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PlaneGame
{
    public class GameController : Game.GameController
    {
        [SerializeField] private int m_passedQuestion = 0;
        [SerializeField] private int m_totalQuestionPerMap = 2;

        new void Start()
        {
            base.Start();
        }

        new void Update()
        {
            base.Update();
        }

        protected override void SubmitAnswerCallback(bool isCorrect) {
            m_passedQuestion++;
            if(m_passedQuestion < m_totalQuestionPerMap) {
                GameObject.Find("/Player").GetComponent<Plane>().currentColisionCloud.Reset();
            } else {
                // Todo: changes to next level
                SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
            }
        }
    }
}
