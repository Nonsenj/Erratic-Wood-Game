using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum AnimalState
{
    Idle,
    Moving,
    Chase,

}
[RequireComponent(typeof(NavMeshAgent))]
public class Animal : MonoBehaviour
{
    [Header("Wander")]
    [SerializeField] protected float wanderDistance = 50f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float maxWalkTime = 6f;

    [Header("Idle")]
    [SerializeField] private float idleTime = 5f;

    [Header("Chase")]
    [SerializeField] private float runSpeed = 8f;

    [Header("Attributes")]
    [SerializeField] private int health = 10;

    protected NavMeshAgent agent;
    protected AnimalState currentState = AnimalState.Idle;

    void Start()
    {
        InitialseAnimal();
    }

    protected virtual void InitialseAnimal()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;

        currentState = AnimalState.Idle;
        UpdateState();
    }

    protected virtual void UpdateState()
    {
        switch (currentState)
        {
            case AnimalState.Idle:
                HandleIdleState();
                break;
            case AnimalState.Moving:
                HandleMoveState();
                break;
            case AnimalState.Chase:
                HandleChaseState();
                break;
        }
    }

    protected Vector3 GetRandomNavMeshPosition(Vector3 origin, float distance)
    {
        for (int i=0; i<5; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * distance;
            randomDirection += origin;
            NavMeshHit navMeshHit;

            if (NavMesh.SamplePosition(randomDirection, out navMeshHit, distance, NavMesh.AllAreas))
            {
                return navMeshHit.position;
            }

        }

        return origin;
    }

    protected virtual void CheckChaseConditions()
    {

    }

    protected virtual void HandleChaseState()
    {
        StopAllCoroutines();
    }

    protected virtual void HandleIdleState()
    {
        StartCoroutine(WaitToMove());
    }

    private IEnumerator WaitToMove()
    {
        float waitTime = Random.Range(idleTime/2,idleTime*2);
        yield return new WaitForSeconds(waitTime);

        Vector3 randomDestination = GetRandomNavMeshPosition(transform.position, wanderDistance);
        agent.SetDestination(randomDestination);
        SetState(AnimalState.Moving);

    }

    protected virtual void HandleMoveState()
    {
        StartCoroutine(WaitToReachDestination());
    }

    private IEnumerator WaitToReachDestination()
    {
        float startTime = Time.time;

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance && agent.isActiveAndEnabled)
        {
            if (Time.time - startTime >= maxWalkTime)
            {
                agent.ResetPath();
                SetState(AnimalState.Idle);
                yield break;
            }

            CheckChaseConditions();
            yield return null;
        }
        SetState(AnimalState.Idle);
    }

    protected void SetState(AnimalState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        OnStateChanged(newState);
    }

    protected virtual void OnStateChanged(AnimalState newState)
    {
        if (newState == AnimalState.Moving)
            agent.speed = walkSpeed;
        if (newState == AnimalState.Chase)
            agent.speed = runSpeed;
        UpdateState();
    }

    public virtual void RecieveDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
