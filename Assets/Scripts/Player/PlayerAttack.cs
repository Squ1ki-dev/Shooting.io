using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;

namespace CodeBase.Player
{
    [RequireComponent(typeof(PlayerAnimator), typeof(CharacterController))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private Transform _attackKnifePoint;
        [SerializeField] private Transform _magicalAttackPoint;
        [SerializeField] private Transform _vfxPoint;
        [SerializeField] private GameObject _knifePrefab;
        [SerializeField] private GameObject _magicalOrbPrefab;
        [SerializeField] private PlayerStatsSO _playerConfig;
        [SerializeField] private GameObject _attackSlashVFX;
        [SerializeField] private PlayerAnimator _playerAnimator;

        private GameObject _vfxObject;

        private GameState _gameState;

        private static int _layerMask;
        private float _attackTimer;
        private float _attackCooldown;
        private float _knifeAttackCooldown = 7f;
        private bool _isAttacking;
        private bool _attackIsActive;

        private Collider[] _hits = new Collider[3];

        private void Awake()
        {
            _gameState = FindObjectOfType<GameState>();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Start() => CheckForGameState();

        private void Update()
        {
            CheckForGameState();
            UpdateCooldown();
            UpdateKnifeCooldown();
            UpdateAttack();

            if (CanAttack() && !_playerAnimator.IsAttacking)
                StartAttack();
        }

        private void CheckForGameState()
        {
            if (_gameState.CurrentState == GameStates.Finish || _gameState.CurrentState == GameStates.Lose)
                this.enabled = false;
            else
                this.enabled = true;
        }

        private void OnAttack() 
        {
            if(_playerConfig.IsSwordsman)
                _vfxObject = ObjectPool.SpawnObject(_attackSlashVFX, _vfxPoint.position, _vfxPoint.rotation);
        }
        private void OnAttackEnded() 
        {
            if(_playerConfig.IsSwordsman)
                ObjectPool.ReturnToPool(_vfxObject);
        }

        private void UpdateCooldown()
        {
            if (_attackCooldown > 0)
            {
                _attackCooldown -= Time.deltaTime;
                _attackCooldown = Mathf.Max(_attackCooldown, 0);
            }
        }

        private void UpdateKnifeCooldown()
        {
            if (_knifeAttackCooldown > 0)
            {
                _knifeAttackCooldown -= Time.deltaTime;
                _knifeAttackCooldown = Mathf.Max(_knifeAttackCooldown, 0);
            }
        }

        private void StartAttack()
        {
            _isAttacking = true;
            if(_playerConfig.IsSwordsman)
            {
                _attackTimer = _playerConfig.AttackSpeed;
                PerformAttack();
            }
            else
            {
                _attackTimer = _playerConfig.MagicalAttackSpeed;
                PerformMagicAttack();
            }

            Debug.Log("Player ATTACK started!");

            if (_knifeAttackCooldown <= 0 && Random.Range(0f, 1f) <= 0.3f)
                PerformKnivesAttack();
        }

        private void PerformMagicAttack()
        {
            for (int i = 0; i < MagicalHit(); i++)
            {
                var enemy = _hits[i]?.transform.parent;
                if (enemy == null)
                    continue;

                Vector3 direction = (enemy.position - _attackKnifePoint.position).normalized;

                GameObject magicOrb = ObjectPool.SpawnObject(_magicalOrbPrefab, _attackKnifePoint.position, Quaternion.LookRotation(direction));
                Debug.Log($"Magic Orb #{i + 1} launched at {enemy.name}.");
                ActivateAttackVibration();
            }
        }

        private void PerformAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                var enemy = _hits[i].transform.parent;

                if (enemy.TryGetComponent<IHealth>(out IHealth health))
                {
                    _playerAnimator.PlayAttack();
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
                
                ActivateAttackVibration();

                Debug.Log($"Hit {_hits[i].name}");
            }
        }

        private void PerformKnivesAttack()
        {
            int knivesToShoot = Mathf.Min(_playerConfig.AmountOfKnives, _hits.Length);

            for (int i = 0; i < knivesToShoot; i++)
            {
                var enemy = _hits[i]?.transform.parent;
                if (enemy == null)
                    continue;

                Vector3 direction = (enemy.position - _attackKnifePoint.position).normalized;

                GameObject knife = ObjectPool.SpawnObject(_knifePrefab, _attackKnifePoint.position, Quaternion.LookRotation(direction));
                Debug.Log($"Knife #{i + 1} launched at {enemy.name}.");
                ActivateAttackVibration();
            }
        }

        private void ActivateAttackVibration()
        {
            if (PlayerPrefs.GetInt(Constants.VibrationParameter) == 1)
                HapticFeedback.LightFeedback();
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

        private int MagicalHit() =>
            Physics.OverlapSphereNonAlloc(_magicalAttackPoint.position, _playerConfig.MagicalAttackRange, _hits, _layerMask);

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
