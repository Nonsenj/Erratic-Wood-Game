using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBox : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventory>();
        var Player = interactor.GetComponent<PlayerManager>();

        inventory.GetSelectedItem();

        if (inventory.currentItem != null)
        {
            inventory.UseSelectedItem();
            Player.AddCoin(20);
            _prompt = "Sell Item";
            return true;
        }

        _prompt = "No have a yield";
        return false;
    }
}
