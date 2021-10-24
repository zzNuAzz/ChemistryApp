using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GUI {
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] private bool m_showing;
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

        void Start()
        {
            
        }

        void Update()
        {
        }

        private void UpdateVisibility() {
            if(gameObject == null) return;
            if(m_showing) {
                gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
            } else {
                gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
            }
        }

    private void OnValidate() {
        UpdateVisibility();
    }

    }
}

