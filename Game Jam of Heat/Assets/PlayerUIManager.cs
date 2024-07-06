using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This Class handles the Player's Health and other UI Components. Uses the PlayerHealth Class to link with the Player Object.
 */
public class PlayerUIManager : MonoBehaviour
{
    public GameObject playerUICanvas; // Reference to PlayerUICanvas
    private List<GameObject> playerHp = new List<GameObject>(); //List to hold player health
    // Start is called before the first frame update
    void Start()
    {
        if (playerUICanvas == null)
        {
            Debug.LogError("PlayerUICanvas reference is not set in the inspector.");
            return;
        }
        // Find and add hp objects to the list. 
        // TODO: Create HP programmatically instead of having a predefined list
        for (int i = 1; i <= 3; i++)
        {
            GameObject hp = playerUICanvas.transform.Find($"hp{i}")?.gameObject;
            if (hp != null)
            {
                playerHp.Add(hp);
            }
            else
            {
                Debug.LogError($"hp{i} not found as a child of PlayerUICanvas.");
            }
        }

    }

    // Make the player take damage
    private void TakeDamage(int damage)
    {
        for (int i = 0; i < playerHp.Count; i++)
        {
            playerHp[i].SetActive(false); // Currently make the hp disappear on taking damage
        }
    }
}
