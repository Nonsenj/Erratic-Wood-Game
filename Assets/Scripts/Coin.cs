using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int amount;
    [SerializeField] private TextMeshProUGUI textcoin;

    public void SetAmount(int amount)
    {
        this.amount = amount;
        textcoin.text = "$" + amount.ToString();
    }
    
}
