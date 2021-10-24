using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelSetting
{
    public class LevelSetting : MonoBehaviour
    {
        public static GameModule game;
        private List<GameModule> m_games;

        [SerializeField] private Dropdown m_gameSelect;
        
        private void Awake() {
            m_games = new List<GameModule>();
            m_games.Add(new MazeGame());
            m_games.Add(new PlaneGame());

            RenderDropdown();

        }

        private void RenderDropdown() {
            if(m_gameSelect) {
                List<string> options = new List<string> ();
                foreach (var option in m_games) {
                    options.Add(option.name);
                }
                m_gameSelect.ClearOptions();
                m_gameSelect.AddOptions(options);

                m_gameSelect.value = 0;
                game = m_games[0];
            }
        }

        public void OnGameChange(int index) {
            game = m_games[index];
        }

        public void Enter() {
            SceneController.LoadSceneByName(game.scene);
        }
    }
    
}

