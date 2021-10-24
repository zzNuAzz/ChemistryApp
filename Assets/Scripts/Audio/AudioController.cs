using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 1)]
        private float m_musicVolume;

        [SerializeField]
        [Range(0, 1)]
        private float m_SFXVolume;

        [SerializeField]
        private bool m_mute;

        public UnityEvent<bool> OnMuteChange;

        public float musicVolume
        {
            get
            {
                return m_musicVolume;
            }
            set
            {
                if (m_musicVolume != value)
                {
                    if (value < 0)
                    {
                        value = 0;
                    }
                    else if (value > 1)
                    {
                        value = 1;
                    }
                    m_musicVolume = value;
                    UpdateChangeValue();
                }
            }
        }

        public float SFXVolume
        {
            get
            {
                return m_SFXVolume;
            }
            set
            {
                if (m_SFXVolume != value)
                {
                    if (value < 0)
                    {
                        value = 0;
                    }
                    else if (value > 1)
                    {
                        value = 1;
                    }
                    m_SFXVolume = value;
                    UpdateChangeValue();
                }
            }
        }

        public bool mute {
            get {
                return m_mute;
            }
            set {
                if(m_mute != value) {
                    Debug.Log("change");
                    m_mute = value;
                    UpdateChangeValue();
                    OnMuteChange.Invoke(value);
                }
            }
        }

        private void UpdateChangeValue()
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = m_musicVolume;
                audioSource.mute = m_mute;
            }
            var settings = GameObject.FindObjectOfType<GameSettings.Settings>();
            if (settings) {
                settings.MuteMusic(m_mute);
            }

        }

        private void OnValidate()
        {
            if(Application.isPlaying) {
                return;
            }
            UpdateChangeValue();
        }

        public AudioClip handClap;
        public AudioClip win;
        public AudioClip lose;
        public AudioClip ting;
        public AudioClip wrongs;
        public AudioClip click;
        public AudioClip jump;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            Debug.Log("Destroy");
            GameSettings.SettingData settingData = new GameSettings.SettingData(this);
            Utils.SaveSystem.Save<GameSettings.SettingData>(settingData, "settings");
        
        }

        public void PlayOneShot(AudioClip clip) {
            var audioSource = gameObject.GetComponent<AudioSource>();
            if(audioSource != null) {
                audioSource.PlayOneShot(clip, m_SFXVolume);
            }
        }

    }

}