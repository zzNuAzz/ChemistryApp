using System.Collections;
using System.Collections.Generic;
using PlaneGame;
using UnityEngine;
using UnityEngine.UI;

namespace PlaneGame
{
    public class Plane : MonoBehaviour
    {
        public Cloud currentColisionCloud;
        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Obstacle")
            {
                GUI.InGameInterface gui = GameObject.Find("/GameUI").GetComponent<GUI.InGameInterface>();
                gui.ShowQuestionPanel(true, true);

                // test
                Cloud target = col.gameObject.GetComponent<Cloud>();
                currentColisionCloud = target;
            }
        }
    }
}
