using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonHandler : MonoBehaviour
{
    public Button continueButton; // Reference to the Continue Button
    public GameObject pauseMenuUI; // Reference to the Pause Menu UI
    public GameObject eventSystem; // Reference to the Event System UI
    private PauseCanvasHandler pauseScript; // Reference to the Pause script in the Event System

    void Start()
    {
        if (continueButton == null || pauseMenuUI == null || eventSystem == null)
        {
            return;
        }

        // Get the Pause component from the eventSystem GameObject
        pauseScript = eventSystem.GetComponent<PauseCanvasHandler>();

        // Add listener to the Continue Button
        continueButton.onClick.AddListener(UnpauseGame);
    }

    void UnpauseGame()
    {
        pauseScript.setIsPaused(false);
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Unpause the game
    }
}
