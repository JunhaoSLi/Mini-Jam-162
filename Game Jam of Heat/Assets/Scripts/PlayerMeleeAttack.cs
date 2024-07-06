using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private float timeSinceCreation = 0;
    private float hitboxActiveTime = 0.2f;
    private float timeToStayOnScreen = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        timeSinceCreation += Time.fixedDeltaTime;
        if (timeSinceCreation > timeToStayOnScreen)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (timeSinceCreation < hitboxActiveTime && collision2D.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision2D.gameObject.GetComponent<Enemy>().TakeDamage(1);
        }
    }
}
