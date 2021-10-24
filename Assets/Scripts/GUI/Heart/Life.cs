using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI {
    [ExecuteInEditMode]
    public class Life : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_hearts;
        [SerializeField] private int m_lifeCount = 3;

        public int lifeCount {
            get {
                return m_lifeCount;
            }
            set {
                if(m_lifeCount == value) return;
                m_lifeCount = value;
                UpdateVisibility();
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            UpdateVisibility();
        }

        private void UpdateVisibility() {
            for(int i = 0; i < m_hearts.Length; i++) {
                if(i < m_lifeCount) {
                    if(m_hearts[i]) {
                        m_hearts[i].SetActive(true);
                    }
                } else {
                    if(m_hearts[i]) {
                        m_hearts[i].SetActive(false);
                    }
                }
            }
        }
    }

}