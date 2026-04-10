using UnityEngine;
using TMPro;

// Manages score and persists across all scenes
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 25;

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points)
    {
        score += points;

        if (score <= 0)
        {
            score = 0;
            UpdateScoreUI();

            Debug.Log("GAME OVER (Score reached 0)");

            GameManager.Instance.TriggerGameOver(true);
            return;
        }

        UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    public void SetScoreText(TextMeshProUGUI newText)
    {
        scoreText = newText;
        UpdateScoreUI();
        Debug.Log("ScoreText connected!");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{score}";
        }
    }
}