using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    private float speed = 15.0f;
    private float xRange = 15;
    private float zRangeForward = 5;
    private float zRangeBackward =-3;

    private PointSystem pointSystemScript;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Access all the public methods and variables of the PointSystem class
        pointSystemScript = GameObject.Find("Player").GetComponent<PointSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Keep the player in bounds in the horizontal direction
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if(transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        // Keep the player in bounds in the vertical direction
        if (transform.position.z < zRangeBackward)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRangeBackward);
        }
        if (transform.position.z > zRangeForward)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRangeForward);
        }
        // Launch a projectile from the player while the game is not over
        if (Input.GetKeyDown(KeyCode.Space) & !pointSystemScript.gameOver)
        {
            // Offset projectile from player so there's no collosion detected between them
            Vector3 cookiePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);

            // Create projectiles at an offset from the player when the spacebar is pressed
            Instantiate(projectilePrefab, cookiePosition, projectilePrefab.transform.rotation);
        }
        // While the game is not over, allow the player to move vertically and horizontally
        if (!pointSystemScript.gameOver)
        {
            // Input manager is used to know when the left or right key is pressed using GetAxis
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
        }        
    }
}
