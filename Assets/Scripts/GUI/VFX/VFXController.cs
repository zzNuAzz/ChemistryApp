using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class VFXController : MonoBehaviour
    {
        [SerializeField] ItemVFX yesDuckSign;
        [SerializeField] ItemVFX noDuckSign;
        
        public void ShowVFX(string name) {
            var audioController = GameObject.FindObjectOfType<Audio.AudioController>();
            if(name == "yesDuckSign") {
                Instantiate(yesDuckSign, gameObject.GetComponent<Transform>());
                if(audioController) {
                    audioController.PlayOneShot(audioController.ting);
                }
                return;
            }

            if(name == "noDuckSign") {
                Instantiate(noDuckSign, gameObject.GetComponent<Transform>());
                if(audioController) {
                    audioController.PlayOneShot(audioController.wrongs);
                }
                return;
            }
        }


            
    }

}
