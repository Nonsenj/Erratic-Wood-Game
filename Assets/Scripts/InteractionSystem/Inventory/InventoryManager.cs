using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 4;
    public Slot[] Slot;
    public GameObject InventoryItemPrefab;

    int selectedSlot = -1;

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 10)
            {
                ChangeSelectedSlot(number - 1);
            }

        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if(selectedSlot >= 0) Slot[selectedSlot].Deselect();
        Slot[newValue].Selected();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        //Check if any slot has the same item with count lower than max
        for (int i = 0; i < Slot.Length; i++)
        {
            Slot slot = Slot[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        //Find any empty slot
        for (int i=0; i< Slot.Length; i++)
        {
            Slot slot = Slot[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, Slot slot)
    {
        GameObject newItemGo = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        Slot slot = Slot[selectedSlot];
        InventoryItem ItemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (ItemInSlot != null)
        {
            Item Item = ItemInSlot.item;
            if (use == true)
            {
                ItemInSlot.count--;
                if (ItemInSlot.count <= 0)
                {
                    Destroy(ItemInSlot.gameObject);
                }
                else
                {
                    ItemInSlot.RefreshCount();
                }

            }

            return Item;
        }

        return null;
    }

}
