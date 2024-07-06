using UnityEngine;
using UnityEngine.UI;

public class ExitBtnHandler : MonoBehaviour
{
    public Button exitButton; // Reference to the Exit Button

    // Start is called before the first frame update
    void Start()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(CloseGame);
        }
        else
        {
            Debug.LogError("Exit button reference is not set in the inspector.");
        }
    }

    // Closes the game
    void CloseGame()
    {
        //  This directive ensures that if the code is running in the Unity Editor, it will stop the play mode using UnityEditor.EditorApplication.isPlaying = false;
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
