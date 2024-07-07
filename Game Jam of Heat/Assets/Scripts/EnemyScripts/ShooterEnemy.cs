using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    private float timeSinceLastAttack = 0;
    private float attackCooldownSec = 2.5f;
    public GameObject projectilePrefab;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Below line is not good for performance probably, have rooms 
        player = GameObject.FindWithTag("Player");
    }

    // called every frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack > attackCooldownSec && player != null && player.activeSelf)
        {
            // Get direction towards player
            Vector2 vectorToPlayer = player.transform.position - transform.position;

            // Create projectile towards player
            Quaternion projectileRotation = Quaternion.LookRotation(Vector3.forward, vectorToPlayer);
            GameObject projectile = Instantiate(projectilePrefab, transform.position, projectileRotation);
            ShooterEnemyProjectile shooterProjectile = projectile.GetComponent<ShooterEnemyProjectile>();
            shooterProjectile.Instantiate(vectorToPlayer);
            
            timeSinceLastAttack = 0;
        }
    }
}
