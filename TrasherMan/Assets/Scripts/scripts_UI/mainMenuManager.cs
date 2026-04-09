using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

//mainMenuManager Class - Manages the main menu UI interactions
public class mainMenuManager : MonoBehaviour {

    public void OnPlayButtonPress() {

        Debug.Log("Play Button Pressed"); //Logs to the console that the play button was pressed
        SceneManager.LoadScene("level_One"); 

    } //End of OnPlayButtonPress Method

    //OnQuitButtonPress Method - Called when the quit button is pressed
    public void OnQuitButtonPress() {

        Debug.Log("Quit Button Pressed"); //Logs to the console that the quit button was pressed
        Application.Quit(); //Quits the application when the quit button is pressed

        //Exit Play Mode iftesting in the Unity Editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

    } //End of OnQuitButtonPress Method

} //End of mainMenuManager Class
