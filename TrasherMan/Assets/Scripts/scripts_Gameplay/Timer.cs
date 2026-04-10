using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Timer Class - Manages the countdown timer, updates the UI, and handles game over conditions when time runs out
public class Timer : MonoBehaviour {

    [Header("Game Object Reference")]
    [SerializeField] TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    [SerializeField] float remainingTime = 0f;

    //Update Method - Is called once per frame
    void Update() {

        //Else-If Statement - Check if remaining time is greater than 0 to continue counting down
        if (remainingTime > 0) {

            remainingTime -= Time.deltaTime;

        } else if (remainingTime <= 0) {

            remainingTime = 0f;
            timerText.color = Color.red;
            Debug.Log("GAME OVER (Time ran out)");
            GameManager.Instance.TriggerGameOver(false); // player did NOT lose by score

        } //End of Else-If Statement

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    } //End of Update method

} //End of Timer class
