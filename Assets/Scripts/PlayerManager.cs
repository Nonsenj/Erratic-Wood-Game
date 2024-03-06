using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private HeathBar healthBar;
    [SerializeField] private EnergyBar energyBar;

    [Header("Attributes")]
    [SerializeField] private int MaxHeath;
    [SerializeField] private int MaxEnergy;

    public int currentHeath {  get; private set; }
    public int currentEnergy { get; private set; }



    private void Start()
    {
        SetMaxHeath(MaxHeath);
        SetMaxEnergy(MaxEnergy);
        healthBar.SetMaxHeath(MaxHeath);
        energyBar.SetMaxEnergy(MaxEnergy);
    }

    protected virtual void SetMaxEnergy(int Energy)
    {
        currentEnergy = Energy;
    }

    protected virtual void SetMaxHeath(int health)
    {
        currentHeath = health;
    }

    public virtual void UseEnergy(int Energy)
    {

        currentEnergy -= 10;
        energyBar.SetEnergy(currentEnergy);
        if (currentEnergy <= 0)
        {
            Debug.Log("EE");
        }
    }

    public virtual void RecieveDamage(int damage)
    {
        currentHeath -= damage;
        healthBar.SetHeath(currentHeath);
        if (currentHeath <= 0) {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Death");
        StopAllCoroutines();
        Destroy(gameObject);
    }

}
