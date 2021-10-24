using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelSetting
{
    public class GameModule
    {
        public string name;
        public string scene;
        public bool isTimeCountDown;
        public float timer;
        
    }
    
    public class MazeGame : GameModule {
        public MazeGame() {
            name = "Mê cung";
            scene = "MazeGame";
            isTimeCountDown = true;
            timer = 301f;
        }
    }

    public class PlaneGame : GameModule {
        public PlaneGame() {
            name = "Máy bay";
            scene = "PlaneGame";
            isTimeCountDown = false;
        }
    }
}