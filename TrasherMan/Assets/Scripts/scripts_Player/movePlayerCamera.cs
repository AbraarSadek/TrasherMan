using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created By: Abraar Sadek
 * Created On: 2026-03-23
 * Scritp Name: movePlayerCamera
 * Purpose: The purpose of this script is to move the player camera to the position of the player camera position.
 *          This is done to ensure that the camera follows the player and is always in the correct position.
 * 
 * Last Edited By: Abraar Sadek
 * Last Edited On: 2026-03-23
 * Last Edits Made: Initial Script Creation
 */

//movePlayerCamera Class - Moves the player camera to the position of the player camera position to ensure it follows the player correctly.
public class movePlayerCamera : MonoBehaviour {

    //Public Variables
    public Transform playerCameraPosition; //Transform variable that will hold the position of the player camera

    //Update Method - Called Once Per Frame
    void Update() {

        //Sets the position of the camera to the position of the player camera
        transform.position = playerCameraPosition.position;

    } //End of Update Method

} //End of movePlayerCamera Class
