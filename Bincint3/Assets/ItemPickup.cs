using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public static Dictionary<Vector2Int, Item> Items = new Dictionary<Vector2Int, Item>();

    Item currentItem = null;
    Vector2 direction;
    Transform player;

    private void Awake()
    {
        player = GetComponent<Transform>();
    }

    private void Update()
    {
        if (currentItem != null)
        {
            currentItem.transform.position = player.position;
        }
    }

    public bool TryPickupItem()
    {
        Vector2Int checkPosition = GetCheckPosition();
        
        if (Items.TryGetValue(checkPosition, out Item pickedUpItem))
        {
            Items.Remove(checkPosition);
            currentItem = pickedUpItem;
            return true;
        }

        return false;
    }

    public bool TryPutdownItem()
    {
        Vector2Int checkPosition = GetCheckPosition();

        if (Items.ContainsKey(checkPosition))
        {
            print("Grid occupied: " + checkPosition);
            return false;
        }

        Items[checkPosition] = currentItem;
        currentItem = null;
        return true;
    }

    private Vector2Int GetCheckPosition()
    {
        Vector2 checkPositionF = new Vector2(player.position.x, player.position.y) + direction;
        Vector2Int checkPosition = Vector2Int.FloorToInt(checkPositionF);
        return checkPosition;
    }
}
