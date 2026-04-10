using UnityEngine;
using TMPro;

// Connects the level's UI to the ScoreManager
public class LevelUIConnector : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start() {
        if (ScoreManager.Instance != null) {
            ScoreManager.Instance.SetScoreText(scoreText);
        }
    }

}