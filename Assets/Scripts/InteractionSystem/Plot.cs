using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour, IInteractable
{
    private string _prompt = "[E] Plant a seed";
    public string InteractionPrompt => _prompt;

    
    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventory>();
        if (inventory == null) return false;

        if (inventory.HasSeed)
        {
            _prompt = "Tomato";
            Debug.Log("Plant");
            return true;
        }

        Debug.Log("No have a seed");
        return false;
    }
}
