using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TrashDescriptionPanelManager Class - Manages the trash description panel and its related UI elements
public class TrashDescriptionPanelManager : MonoBehaviour {

    [Header("Game Object Reference")]
    public GameObject trashDescriptionPanel;
    public GameObject trashItemsDescriptionOne;
    public GameObject trashItemsDescriptionTwo;

    //Start Method - Called once at that start
    void Start() {
        
        if (trashDescriptionPanel == null) {
            Debug.LogError("Trash Description Panel reference is not set in the inspector.");
        } 

        if (!trashDescriptionPanel.activeInHierarchy) {
            trashDescriptionPanel.SetActive(true);
        }

        if (trashItemsDescriptionOne == null || trashItemsDescriptionTwo == null) {
            Debug.LogError("Trash Items Description ONE or TWO references are not set in the inspector.");
        } else {
            trashItemsDescriptionOne.SetActive(true);
            trashItemsDescriptionTwo.SetActive(false);
            Time.timeScale = 0f; // Pause the game when the trash description panel is active
        }

    } //End of Start Method

    //OnCloseButtonPress Method - Called when the close button is pressed
    public void OnCloseButtonPress() {
        trashDescriptionPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f; // Resume the game when the trash description panel is closed
    }

    //OnBackButtonPress Method - Called when the back button is pressed
    public void OnBackButtonPress() {
        if (!trashItemsDescriptionOne.activeSelf) {
            trashItemsDescriptionOne.SetActive(true);
            trashItemsDescriptionTwo.SetActive(false);
        }
    }

    //OnNextButtonPress Method - Called when the next button is pressed
    public void OnNextButtonPress() {
        if (!trashItemsDescriptionTwo.activeSelf) {
            trashItemsDescriptionOne.SetActive(false);
            trashItemsDescriptionTwo.SetActive(true);
        } 
    }

} //End of TrashDescriptionPanelManager Class