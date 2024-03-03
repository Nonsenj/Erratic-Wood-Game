using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_test : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject targat;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    // Update is called once per frame
    void Update()
    {
        Seek(targat.transform.position);
    }
}
