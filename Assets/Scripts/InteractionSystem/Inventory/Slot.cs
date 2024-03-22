using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color SelectedColor;
    public Color notSelectedColor;

    private void Awake()
    {
        Deselect();
    }

    public void Selected()
    {
        image.color = SelectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem InventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventoryItem.parentAfterDrag = transform;

        }
    }

}
