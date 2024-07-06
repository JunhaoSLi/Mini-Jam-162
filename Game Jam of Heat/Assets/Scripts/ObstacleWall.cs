using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWall : MonoBehaviour
{
    public GameObject playerUICanvas;

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player took damage");
        }
    }
}
