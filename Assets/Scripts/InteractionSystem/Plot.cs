using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plot : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject plant;
    [SerializeField] Item Item;
    [SerializeField] InventoryManager inventoryManger;
    private Plant currentPlant;
    private bool isEmpty = true;
    private string _prompt = "[E] Plant a seed";
    public string InteractionPrompt => _prompt;

    
    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventory>();
        PlayerManager player = interactor.GetComponent<PlayerManager>();

        Debug.Log(inventoryManger);

        if (inventory.checkisseep() && isEmpty)
        {
            inventory.UseSelectedItem();
            player.UseEnergy(10);
            _prompt = "Tomato";
            GameObject instantiatedPlant = Instantiate(plant);
            instantiatedPlant.transform.parent = gameObject.transform;
            Vector3 plantPosition = Vector3.zero;
            plantPosition.y = 0;
            instantiatedPlant.transform.localPosition = plantPosition;

            currentPlant = instantiatedPlant.GetComponent<Plant>();
            currentPlant.dayOfPlaning = TimeManager.instance.dayInGame;
            isEmpty = false;
            return true;
        }

        if (currentPlant != null)
        {
            if (currentPlant.haveProduce)
            {
                inventoryManger.AddItem(Item);
                return true;
            }
            else
            {
                return false;
            }
        }

        Debug.Log("No have a seed");
        return false;
    }
}
