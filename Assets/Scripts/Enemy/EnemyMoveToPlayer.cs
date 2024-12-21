using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveToPlayer : MonoBehaviour
{
    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    private IGameFactory _gameFactory;
    
    [SerializeField] private float _minDistance;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();

        if(_gameFactory.PlayerObject != null)
            InitializePlayerTranform();
        else
            _gameFactory.PlayerCreated += PlayerCreated;
    }

    private void Update()
    {
        if(PlayerInitialized() && PlayerNotReached())
            navMeshAgent.SetDestination(playerTransform.position);
    }

    private void PlayerCreated() => InitializePlayerTranform();
    private void InitializePlayerTranform() => playerTransform = _gameFactory.PlayerObject.transform;
    private bool PlayerInitialized() => playerTransform != null;
    private bool PlayerNotReached() =>
        Vector3.Distance(navMeshAgent.transform.position, playerTransform.position) >= _minDistance;
}
