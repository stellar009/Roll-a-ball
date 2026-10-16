using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton reference so other scripts can access this easily
    public static GameManager instance;

    // Maximum time allowed to collect all gems
    public float maxGemCollectionTime = 120f;

    // Read-only flag to check if the game has ended
    public bool isGameOver { get; private set; }

    // Remaining time shown in the UI
    public float remainingTime { get; private set; }

    // Gem score counter
    public int m_Count { get; private set; }

    // Tracks how much time has passed since the game started
    private float m_CollectionTime;

    // Set true if this scene is the main menu
    [SerializeField] private bool onMainMenu;

    private void Awake()
    {
        // Set up the singleton instance
        if(!instance)
        {
            instance = this;
        }

        // Show/hide cursor depending on whether we're on the main menu
        ActivateCursor(onMainMenu);
        isGameOver = false;
    }

    private void Update()
    {
        // Increase elapsed time every frame
        m_CollectionTime += Time.deltaTime;

        // End the game if time runs out
        if(m_CollectionTime > maxGemCollectionTime)
        {
            GameOver();
        }
    }

    // Toggles cursor visibility and lock state
    public void ActivateCursor(bool enable)
    {
        Cursor.visible = enable;
        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
    }

    // Updates the score text on the UI
    public void UICounter(TextMeshProUGUI counterTmp)
    {
        counterTmp.text = string.Empty;
        counterTmp.text = $"Score: {m_Count}";
    }

    // Increments the gem counter
    public void Counter()
    {
        m_Count++;
    }

    // Updates the timer text on the UI
    public void GameOverTimer(TextMeshProUGUI timerTmp)
    {
        timerTmp.text = string.Empty;
        remainingTime = maxGemCollectionTime - m_CollectionTime;
        timerTmp.text = $"{remainingTime:F0} Seconds remaining";
    }

    // Shows/hides a panel and toggles the cursor accordingly
    public void EnablePanel(GameObject panel, bool enable)
    {
        panel.SetActive(enable);
        ActivateCursor(enable);
    }

    // Ends the game — disables player input and sets the game over flag
    public void GameOver()
    {
        FindObjectOfType<GameInputManager>().EnablePlayerControls(false);
        isGameOver = true;
    }

    // Loads the Main Menu scene
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Reloads the current scene to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quits the application
    public void ExitGame()
    {
        Application.Quit();
    }

    // Loads the next scene (used to start the game from the menu)
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}