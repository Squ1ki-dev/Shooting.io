using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;

namespace CodeBase.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Vector3 boxSize;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private PlayerStatsSO playerConfig;

        private static int _layerMask;
        private float _attackTimer;
        private float _attackCooldown;
        private bool _isAttacking;
        private bool _attackIsActive;
        
        private Collider[] _hits = new Collider[3];

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            UpdateCooldown();
            UpdateAttack();

            if(CanAttack())
                StartAttack();
        }

        private void UpdateCooldown()
        {
            if (_attackCooldown > 0)
            {
                _attackCooldown -= Time.deltaTime;
                _attackCooldown = Mathf.Max(_attackCooldown, 0); // Clamp to 0
            }
        }

        private void StartAttack()
        {
            _isAttacking = true;
            _attackTimer = playerConfig.AttackSpeed;

            Debug.Log("Player ATTACK started!");
            PerformAttack();
        }

        private void PerformAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                var enemy = _hits[i].transform.parent;

                if (enemy.TryGetComponent<IHealth>(out IHealth health))
                    health.TakeDamage(playerConfig.Damage);

                if (enemy.TryGetComponent<Rigidbody>(out Rigidbody enemyRigidbody))
                {
                    if (Random.Range(0f, 1f) <= 0.3f)
                    {
                        Vector3 pushDirection = (_hits[i].transform.position - attackPoint.position).normalized;
                        enemyRigidbody.AddForce(pushDirection * playerConfig.PushStrength);
                        health.TakeDamage(playerConfig.Damage);
                    }
                }
                
                if (PlayerPrefs.GetInt(Constants.VibrationParameter) == 1)
                    HapticFeedback.LightFeedback();

                Debug.Log($"Hit {_hits[i].name}");
            }
        }

        private void UpdateAttack()
        {
            if (_isAttacking)
            {
                _attackTimer -= Time.deltaTime;

                if (_attackTimer <= 0)
                    EndAttack();
            }
        }

        private void EndAttack()
        {
            _isAttacking = false;
            _attackCooldown = playerConfig.AttackCooldownDuration;
            Debug.Log("Player ATTACK ended!");
        }

        public void EnableAttack() => _attackIsActive = true;
        public void DisableAttack() => _attackIsActive = false;

        private bool CooldownIsUp() => _attackCooldown <= 0;
        private bool CanAttack() => _attackIsActive && !_isAttacking && CooldownIsUp();

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(attackPoint.position, playerConfig.AttackRange, _hits, _layerMask);

        private void OnDrawGizmos()
        {
            if (attackPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(attackPoint.position, playerConfig.AttackRange);
            }
        }
    }
}