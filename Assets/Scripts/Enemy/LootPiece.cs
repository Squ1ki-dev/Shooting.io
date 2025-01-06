using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Player;
using CodeBase.Service;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour, ICollectible
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private EnemySO enemySO;

        private bool _picked;
        private bool _hasTarget;
        private Vector3 _targetPos;
        private Rigidbody _rb;
        public static event Action<int> OnLootPicked;

        private void Awake() => _rb = GetComponent<Rigidbody>();
        
        public void Collect()
        {
            if (_picked)
                return;

            _picked = true;
            OnLootPicked?.Invoke(enemySO.XPValue);
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if(_hasTarget)
            {
                Vector3 targetDir = (_targetPos - transform.position).normalized;
                _rb.velocity = new Vector3(targetDir.x, targetDir.y, targetDir.z) * _moveSpeed;
            }
        }

        public void SetTarget(Vector3 position)
        {
            _targetPos = position;
            _hasTarget = true;
        }
    }
}