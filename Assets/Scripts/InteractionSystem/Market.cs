using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    [SerializeField] private GameObject MarketUI;
    private bool open = false;

    public bool Interact(Interactor interactor)
    {
        open = !open;
        MarketUI.SetActive(open);
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
        return true;
    }
}
