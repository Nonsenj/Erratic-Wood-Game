using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool Haskey = false;
    public bool HasSeed = false;
    public bool open = false;
    public GameObject MainInventoryUI;
    public InventoryManager inventoryManager;
    public Item currentItem;

    public void UseSelectedItem()
    {
        currentItem = inventoryManager.GetSelectedItem(true);
        if (currentItem != null)
        {
            Debug.Log("Use item" + currentItem);
        }
        else
        {
            Debug.Log("No item Use!");
        }
    }

    public void GetSelectedItem()
    {
        currentItem = inventoryManager.GetSelectedItem(false);
        if (currentItem != null)
        {
            Debug.Log("Received item" + currentItem);
        }
        else
        {
            Debug.Log("No item received!");
        }
    }

    public bool checkisseep()
    {
        GetSelectedItem();
        if (currentItem.type == ItemType.Seed) { return true; }
        return false;
    }

    void OpenInventory()
    {
        open = !open;
        MainInventoryUI.SetActive(open);
        if (open)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) OpenInventory();

    }
}
