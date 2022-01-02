using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public int playerLives = 3;
    public int score = 0;
    private static bool startOfGame = true;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        // Display the score and number of lives at the start of the game
        if (startOfGame == true)
        {
            Debug.Log("Lives: " + playerLives);
            Debug.Log("Score: " + score);
            startOfGame = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void DecreaseLife()
    {
        // While the players still has lives, decrease the lives and display the remaining amount of lives
        if(playerLives > 0)
        {
            playerLives--;
            Debug.Log("Lives: " + playerLives);
        }
        // When the player has no more lives, display game over
        if (playerLives == 0)
        {
            Debug.Log("Game Over! No more lives.");
            gameOver = true;
        }
    }
    public void IncreaseScore()
    {
        // Increase the score and display it
        score++;
        Debug.Log("Score: " + score);
    }
}
