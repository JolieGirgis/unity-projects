using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 10;
    private float lowerSpawnRangeZ = 2;
    private float upperSpawnRangeZ = 15;
    private float topLimitZ = 20;
    private float sideLimitX = 20;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    private PointSystem pointSystemScript;

    // Start is called before the first frame update
    void Start()
    {
        // Access all the public methods and variables of the PointSystem class
        pointSystemScript = GameObject.Find("Player").GetComponent<PointSystem>();
        // Call the SpawnRandomAnimal method every 1.5 seconds 
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Randomly generates the animal index and spawn position
    void SpawnRandomAnimal()
    {
        // While the game is not over, spawn animals
        if (!pointSystemScript.gameOver)
        {
            // Index cannot be a float. If not assigned a value, it will be stored as 0
            // Generate a random index for the animalPrefabs array
            int animalIndex = Random.Range(0, animalPrefabs.Length);

            // Spawning positions for the animals that are coming from the top and sides of the screen  
            Vector3 spawnPosX = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, topLimitZ);
            Vector3 spawnPosZRight = new Vector3(sideLimitX, 0, Random.Range(lowerSpawnRangeZ, upperSpawnRangeZ));
            Vector3 spawnPosZLeft = new Vector3(-sideLimitX, 0, Random.Range(lowerSpawnRangeZ, upperSpawnRangeZ));

            //Rotating animals that are spawning from sides of the screen
            Quaternion spawnRotationFaceLeft = Quaternion.Euler(0, 270, 0);
            Quaternion spawnRotationFaceRight = Quaternion.Euler(0, -270, 0);

            // Creating the animals and assigning food bars
            Instantiate(animalPrefabs[animalIndex], spawnPosX, animalPrefabs[animalIndex].transform.rotation);
            Instantiate(animalPrefabs[animalIndex], spawnPosZRight, spawnRotationFaceLeft);
            Instantiate(animalPrefabs[animalIndex], spawnPosZLeft, spawnRotationFaceRight);
        }
    }
}
