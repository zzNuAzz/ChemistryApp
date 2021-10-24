using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI {
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TMP_Text scoreDisplay;

        private int m_score;

        public int GameScore {
            get {
                return m_score;
            }
            set {
                if(value < 0) return;
                if (scoreDisplay)
                {
                    m_score = value;
                    scoreDisplay.text = string.Format("Score: {0}", value);
                }
                else
                {
                    Debug.LogWarning("Score display is null");
                }
            }
        }
    };
}
