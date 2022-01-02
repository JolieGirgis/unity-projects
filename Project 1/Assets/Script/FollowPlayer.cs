using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Public variables can be used outside of the class. Private varaibles can only be used inside the class
    public GameObject player;
    private Vector3 offset = new Vector3(0, 5, -7);
    private Vector3 offsetFP = new Vector3(0, 2.1f, 1);
    private bool firstPersonActive = false;

    public Camera rearCamera;
    public Camera driverCamera;
    public KeyCode switchKeyFP;
    public KeyCode switchKeyTP;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    // LateUpdate is called after the Update method
    void LateUpdate()
    {
        // When the key for first person camera is pressed, activate the driver camera
        if (Input.GetKeyDown(switchKeyFP))
        {
            firstPersonActive = true;
            rearCamera.gameObject.SetActive(false);
            driverCamera.gameObject.SetActive(true);
        }
        // When the key for third person camera is pressed, activate the rear camera
        else if (Input.GetKeyDown(switchKeyTP))
        {
            firstPersonActive = false;
            rearCamera.gameObject.SetActive(true);
            driverCamera.gameObject.SetActive(false);
        }
        // When the rear camera is active, place the camera behind the car
        if (firstPersonActive == false)
        {
            rearCamera.transform.position = player.transform.position + offset;
        }
        // When the driver camera is active, place the camera in front of the driver seat
        else if (firstPersonActive == true)
        {
            driverCamera.transform.position = player.transform.position + offsetFP;
            transform.rotation = player.transform.rotation;
        }
    }
}
