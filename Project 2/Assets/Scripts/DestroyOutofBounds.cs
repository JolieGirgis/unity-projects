using UnityEngine;

public class DestroyOutofBounds : MonoBehaviour
{
    private float lowerBoundZ = -10.0f;
    private float upperBoundZ = 20.0f;
    private float xRange = 35;

    private PointSystem pointSystemScript;

    // Start is called before the first frame update
    void Start()
    {
        // Access all the public methods and variables of the PointSystem class
        pointSystemScript = GameObject.Find("Player").GetComponent<PointSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // If an animal goes past the player's view in the game, remove that object and decrease the player's lives
        if (transform.position.z < lowerBoundZ)
        {
            if (pointSystemScript.gameOver == false)
            {
                pointSystemScript.DecreaseLife();
            }

            Destroy(gameObject);
        }
        else if(transform.position.z > upperBoundZ)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x > xRange || transform.position.x < -xRange)
        {
            if (pointSystemScript.gameOver == false)
            {
                pointSystemScript.DecreaseLife();
            }
            Destroy(gameObject);
        }
    }
}
