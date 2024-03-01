using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int MaxHeath;
    [SerializeField] private int MaxEnergy;
    public int currentHeath {  get; private set; }

    private void Start()
    {
        SetMaxHeath(MaxHeath);
    }
    protected virtual void SetMaxHeath(int health)
    {
        currentHeath = health;
    }

    public virtual void RecieveDamage(int damage)
    {
        currentHeath -= damage;
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
