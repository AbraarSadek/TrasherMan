using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Controls Game Over scene UI
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button nextLevelButton;

    private void Start() {

        // Unlocks the cursor so it can move freely
        Cursor.lockState = CursorLockMode.None;

        // Makes the hardware cursor visible again
        Cursor.visible = true;

        int score = ScoreManager.Instance.GetScore();

        finalScoreText.text = $"Score: {score}";

        if (GameManager.Instance.didPlayerLose)
        {
            resultText.text = "YOU LOSE";
        }
        else
        {
            resultText.text = "YOU WIN";
        }

        HandleNextLevelButton();
    }

    private void HandleNextLevelButton()
    {
        int lastLevel = GameManager.Instance.lastPlayedLevelIndex;

        if (GameManager.Instance.didPlayerLose || lastLevel >= 3)
        {
            nextLevelButton.interactable = false;
        }
        else
        {
            nextLevelButton.interactable = true;
        }
    }

    public void OnRetry()
    {
        GameManager.Instance.RetryLevel();
    }

    public void OnNextLevel()
    {
        GameManager.Instance.NextLevel();
    }

    public void OnHome()
    {
        GameManager.Instance.GoHome();
    }
}