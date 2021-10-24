using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSettings
{
    [System.Serializable]
    public class SettingData
    {
        public float musicVolume;
        public float sfxVolume;
        public bool isMute;

        public SettingData(Audio.AudioController audioController) {
            musicVolume = audioController.musicVolume;
            sfxVolume = audioController.SFXVolume;
            isMute = audioController.mute;
        }

        public SettingData() {
            musicVolume = 0.5f;
            sfxVolume = 0.8f;
            isMute = false;
        }

    }

}