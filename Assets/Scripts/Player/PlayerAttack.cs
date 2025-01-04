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
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private Transform _attackKnifePoint;
        [SerializeField] private GameObject _knifePrefab;
        [SerializeField] private PlayerStatsSO _playerConfig;
        [SerializeField] private GameObject _attackSlashVFX;

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
            _attackTimer = _playerConfig.AttackSpeed;

            Debug.Log("Player ATTACK started!");
            PerformAttack();
            if (Random.Range(0f, 1f) <= 0.3f)
                PerformKnivesAttack();
        }

        private void PerformAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                var enemy = _hits[i].transform.parent;

                if (enemy.TryGetComponent<IHealth>(out IHealth health))
                {
                    StartCoroutine(ActivateAttackVFX());
                    health.TakeDamage(_playerConfig.Damage);
                }

                if (enemy.TryGetComponent<Rigidbody>(out Rigidbody enemyRigidbody))
                {
                    if (Random.Range(0f, 1f) <= 0.3f)
                    {
                        Vector3 pushDirection = (_hits[i].transform.position - _attackPoint.position).normalized;
                        enemyRigidbody.AddForce(pushDirection * _playerConfig.PushStrength);
                    }
                }
                
                if (PlayerPrefs.GetInt(Constants.VibrationParameter) == 1)
                    HapticFeedback.LightFeedback();

                Debug.Log($"Hit {_hits[i].name}");
            }
        }

        private void PerformKnivesAttack()
        {
            int knivesToShoot = Mathf.Min(_playerConfig.AmountOfKnives, _hits.Length);

            for (int i = 0; i < knivesToShoot; i++)
            {
                var enemyTransform = _hits[i]?.transform?.parent;
                if (enemyTransform == null)
                    continue;

                Vector3 direction = (enemyTransform.position - _attackKnifePoint.position).normalized;

                GameObject knife = ObjectPool.SpawnObject(_knifePrefab, _attackKnifePoint.position, Quaternion.LookRotation(direction));
                Debug.Log($"Knife #{i + 1} launched at {enemyTransform.name}.");

                if (enemyTransform.TryGetComponent<IHealth>(out IHealth health))
                {
                    health.TakeDamage(_playerConfig.KnivesDamage);
                    ObjectPool.ReturnToPool(knife);
                }
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
            _attackCooldown = _playerConfig.AttackCooldownDuration;
            Debug.Log("Player ATTACK ended!");
        }

        public void EnableAttack() => _attackIsActive = true;
        public void DisableAttack() => _attackIsActive = false;

        private bool CooldownIsUp() => _attackCooldown <= 0;
        private bool CanAttack() => _attackIsActive && !_isAttacking && CooldownIsUp();

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(_attackPoint.position, _playerConfig.AttackRange, _hits, _layerMask);

        private int KnifeHit() =>
            Physics.OverlapSphereNonAlloc(_attackKnifePoint.position, 10f, _hits, _layerMask);

        private IEnumerator ActivateAttackVFX()
        {
            Quaternion attackVFXRotation = Quaternion.Euler(-90, 0, 0);
            GameObject _attackVFX = ObjectPool.SpawnObject(_attackSlashVFX, _attackPoint.position, attackVFXRotation);
            yield return new WaitForSeconds(1.5f);
            ObjectPool.ReturnToPool(_attackVFX);
        }

        private void OnDrawGizmos()
        {
            if (_attackPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_attackPoint.position, _playerConfig.AttackRange);
            }
        }
    }
}