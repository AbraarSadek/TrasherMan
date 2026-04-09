using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [Header("Game Object Reference")]
    [SerializeField] TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    [SerializeField] float remainingTime = 0f;

    //Update Method - Is called once per frame
    void Update() {
        
        //Else-If Statement - 
        if (remainingTime > 0) {

            remainingTime -= Time.deltaTime;

        } else if (remainingTime <= 0) {

            remainingTime = 0f;
            timerText.color = Color.red;
            //TODO: Call gameOver method here from GameManager script

        }

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

}
