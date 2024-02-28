using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Predator : Animal
{
    [Header("Predator Variables")]
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float maxChaseTime = 10f;
    [SerializeField] private int biteDamage = 4;
    [SerializeField] private float biteCooldown = 1;

    public Collider[] colliders = new Collider[10];



    private Prey currentChaseTarget;
    private PlayerMoment currentPlayerTarget;


    protected override void CheckChaseConditions()
    {
        if (currentChaseTarget)
            return;
        
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, detectionRange, colliders);

        for (int i = 0; i < numColliders; i++)
        {
            PlayerMoment target = colliders[i].GetComponent<PlayerMoment>();

            if (target != null)
            {
                Debug.Log("Player");
                ChasePlayer(target);
                return;
            }

        }

        for (int i=0; i < numColliders; i++)
        {
            Prey prey = colliders[i].GetComponent<Prey>();

            if (prey != null)
            {
                Debug.Log("Sheep");
                StartChase(prey);
                return;
            }
        }

        currentChaseTarget = null;
    }

    private void StartChase(Prey prey)
    {
        currentChaseTarget = prey;
        SetState(AnimalState.Chase);
    }

    private void ChasePlayer(PlayerMoment playerTarget)
    {
        currentPlayerTarget = playerTarget;
        SetState(AnimalState.Chase);
    }

    protected override void HandleChaseState()
    {
        if (currentChaseTarget != null)
        {
            currentChaseTarget.AlertPrey(this);
            StartCoroutine(ChasePrey());
        }
        else if(currentPlayerTarget != null)
        {
            StartCoroutine(ChasePlayer());
        }
        else
        {
            SetState(AnimalState.Idle);
        }
    }

    private IEnumerator ChasePlayer()
    {
        float startTime = Time.time;
        while (true)
        {
            if (Time.time - startTime >= maxChaseTime || currentPlayerTarget == null)
            {
                StopChase();
                yield break;
            }

            SetState(AnimalState.Chase);
            agent.SetDestination(currentPlayerTarget.transform.position);
            yield return null;
        }
    }

    private IEnumerator ChasePrey()
    {
        float startTime = Time.time;

        while (currentChaseTarget != null && Vector3.Distance(transform.position, currentChaseTarget.transform.position) > agent.stoppingDistance)
        {
            if (Time.time - startTime >= maxChaseTime || currentChaseTarget == null)
            {
                StopChase();
                yield break;
            }

            SetState(AnimalState.Chase);
            agent.SetDestination(currentChaseTarget.transform.position);
            yield return null;
        }

        if (currentChaseTarget)
            currentChaseTarget.RecieveDamage(biteDamage);

        yield return new WaitForSeconds(biteCooldown);

        currentChaseTarget = null;
        HandleChaseState();
        CheckChaseConditions();
    }

    private void StopChase()
    {
        agent.ResetPath();
        currentChaseTarget = null;
        currentPlayerTarget = null;
        SetState(AnimalState.Moving);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderDistance);
    }

}
