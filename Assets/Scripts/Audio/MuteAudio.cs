using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio {
    public class MuteAudio : MonoBehaviour {


        private void Awake() {
            var button = gameObject.GetComponent<GUI.ToggleButton>();
            if(button == null) {
                return;
            }
            var audioController = GameObject.FindObjectOfType<Audio.AudioController>();


            if(audioController != null) {
                button.isOn = !audioController.mute;
                audioController.OnMuteChange.AddListener(Change);
            } 
        }
        public void OnChange(GUI.ToggleButton button) {
            var settings = GameObject.FindObjectOfType<GameSettings.Settings>();
            if(settings != null) {
                settings.MuteMusic(!button.isOn);
            }
        }

        public void Change(bool isMute) {
            var button = gameObject.GetComponent<GUI.ToggleButton>();
            if(button == null) {
                return;
            }
            button.isOn = !isMute;
        }

        private void OnDestroy() {
            var audioController = GameObject.FindObjectOfType<Audio.AudioController>();
            if(audioController != null) {
                audioController.OnMuteChange.RemoveListener(Change);
            }
        }
    }
}