using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This is an example of a trap/obstacle. Will report X damage to the PlayerHealth class.
 */
public class ObstacleWall : MonoBehaviour
{
    public GameObject playerHealth;

    void Start()
    {
        if (playerHealth == null)
        {
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player took damage");
        }
    }
}
