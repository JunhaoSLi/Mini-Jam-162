using System;
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
        // Assumes hp blocks are children of healthBar, all having names like hp1, hp2, etc
        for (int i = 0; i < healthBar.transform.childCount; i++)
        {
            GameObject healthBarChild = healthBar.transform.GetChild(i).gameObject;
            if (healthBarChild.name.StartsWith("hp"))
            {
                playerHp.Add(healthBarChild);
            }
        }

        // Sort the hp blocks in ascending order
        playerHp.Sort((a, b) => GetHpIndex(a).CompareTo(GetHpIndex(b)));
    }

    // Make the player take damage
    public void TakeDamage(int origHealth, int damage)
    {
        int origHealthIdx = origHealth - 1;
        int newHealthIdx = origHealth - damage - 1;
        newHealthIdx = Math.Max(newHealthIdx, -1); // Bounds check for if we take a lot of damage at once

        // Start from the highest health block and count down
        for (int i = origHealthIdx; i > newHealthIdx; i--)
        {
            playerHp[i].SetActive(false); // Currently make the hp disappear on taking damage
        }
    }


    private int GetHpIndex(GameObject hpObject)
    {
        try
        {
            return Int32.Parse(hpObject.name.Substring(2));
        }
        catch
        {
            Debug.LogError("Could not get HP Index for HealthBarManager");
            return 0;
        }
    }
}
