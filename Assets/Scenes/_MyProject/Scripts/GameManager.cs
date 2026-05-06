using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Collectibles")]
    public int totalCollectibles = 9;
    public int collectedCount = 0;

    [Header("Timer")]
    public float timeLimit = 180f; // 3 minutes
    public float timeRemaining;
    public bool gameEnded = false;

    [Header("UI")]
    public TMP_Text collectibleText;
    public TMP_Text timerText;
    public GameObject endPanel;
    public TMP_Text finalStatsText;

    [Header("Player")]
    public GameObject playerController;

    void Start()
    {
        Time.timeScale = 1f;

        timeRemaining = timeLimit;

        if (endPanel != null)
        {
            endPanel.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateCollectibleText();
        UpdateTimerText();
    }

    void Update()
{
    if (gameEnded)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        return;
    }

    timeRemaining -= Time.deltaTime;

    if (timeRemaining <= 0f)
    {
        timeRemaining = 0f;
        UpdateTimerText();
        EndLevel("Time ran out.");
        return;
    }

    UpdateTimerText();
}

    public void AddCollectible(int amount)
    {
        if (gameEnded)
        {
            return;
        }

        collectedCount += amount;
        UpdateCollectibleText();

        Debug.Log("Collected: " + collectedCount + " / " + totalCollectibles);

        if (collectedCount >= totalCollectibles)
        {
            EndLevel("Collected all Dragon Balls...\nYou ascended.");
        }
    }

    public void EndLevel(string reason)
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }

        float timeUsed = timeLimit - timeRemaining;

        if (finalStatsText != null)
        {
            finalStatsText.text =
                reason + "\n\n" +
                "Dragon Balls Collected: " + collectedCount + " / " + totalCollectibles + "\n" +
                "Time Used: " + FormatTime(timeUsed) + "\n" +
                "Time Remaining: " + FormatTime(timeRemaining);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerController != null)
        {
        playerController.SetActive(false);
        }
        
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateCollectibleText()
    {
        if (collectibleText != null)
        {
            collectibleText.text = "Dragon Balls: " + collectedCount + " / " + totalCollectibles;
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + FormatTime(timeRemaining);
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}