using UnityEngine;
using UnityEngine.UI;

public class PauseCanvasHandler : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the Pause Menu Canvas
    private bool isPaused; // Indicates if the game is paused or not

    void Start()
    {
        pauseMenuUI.SetActive(false);
        setIsPaused(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }   
    }

    private void PauseGame()
    {
        setIsPaused(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pauses game time
    }

    private void Resume()
    {
        setIsPaused(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Unpauses game time
    }

    public void setIsPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }
}
