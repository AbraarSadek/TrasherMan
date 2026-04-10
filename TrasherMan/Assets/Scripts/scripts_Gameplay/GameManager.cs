using UnityEngine;
using UnityEngine.SceneManagement;

// Controls game flow across all scenes
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool didPlayerLose = false;
    public int lastPlayedLevelIndex;

    private bool isGameOver = false;

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

    // Called when game ends
    public void TriggerGameOver(bool playerLost)
    {
        if (isGameOver) return;

        isGameOver = true;
        didPlayerLose = playerLost;

        lastPlayedLevelIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(4);
    }

    // Called from Main Menu
    public void StartGame()
    {
        ScoreManager.Instance.ResetScore();
        isGameOver = false;

        SceneManager.LoadScene(1); // Level 1
    }

    public void RetryLevel()
    {
        isGameOver = false;
        ScoreManager.Instance.ResetScore();

        SceneManager.LoadScene(lastPlayedLevelIndex);
    }

    public void NextLevel()
    {
        isGameOver = false;

        int nextLevel = lastPlayedLevelIndex + 1;
        SceneManager.LoadScene(nextLevel);
    }

    public void GoHome()
    {
        isGameOver = false;
        ScoreManager.Instance.ResetScore();

        SceneManager.LoadScene(0); // Main Menu
    }
}