using UnityEngine;


public class DetectCollisions : MonoBehaviour
{
    private PointSystem pointSystemScript;

    private void Start()
    {
        // Access all the public methods and variables of the PointSystem class
        pointSystemScript = GameObject.Find("Player").GetComponent<PointSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // When the player collides with an animal, decrease their lives.
        if (other.gameObject.name == "Player")
        {
            if (pointSystemScript.playerLives != 0)
            {
                pointSystemScript.DecreaseLife();
            }
            Destroy(gameObject);
        }
        // When the projectile collides with an animal, increase the animal's food bar
        else if (other.gameObject.tag == "Animal")
        {
            other.GetComponent<AnimalFoodBar>().IncreaseFoodBar();
        }
        // Destroy projectile
        else
        {
            Destroy(other.gameObject);
        }
    }
}

