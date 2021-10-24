using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

// Modify on editor tab Open scene 
public class SceneChangeTab 
{
    [MenuItem("Open Scene/Menu")]
    public static void OpenSceneMenu()
    {
        SceneChangeTab.OpenScene("StartMenu");
    }

    [MenuItem("Open Scene/Games Setting")]
    public static void OpenSceneSettings()
    {
        SceneChangeTab.OpenScene("GameSettings");
    }

    [MenuItem("Open Scene/MiniGame/PlaneGame")]
    public static void OpenScenePlaneGame()
    {
        SceneChangeTab.OpenScene("MiniGame/PlaneGame");
    }
    [MenuItem("Open Scene/MiniGame/MazeGame")]
    public static void OpenSceneMazeGame()
    {
        SceneChangeTab.OpenScene("MiniGame/MazeGame");
    }

    

    public static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity");
        }
    }
}