using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created By: Abraar Sadek
 * Created On: 2026-03-23
 * Scritp Name: playerCamera
 * Purpose: The purpose of this script is to control the player's camera movement based on mouse input. 
 *          It allows the player to look around in the game world by moving the mouse, 
 *          and it also ensures that the camera follows the player's orientation correctly.
 * 
 * Last Edited By: Abraar Sadek
 * Last Edited On: 2026-03-23
 * Last Edits Made: Initial Script Creation
 */

//playerCamera Class - Manages the player's camera movement based on mouse movement and player orientation.
public class playerCamera : MonoBehaviour {

    //Public Variables - That Will Hold the Sensitivity of the Mouse Movement and the Player's Orientation
    [Header("Mouse Sensitivity")]
    public float sensitivityX;
    public float sensitivityY;

    //Public Variable - That Will Hold the Player's Orientation Transform
    [Header("Player Orientation")]
    public Transform playerOrientation;

    //Private Variables - That Will Hold the Current Rotation of the Camera on the X and Y Axes
    private float xCameraRotation;
    private float yCameraRotation;

    //Start Method - Called Once When the Script is First Enabled
    void Start() {

        //Locks the cursor to the center of the screen and makes it invisible
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;

    } //End of Start Method


    //Update Method - Called Once Per Frame
    void Update() {

        //Gets the mouse input for both the X and Y axes,
            //multiple it by the time delta and sensitivity to ensure smooth camera movement.
        float mouseInputX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseInputY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivityY;

        //Updates the camera rotation based on the mouse input.
            //The Y rotation is updated with the X mouse input,
            //and the X rotation is updated with the Y mouse input.
        yCameraRotation += mouseInputX;
        xCameraRotation -= mouseInputY;

        xCameraRotation = Mathf.Clamp(xCameraRotation, -90f, 90f); //Clamps the X rotation to 90 degrees

        //Applies the calculated rotations to the camera and player orientation.
        transform.rotation = Quaternion.Euler(xCameraRotation, yCameraRotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yCameraRotation, 0);
    } //End of Update Method

} //End of playerCamera Class
