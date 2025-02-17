using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;

        void Update()
        {
            if(ShouldMove())
                _animator.Move(_agent.velocity.magnitude);
            else
                _animator.StopMoving();
        }

        private bool ShouldMove() => _agent.velocity.magnitude > MinimalVelocity;
    }
}