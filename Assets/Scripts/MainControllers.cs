using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainControllers : Singleton<MainControllers>
{
    public void Awake() {
        // MakeSingleton(false);
        
    }

    public void StartGame() {
        Debug.Log("start");
    }

     public void Settings() {
        Debug.Log("setting");
    }

    public void ExitGame() {
            Debug.Log("Quit");
            Application.Quit();
        }
}
