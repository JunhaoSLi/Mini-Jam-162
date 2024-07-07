using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 6.25f;
    Vector2 movementInput;
    Vector2 facingDirection = Vector2.right;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;

    private PlayerInfo playerInfo;

    public GameObject meleeAttackPrefab;
    private float meleeAttackCooldownSec = 0.6f;
    private float timeSinceLastMeleeAttack = 0;
    private bool meleeOnCooldown = false;

    private float itemsCooldownSec = 1f;
    private float timeSinceLastItemsUse = 0;
    private bool itemsOnCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInfo = GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastMeleeAttack += Time.deltaTime;
        timeSinceLastItemsUse += Time.deltaTime;
        if (timeSinceLastMeleeAttack > meleeAttackCooldownSec)
        {
            meleeOnCooldown = false;
        }
        if (timeSinceLastItemsUse > itemsCooldownSec)
        {
            itemsOnCooldown = false;
        }

        if (Input.GetMouseButtonDown(0) && !meleeOnCooldown)
        {
            // Send out a melee attack
            Vector2 meleeDirection = facingDirection.normalized;
            Quaternion rotationTowardsMovement = Quaternion.LookRotation(Vector3.forward, meleeDirection);
            Vector3 meleePosition = transform.position + ((Vector3) meleeDirection * 1.5f);
            Instantiate(meleeAttackPrefab, meleePosition, rotationTowardsMovement, transform);
            timeSinceLastMeleeAttack = 0;
            meleeOnCooldown = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && !itemsOnCooldown && playerInfo.playerItems.Count != 0)
        {
            // Use items
            if (playerInfo.playerItems.Contains(Item.VolcanoOrb))
            {
                // Destroy all nearby enemies
                // This is just a random effect I picked to test items and has flaws like killing enemies in other rooms
                LayerMask enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
                Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, 256, enemyLayerMask);
                foreach (Collider2D collider in enemyColliders)
                {
                    Destroy(collider.gameObject);
                }
            }
            if (playerInfo.playerItems.Contains(Item.YellowRings))
            {
                // Turn player yellow, another random effect I picked to test items
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            timeSinceLastItemsUse = 0;
            itemsOnCooldown = true;
        }
    }

    // called every 0.02 seconds
    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            // Calculate the distance to move
            float distance = moveSpeed * Time.fixedDeltaTime;
            Vector2 move = movementInput * distance;
            // If movement input is not 0, try to move
            // Check for potential collisions
            int count = rb.Cast(
            movementInput, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine on which layer a collision can occur on
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            distance // The amount to cast equal to the movement distance
        );

            if (count == 0)
            {
                // Move the player
                rb.MovePosition(rb.position + move);
            }
            else
            // TODO: Currently, when the player hits a wall, if you hold the button against the wall, the player will not move no matter what other input comes.
            // We want the player to be able to move without releasing the button against the wall
            {
                // Move the player up to the point of collision with a small offset to avoid getting stuck
                RaycastHit2D hit = castCollisions[0];
                float offset = 0.01f; // Small offset to prevent sticking
                Vector2 newPosition = rb.position + move.normalized * (hit.distance - offset);

                rb.MovePosition(newPosition);
            }
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
        if (movementInput != Vector2.zero)
        {
            facingDirection = movementInput;
        }
    }

}
