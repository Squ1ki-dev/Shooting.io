using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using CodeBase.Service;

namespace CodeBase.Enemy
{
    public class EnemyMoveToPlayer : MonoBehaviour
    {
        private Transform playerTransform;
        [SerializeField] private float _minDistance;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemySO _enemyConfig;
        private IGameFactory _gameFactory;
        private GameState _gameState;
        
        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _gameState = FindObjectOfType<GameState>();

            CheckGameState();

            if(_gameFactory.PlayerObject != null)
                InitializePlayerTranform();
            else
                _gameFactory.PlayerCreated += PlayerCreated;
        }

        private void Update()
        {
            CheckGameState();

            if(PlayerInitialized() && PlayerNotReached())
                _agent.destination = playerTransform.position;
        }

        private void CheckGameState()
        {
            if(_gameState.CurrentState == GameStates.Game)
            {
                this.enabled = true;
                _agent.speed = _enemyConfig.Speed;
            }
            else
            {
                this.enabled = false;
                _agent.speed = 0;
            }
        }

        private void PlayerCreated() => InitializePlayerTranform();
        private void InitializePlayerTranform() => playerTransform = _gameFactory.PlayerObject.transform;
        private bool PlayerInitialized() => playerTransform != null;
        private bool PlayerNotReached() =>
            Vector3.Distance(_agent.transform.position, playerTransform.position) >= _minDistance;
    }
}