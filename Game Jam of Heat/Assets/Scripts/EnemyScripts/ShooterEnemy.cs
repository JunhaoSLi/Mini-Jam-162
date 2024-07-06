using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    private float timeSinceLastAttack = 0;
    private float attackCooldownSec = 2;
    public GameObject projectilePrefab;
    public GameObject player; // right now, just set the reference to the player via the UI, this will probably need to change

    // Start is called before the first frame update
    void Start()
    {

    }

    // called every frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack > attackCooldownSec && player.activeSelf)
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
