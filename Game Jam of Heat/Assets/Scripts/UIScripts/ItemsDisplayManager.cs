using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDisplayManager : MonoBehaviour
{
    // We probably don't need separate prefabs for each item icon (nor for each item pickup)
    // Just doing this to for easiness
    [SerializeField] GameObject volcanoOrbIconPrefab;
    [SerializeField] GameObject yellowRingsIconPrefab;

    // Key = item enum, Value = ui icon game object, made this a dict in case we need to know the mapping in the future
    Dictionary<Item, GameObject> currentlyDisplayedItems = new Dictionary<Item, GameObject>();

    public void DisplayNewItem(Item newItem)
    {
        if (!currentlyDisplayedItems.ContainsKey(newItem))
        {
            GameObject iconToCreate = getIconGameObjectForItem(newItem);
            GameObject itemIcon = Instantiate(iconToCreate, transform);

            Vector3 iconPosition = getNextIconPosition();
            itemIcon.GetComponent<RectTransform>().anchoredPosition = iconPosition;

            currentlyDisplayedItems.Add(newItem, itemIcon);
        }
    }

    private GameObject getIconGameObjectForItem(Item newItem)
    {
        switch (newItem)
        {
            case Item.VolcanoOrb:
                return volcanoOrbIconPrefab;
            case Item.YellowRings:
            default:
                return yellowRingsIconPrefab;
                
        }
    }

    private Vector3 getNextIconPosition()
    {
        return new Vector3(280 + 45 * currentlyDisplayedItems.Count, 200, 0);
    }
}
