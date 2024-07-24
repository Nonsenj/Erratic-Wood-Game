using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public PlayerManager coin;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
    }

    public void SellItem(int id)
    {
        if (coin.currentCoin >= 0)
        {
            inventoryManager.AddItem(itemsToPickup[id]);
            switch (id)
            {
                case 0: coin.UseCoin(20); break;
                case 1: coin.UseCoin(10); break;
                case 2: coin.UseCoin(50); break;
            }
        }

    }

    public void GetSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Received item " + receivedItem);
        }
        else
        {
            Debug.Log("No item received!");
        }
    }

    public void UseSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Use item " + receivedItem);
        }
        else
        {
            Debug.Log("No item Use!");
        }
    }
}
