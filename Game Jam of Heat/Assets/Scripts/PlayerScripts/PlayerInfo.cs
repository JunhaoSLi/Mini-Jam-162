using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This Class ties together the Player UI and the Player Game Object itself.
 * TODO: The Goal is to manage the damage of both classes and keep them in Sync. Destroy Player and lead to "Game Over" screen when Health hits 0.
 */
public class PlayerInfo : MonoBehaviour
{
    public int health = 3;
    public GameObject player;
    public GameObject playerUI;
        
    // Start is called before the first frame update
    void Start()
    {
        if (player == null || playerUI == null)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        int prevHealth = health;
        health -= damage;
        playerUI.GetComponent<HealthBarManager>().TakeDamage(prevHealth, damage);
        if (health <= 0)
        {
            player.SetActive(false);
        }
    }

}
