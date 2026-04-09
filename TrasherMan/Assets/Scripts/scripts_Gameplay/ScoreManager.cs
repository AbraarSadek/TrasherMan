using UnityEngine;
using TMPro; //Required for TextMeshPro UI

//ScoreManager Class - Manages the player's score, updates the UI, and handles game over conditions
public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance; //Singleton instance so other scripts can easily access ScoreManager

    private int score = 25; //Current player score

    [Header("UI Object References")]
    [SerializeField] private TextMeshProUGUI scoreText; //Reference to the TextMeshProUGUI component for displaying the score

    //Awake Method - Runs before Start Method
    private void Awake() {

        //If-Statement - Ensure only one instance of ScoreManager exists (Singleton pattern)
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject); //Destroy this instance if another instance exists (avoid duplicates)

        } //End of If-Else Statement

    } //End of Awake method

    //Start Method - Runs on the first frame
    private void Start() {
        UpdateScoreUI(); //Call UpdateScoreUI to initialize the score display at the start of the game
    }

    //AddPoints Method - Adds or subtracts points from the player's score
    public void AddPoints(int points) {

        score += points; //Modify the score

        //If-Statement - Check if score has reached zero or below
        if (score <= 0) {

            score = 0; //Clamp score to 0 so it doesn't go negative

            UpdateScoreUI(); //Call UpdateScoreUI to show the final score before game over

            //Trigger game over logic (for now just a log)
            Debug.Log("GAME OVER");
            //TODO: Replace with actual Game Over screen / restart system

            return; //Stop further execution

        } //End of If-Statement

        UpdateScoreUI(); //Call UpdateScoreUI to refresh the score display

    } //End of AddPoints method 

    //UpdateScoreUI Method - Updates the on-screen score text
    private void UpdateScoreUI() {
        scoreText.text = $"{score}"; //Display score in a clean format
    }

} //End of ScoreManager class