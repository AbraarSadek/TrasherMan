using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

//mainMenuManager Class - Manages the main menu UI interactions
public class mainMenuManager : MonoBehaviour {

    [Header("Game Object Reference")]
    public GameObject controlPanel; //GameObject variable that will hold the control panel UI element

    //OnPlayButtonPress Method - Called when the play button is pressed
    public void OnPlayButtonPress() {

        Debug.Log("Play Button Pressed"); //Logs to the console that the play button was pressed
        SceneManager.LoadScene("level_One"); 

    } //End of OnPlayButtonPress Method

    //OnControlsButtonPress Method - Called when the controls button is pressed
    public void OnControlsButtonPress() {

        Debug.Log("Controls Button Pressed"); //Logs to the console that the controls button was pressed
        controlPanel.SetActive(true); //Activates the control panel UI element when the controls button is pressed

    } //End of OnControlsButtonPress Method

    //OnQuitButtonPress Method - Called when the quit button is pressed
    public void OnQuitButtonPress() {

        Debug.Log("Quit Button Pressed"); //Logs to the console that the quit button was pressed
        Application.Quit(); //Quits the application when the quit button is pressed

        //Exit Play Mode iftesting in the Unity Editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

    } //End of OnQuitButtonPress Method

    //OnBackButtonPress Method - Called when the back button is pressed
    public void OnBackButtonPress() {

       Debug.Log("Back Button Pressed"); //Logs to the console that the back button was pressed
       controlPanel.SetActive(false); //Deactivates the control panel UI element when the back button is pressed
    
    } //End of OnBackButtonPress Method

} //End of mainMenuManager Class
