using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected int enemyHealth = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
