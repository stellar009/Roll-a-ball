using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    // Text element that displays the score during gameplay
    public TextMeshProUGUI scoreTMP;
    // Text element that displays the countdown timer during gameplay
    public TextMeshProUGUI timerTMP;
    // Panel shown when the game ends
    public GameObject gameOverPanel;

    [Header("UI Settings")]
    // How often (in seconds) the UI updates
    [Range(0.1f, 0.5f)] public float updateTime = 0.2f;

    // Cached wait instruction to avoid creating a new one every loop
    private WaitForSeconds delay;

    // Reference to the TextMeshPro inside the game over panel
    private TextMeshProUGUI m_GameOverPanelTMP;

    // Start is called once before the first frame
    void Start()
    {
        // Create the reusable delay
        delay = new WaitForSeconds(updateTime);

        // Start the UI update loop
        StartCoroutine(UpdateUI());
    }

    // Coroutine that refreshes UI text at a fixed interval
    IEnumerator UpdateUI()
    {
        while(true)
        {
            // If the game is over, show the game over panel and stop updating
            if (GameManager.instance.isGameOver)
            {
                GameManager.instance.EnablePanel(gameOverPanel, GameManager.instance.isGameOver);
                GameOverText();
                StopAllCoroutines();
            }

            // Update the in-game score display
            GameManager.instance.UICounter(scoreTMP);

            // Update the in-game timer display
            GameManager.instance.GameOverTimer(timerTMP);

            // Wait before updating again
            yield return delay;
        }
    }

    // Fills the game over panel with the final score and time remaining
    private void GameOverText()
    {
        // Grab the TextMeshPro component inside the panel
        m_GameOverPanelTMP = gameOverPanel.GetComponentInChildren<TextMeshProUGUI>();

        // Display the final score and remaining time with colors
        m_GameOverPanelTMP.text = $"<color=#696eff>Score: {GameManager.instance.m_Count} </color>\n " +
            $"<color=#f8acff>Time Remaining: {GameManager.instance.remainingTime:F0}</color>";
    }
}