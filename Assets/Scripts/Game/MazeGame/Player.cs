using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGame {

    public class Player : MonoBehaviour
    {
        [SerializeField] private GameController mazeController;
        // Start is called before the first frame update
        private void OnTriggerEnter2D(Collider2D col) {
            if(col.gameObject.tag == "Obstacle") {
                mazeController.ShowQuestion(true);
                mazeController.SetActiveQuestion(col.gameObject);
            }
        }
    }

}