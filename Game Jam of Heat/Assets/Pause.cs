using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("i pressed esc");
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
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pauses game time
    }

    private void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Unpauses game time
    }
}
