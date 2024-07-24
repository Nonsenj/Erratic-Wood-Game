using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private GameObject seed;
    [SerializeField] private GameObject youngPlant;
    [SerializeField] private GameObject maturePlant;

    [SerializeField] private List<GameObject> plantProduceSpawns;

    [SerializeField] private GameObject producePrefab;

    [SerializeField] public int dayOfPlaning;
    [SerializeField] private int plantAge = 0;

    [SerializeField] private int ageForYoung;
    [SerializeField] private int ageForMature;
    [SerializeField] private int ageForFirstProduceBatch;

    [SerializeField] private int daysForNewProduce;
    [SerializeField] private int daysRemainingForNewProduceCounter;

    [SerializeField] bool isOneTimeHarvest;
    [SerializeField] bool isWatered;

    public bool haveProduce = false;

    private void OnEnable()
    {
        TimeManager.instance.OnDayPass.AddListener(DayPass);
    }

    private void OnDisable()
    {
        TimeManager.instance.OnDayPass.RemoveListener(DayPass);
    }

    private void DayPass()
    {
        if (isWatered)
        {
            plantAge++;
        }

        CheakGrowth();

        CheckProduce();
    }

    private void CheckProduce()
    {
        if (plantAge == ageForFirstProduceBatch)
        {
            GenerateProduceForEmptySpawns();
            haveProduce = true;
        }

        if (plantAge > ageForFirstProduceBatch)
        {
            
            if (daysRemainingForNewProduceCounter == 0)
            {
                GenerateProduceForEmptySpawns();
                haveProduce = true;
                daysRemainingForNewProduceCounter = daysForNewProduce;
            }
            else
            {
                daysRemainingForNewProduceCounter--;
            }
        }
        

    }

    public void CropYield()
    {
        foreach (GameObject item in plantProduceSpawns)
        {
            foreach (Transform child in item.transform) { 
                Destroy(child.gameObject);
            }
        }

    }

    private void GenerateProduceForEmptySpawns()
    {
        foreach (GameObject spawn in plantProduceSpawns)
        {
            if (spawn.transform.childCount == 0)
            {
                GameObject produce = Instantiate(producePrefab);

                produce.transform.parent = spawn.transform;

                Vector3 producePosition = Vector3.zero;
                producePosition.y = 0f;
                produce.transform.localPosition = producePosition;
            }
        }
        
    }

    private void CheakGrowth()
    {
        seed.SetActive(plantAge < ageForYoung);
        youngPlant.SetActive(plantAge >= ageForYoung && plantAge < ageForMature);
        maturePlant.SetActive(plantAge >= ageForMature);
    }
}
