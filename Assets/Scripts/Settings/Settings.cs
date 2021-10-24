using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private bool m_showing;
        public bool showing
        {
            get
            {
                return m_showing;
            }
            set
            {
                if (m_showing != value)
                {
                    m_showing = value;
                    UpdateVisibility();
                }
            }
        }

        [SerializeField] private Audio.AudioController m_audioController;
        [SerializeField] private Slider m_musicSlider;
        [SerializeField] private Slider m_sfxSlider;
        [SerializeField] private Dropdown m_subject;



        private void OnValidate()
        {
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            if (gameObject != null)
            {
                if (m_showing)
                {
                    gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
                }
                else
                {
                    gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
                }
            }
        }

        public void Show(bool state)
        {
            showing = state;
        }

        public static void Show() {
            var settings = GameObject.FindObjectOfType<Settings>();
            if(settings != null) {
                settings.showing = true;
            }
        }

        private void Start() {
            var savedSetting = Utils.SaveSystem.Load<SettingData>("settings");
            ApplySetting(savedSetting != null ? savedSetting : new SettingData());

            if(m_subject != null) {
                List<string> options = new List<string> ();
                foreach (var option in Question.QuestionManager.questionSet) {
                    options.Add(option.Item1);
                }
                m_subject.ClearOptions();
                m_subject.AddOptions(options);
                m_subject.value = Question.QuestionManager.Instance.subject;
            }
        }

        public void OnMusicVolumeChanged(Slider slider)
        {
            if (m_audioController != null)
            {
                m_audioController.musicVolume = slider.value;
                if(m_audioController.musicVolume == 0.0f) {
                    MuteMusic(true);
                } else {
                    if(m_audioController.mute == true) {
                        MuteMusic(false);
                    }
                }
            }
        }

        public void OnSFXVolumeChanged(Slider slider)
        {
            if (m_audioController != null)
            {
                m_audioController.SFXVolume = slider.value;
            }


        }

        public void ApplySetting(SettingData settingData)
        {
            if (m_audioController != null)
            {
                m_audioController.mute = settingData.isMute;
            }
            if (m_musicSlider)
            {
                m_musicSlider.value = settingData.musicVolume;
            }
            if (m_sfxSlider)
            {
                m_sfxSlider.value = settingData.sfxVolume;
            }
        }

        public void MuteMusic(bool isMute) {
            if(m_audioController) {
                if (m_audioController.mute == true && isMute == false && m_audioController.musicVolume == 0.0f)
                {
                    m_audioController.musicVolume = 0.8f;
                    m_musicSlider.value = 0.8f;
                }
                m_audioController.mute = isMute;
                
            }
            
        }

        public void ChangeSubject(int index) {
            Question.QuestionManager.Instance.LoadSetIndex(index);
        }

    }
}
