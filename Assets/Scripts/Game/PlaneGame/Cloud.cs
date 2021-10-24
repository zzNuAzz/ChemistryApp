using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlaneGame
{
    
public class Cloud : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    [SerializeField]
    private bool isMovement = false;

    private Vector3 initPosition;

    private void Awake() {
        initPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovement)
        {
            transform.position =
                transform.position +
                new Vector3(-1 * movementSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isMovement = false;
            transform.position = initPosition;
        }
    }

    public void Reset()
    {
        isMovement = true;
        transform.position = initPosition;
    }
}

}
