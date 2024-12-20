using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform destination;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetDestination();
    }

    private void SetDestination()
    {
        Vector3 targetVector = destination.position;
        navMeshAgent.SetDestination(targetVector);
    }

    private void Update()
    {
        SetDestination();
    }
}
