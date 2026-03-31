using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created By: Abraar Sadek
 * Created On: 2026-03-23
 * Scritp Name: playerMovement
 * Purpose: The purpose of this script is to manage the player's movement based on input and physics, 
 *          allowing the player to move, jump, and control their speed while grounded or in the air.
 * 
 * Last Edited By: Abraar Sadek
 * Last Edited On: 2026-03-23
 * Last Edits Made: Initial Script Creation
 */

//playerMovement Class - Manages the player's movement based on input and physics.
public class playerMovement : MonoBehaviour {

    [Header("Player Movement Settings")]
    public float moveSpeed = 2.5f; //Float variable that will hold the speed at which the player moves.
    public float groundDrag = 2f; //Float variable that will hold the amount of drag applied to the player when grounded.
    public float jumpForce = 5f; //Float variable that will hold the players jumping force.
    public float jumpCooldown = 0.5f; //Float variable that will hold the jump cool down timer.
    public float airMultiplier = 0.25f; //Float variable that will hold the multiplier for when the player is in the air.
    bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode playerJumpKey = KeyCode.Space; //KeyCode variable that will hold the key used for jumping.

    [Header("Ground Check")]
    public float playerHeight = 2f; //Float variable that will hold the height of the player for ground checking.
    public LayerMask whatIsGround; //LayerMask variable that will specify which layers are considered ground for the player.
    bool isGrounded; //Boolean variable that will indicate whether the player is currently grounded or not.

    public Transform playerOrientation; //Transform variable that will hold the player's orientation.

    float horizontalInput; //Float variable that will hold the player's horizontal input.
    float verticalInput; //Float variable that will hold the player's vertical input.

    Vector3 playerMovementDirection; //Vector3 variable that will hold the direction in which the player will move.

    Rigidbody playerRigidbody; //Rigidbody variable that will hold the player's Rigidbody component.

    //Start Method - Called Once When the Script is First Enabled
    void Start() {

        playerRigidbody = GetComponent<Rigidbody>(); //Gets the player's Rigidbody component and assigns it to the playerRigidbody variable.
        playerRigidbody.freezeRotation = true; //Freezes the player's rotation to prevent it from tipping over when moving.

    } //End of Start Method

    //Update Method - Called Once Per Frame
    void Update()
    {

        //Checks if the player is grounded by casting a ray downwards from the player's position.
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        //Method Calls
        MyInput(); //Calls to get the player's input.
        PlayerSpeedControl(); //Call to control players spped.

        //If-Statement - Checks if the player is grounded and applies the appropriate drag to the player's Rigidbody.
        if (isGrounded) {
            playerRigidbody.linearDamping = groundDrag; //Applies the ground drag to the player's Rigidbody when grounded.
        } else {
            playerRigidbody.linearDamping = 0; //Removes the drag from the player's Rigidbody when not grounded.
        } //End of If-Statement

    }//End of Update Method

    //FixedUpdate Method - Called at a Fixed Interval (Used for Physics Calculations)
    void FixedUpdate() {

        // Align the player's yaw with the playerOrientation so the player object always faces camera yaw.
        // Use Rigidbody.MoveRotation so rotation stays compatible with physics.
        if (playerOrientation != null) {
            Quaternion targetRotation = Quaternion.Euler(0f, playerOrientation.eulerAngles.y, 0f);
            playerRigidbody.MoveRotation(targetRotation);
        }

        MovePlayer(); //Calls the MovePlayer method to move the player based on the input.
    } //End of FixedUpdate Method

    void MyInput() {

        //Gets the player's input for both the horizontal and vertical axes using the Input.GetAxisRaw method, 
            //and assigns it to the horizontalInput and verticalInput variables respectively.
        horizontalInput = Input.GetAxisRaw("Horizontal"); 
        verticalInput = Input.GetAxisRaw("Vertical");

        //If-Statement - Checks if the player is grounded, if the jump key is pressed, and if the player is ready to jump.
        if (Input.GetKey(playerJumpKey) && readyToJump && isGrounded) {

            readyToJump = false; //Sets the readyToJump boolean to false to prevent multiple jumps.
            JumpPlayer(); //Calls the JumpPlayer method to make the player jump.    
            Invoke(nameof(ResetPlayerJump), jumpCooldown); //Invokes the ResetPlayerJump method after the jumpCooldown time has passed to reset the player's ability to jump.
        
        } //End of If-Statement

    } //End of MyInput Method

    //MovePlayer Method - Handles the player's movement by applying forces to the player's Rigidbody,
        //based on the input and whether the player is grounded or in the air.
    private void MovePlayer() {

        //Calculates the direction in which the player will move based on the player's orientation and input.
        playerMovementDirection = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;

        //If-Statement - Checks if the player is grounded and applies the appropriate force to the player's Rigidbody based on the movement direction and speed.
        if (isGrounded) {
            //Applies a force to the player's Rigidbody in the direction of movement,
                //multiplied by the moveSpeed and a factor of 10 for better responsiveness.
            playerRigidbody.AddForce(playerMovementDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        } else if (!isGrounded) {
            //Applies a force to the player's Rigidbody in the direction of movement,
                //multiplied by the moveSpeed, airMultiplier, and a factor of 10 for better responsiveness while in the air.
            playerRigidbody.AddForce(playerMovementDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        } //End of If-Statement

    }//End of MovePlayer Method

    //PlayerSpeedControl Method - Controls the player's speed to prevent it from exceeding the set moveSpeed.
    private void PlayerSpeedControl() {

        //Gets the player's velocity on the X and Z axes, ignoring the Y axis.
        Vector3 flatVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);

    } //End of PlayerSpeedControl Method

    //JumpPlayer Method - Handles the player's jumping mechanics, applying a force upwards when the player jumps.
    private void JumpPlayer() {

        //Resets the player's vertical velocity to 0 before applying the jump force.
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);

        //Applies an impulse force upwards to make the player jump.
        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    } //End of JumpPlayer Method

    //ResetPlayerJump Method - Resets the player's ability to jump after a cooldown period.
    private void ResetPlayerJump() {
        readyToJump = true; //Sets the readyToJump boolean to true, allowing the player to jump again after the cooldown.
    } //End of ResetPlayerJump Method

} //End of playerMovement Class
