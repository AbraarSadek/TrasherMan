using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//SceneChecker Class - Checks the current active scene and performs actions based on it
public class SceneChecker : MonoBehaviour {

    //Start Method - Called once at that start
    void Start() {
        
        if (SceneManager.GetActiveScene().name == "level_Three") {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    } //End of Start Method

} //End of SceneChecker Class
