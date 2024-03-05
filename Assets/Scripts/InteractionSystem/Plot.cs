using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plot : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject plant;
    private Plant currentPlant;
    private bool isEmpty = true;
    private string _prompt = "[E] Plant a seed";
    public string InteractionPrompt => _prompt;

    
    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventory>();
        if (inventory == null) return false;

        if (inventory.HasSeed && isEmpty)
        {
            _prompt = "Tomato";
            GameObject instantiatedPlant = Instantiate(plant);
            instantiatedPlant.transform.parent = gameObject.transform;
            Vector3 plantPosition = Vector3.zero;
            plantPosition.y = 0;
            instantiatedPlant.transform.localPosition = plantPosition;

            currentPlant = instantiatedPlant.GetComponent<Plant>();
            currentPlant.dayOfPlaning = TimeManager.instance.dayInGame;
            isEmpty = false;
            Debug.Log("Plant");
            return true;
        }

        
        Debug.Log("No have a seed");
        return false;
    }
}
