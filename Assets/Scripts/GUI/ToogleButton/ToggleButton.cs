using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GUI
{
    [ExecuteInEditMode]
    public class ToggleButton : MonoBehaviour
    { 
        [SerializeField]
        private Sprite[] switchSprites;

        public UnityEvent OnValueChanged;

        public bool isOn;

       
        int _spriteIdx {
            get {
                return isOn ? 0 : 1;
            }
        }

        private Image switchImage;

        // Start is called before the first frame update
        private void Awake() {
            gameObject.GetComponent<Button>().onClick.AddListener(TurnOnAndOff);
        }
        void Start()
        {
            switchImage = GetComponent<Button>().image;
            
        }

        private void Update() {
            switchImage.sprite = switchSprites[_spriteIdx];
        }

        void TurnOnAndOff()
        {
            Debug.Log(OnValueChanged.GetPersistentEventCount());
            isOn = !isOn;
            switchImage.sprite = switchSprites[_spriteIdx];
            if(OnValueChanged != null) {
                OnValueChanged.Invoke();
            }

        }
        
        private void OnValidate() {
            if(switchImage) {
                switchImage.sprite = switchSprites[_spriteIdx];
            }
        }
        
    }
}
