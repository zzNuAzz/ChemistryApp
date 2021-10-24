using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class ItemVFX : MonoBehaviour
    {
        [SerializeField] Animator m_Animator;
        [SerializeField] Vector3 m_initScale = new Vector3(1,1,1);
        [SerializeField] private bool m_hide = false;
        Transform m_Transform;

        public float delay = 2; // second

        // Start is called before the first frame update
        void Start()
        {
            m_Transform = gameObject.GetComponent<Transform>();
            m_Transform.localScale = m_initScale;
            m_Animator.SetBool("Idle", false);
            StartCoroutine(ShowVFX());
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Destroy(gameObject);
            }
        }

        private void setHide(bool hideState) {
            m_hide = hideState;
            m_Animator.SetBool("Idle", hideState);
        }

        public IEnumerator ShowVFX()
        {
            setHide(false);
            yield return new WaitForSeconds(delay);

            setHide(true);
        }
        private void OnValidate() {
            if(m_hide) {
                gameObject.GetComponent<Transform>().localScale = new Vector3(0, 0);
            } else {
                gameObject.GetComponent<Transform>().localScale = m_initScale;
            }
        }
    }
}
