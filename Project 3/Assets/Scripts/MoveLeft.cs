using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 25;
    public float dashSpeed = 35;
    private PlayerController playerControllerScript;
    private float leftBound = -15;
    // Start is called before the first frame update
    void Start()
    {
        // Access variables and methods from the playerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // While the game is not over, move objects to the left
        if(playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            // When the player has activated dash, make objects move faster
            if (playerControllerScript.dashActive)
            {
                transform.Translate(Vector3.left * Time.deltaTime * dashSpeed);
            }
        }

        // When obstacles get past the player's screen, destroy them
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        
    }
}
