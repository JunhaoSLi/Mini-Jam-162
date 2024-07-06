using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This Class handles the Player's Health and other UI Components. Uses the PlayerHealth Class to link with the Player Object.
 */
public class HealthBarManager : MonoBehaviour
{
    public GameObject healthBar; // Reference to PlayerUICanvas
    private List<GameObject> playerHp = new List<GameObject>(); //List to hold player health objs
    // Start is called before the first frame update
    void Start()
    {

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
