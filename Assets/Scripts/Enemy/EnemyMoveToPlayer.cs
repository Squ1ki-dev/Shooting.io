using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using CodeBase.Service;

public class EnemyMoveToPlayer : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private float _minDistance;
    [SerializeField] private NavMeshAgent _agent;
    private IGameFactory _gameFactory;
    private GameState _gameState;
    
    private void Start()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();
        _gameState = FindObjectOfType<GameState>();

        if(_gameFactory.PlayerObject != null)
            InitializePlayerTranform();
        else
            _gameFactory.PlayerCreated += PlayerCreated;
    }

    private void Update()
    {
        if(_gameState.CurrentState != GameStates.Game)
        {
            this.enabled = false;
            _agent.speed = 0;
        }
        else
            this.enabled = true;

        if(PlayerInitialized() && PlayerNotReached())
            _agent.destination = playerTransform.position;
    }

    private void PlayerCreated() => InitializePlayerTranform();
    private void InitializePlayerTranform() => playerTransform = _gameFactory.PlayerObject.transform;
    private bool PlayerInitialized() => playerTransform != null;
    private bool PlayerNotReached() =>
        Vector3.Distance(_agent.transform.position, playerTransform.position) >= _minDistance;
}
