using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyProjectile : MonoBehaviour
{
    private Vector2 direction;
    private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Instantiate(Vector2 direction)
    {
        this.direction = direction.normalized;
    }

    void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime * (Vector3) direction;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            collision2D.gameObject.GetComponent<PlayerInfo>().TakeDamage(1);
            Destroy(gameObject);
        }
        else if (collision2D.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

}
