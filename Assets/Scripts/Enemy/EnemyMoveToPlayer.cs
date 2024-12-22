using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyMoveToPlayer : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private NavMeshAgent _agent;
    private IGameFactory _gameFactory;
    
    [SerializeField] private float _minDistance;

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
            _agent.destination = playerTransform.position;
    }

    private void PlayerCreated() => InitializePlayerTranform();
    private void InitializePlayerTranform() => playerTransform = _gameFactory.PlayerObject.transform;
    private bool PlayerInitialized() => playerTransform != null;
    private bool PlayerNotReached() =>
        Vector3.Distance(_agent.transform.position, playerTransform.position) >= _minDistance;
}
