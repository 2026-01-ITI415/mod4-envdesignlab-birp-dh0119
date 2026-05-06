using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Collectibles")]
    public int totalCollectibles = 9;
    public int collectedCount = 0;

    [Header("Timer")]
    public float timeElapsed = 0f;
    public bool gameEnded = false;

    [Header("UI")]
    public TMP_Text collectibleText;
    public TMP_Text timerText;
    public GameObject endPanel;
    public TMP_Text finalStatsText;

    void Start()
    {
        Time.timeScale = 1f;

        if (endPanel != null)
        {
            endPanel.SetActive(false);
        }

        UpdateCollectibleText();
        UpdateTimerText();
    }

    void Update()
    {
        if (!gameEnded)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerText();
        }
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
            EndLevel("All DragonBall collected!");
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

        if (finalStatsText != null)
        {
            finalStatsText.text =
                reason + "\n\n" +
                "DragonBall Collected: " + collectedCount + " / " + totalCollectibles + "\n" +
                "Time: " + FormatTime(timeElapsed);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    void UpdateCollectibleText()
    {
        if (collectibleText != null)
        {
            collectibleText.text = "DrangonBall: " + collectedCount + " / " + totalCollectibles;
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + FormatTime(timeElapsed);
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}