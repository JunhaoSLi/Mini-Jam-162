using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Items are currently coded across the project as a one-time unlock with no item consumption, no stacking
public enum Item
{
    VolcanoOrb = 0,
    YellowRings = 1
}

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            collision2D.gameObject.GetComponent<PlayerInfo>().GetNewItem(item);
            Destroy(gameObject);
        }
    }
}
