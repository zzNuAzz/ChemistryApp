using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // btn in startMenu
    public void LoadGameScene() {
        Debug.Log("start game");
        SceneManager.LoadScene("GameSettings");
    }

    // btn in game
    public void LoadMainMenuScene() {
        SceneManager.LoadScene("StartMenu");
    }

    public static void LoadSceneByName(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void ResetScene() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    // btn in startMenu
    public void ExitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
