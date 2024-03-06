using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        
        var inventory = interactor.GetComponent<Inventory>();
        if (inventory == null) return false;

        if (inventory.Haskey)
        {
            Debug.Log("Opening chest");
            return true;
        }

        Debug.Log("No key found!");
        return false;
    }
}
