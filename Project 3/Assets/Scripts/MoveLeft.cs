using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float normalSpeed = 25;
    public float speed;
    public float dashSpeed = 30;
    private PlayerController playerControllerScript;
    private float leftBound = -15;
    // Start is called before the first frame update
    void Start()
    {
        speed = normalSpeed;
        // Access variables and methods from the playerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // While the game is not over, move objects to the left
        if (playerControllerScript.gameOver == false && !playerControllerScript.startScene)
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

        // When player is dashing, increase the player's speed
        if (playerControllerScript.dashActive)
        {
            speed = dashSpeed;
        }
        else if (!playerControllerScript.dashActive)
        {
            speed = normalSpeed;
        }

    }
}
