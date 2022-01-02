using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 40.0f;
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
        // Move the game object forward automatically while the game is not over
        if (!pointSystemScript.gameOver)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
