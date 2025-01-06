using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CandyCoded.HapticFeedback;
using CodeBase.Service;
using CodeBase.Enemy;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float Cleavage;
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private EnemyAnimator _animator;

    private const float EffectiveDistance = 0.5f;
    private IGameFactory _factory;
    private Transform _playerTranfrom;
    private float _attackCooldown;
    private bool _isAttacking;
    private bool _attackIsActive;
    private int _layerMask;
    private Collider[] _hits = new Collider[1];

    private void Awake()
    {
        _layerMask = 1 << LayerMask.NameToLayer("Player");
        _factory = AllServices.Container.Single<IGameFactory>();
        _factory.PlayerCreated += OnPlayerCreated;
    }

    private void Update()
    {
        UpdateCooldown();

        if (CanAttack())
            StartAttack();
    }

    private void OnAttack()
    {
        if (Hit(out Collider hit))
        {
            PhysicsDebug.DrawDebug(attackPoint.position, Cleavage, 3);
            Debug.Log($"Hit Player: {hit.name}");
            hit.transform.GetComponent<IHealth>().TakeDamage(enemySO.DamageValue);
            if (PlayerPrefs.GetInt(Constants.VibrationParameter) == 1)
                HapticFeedback.LightFeedback();
        }
    }

    private void OnAttackEnded()
    {
        _attackCooldown = enemySO.AttackCooldown;
        _isAttacking = false;
    }

    private bool Hit(out Collider hit)
    {
        int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);
        hit = _hits.FirstOrDefault();

        return hitsCount > 0;
    }
    private Vector3 StartPoint()
    {
        return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
            transform.forward * EffectiveDistance;
    }

    public void EnableAttack() => _attackIsActive = true;
    public void DisableAttack() => _attackIsActive = false;

    private void UpdateCooldown()
    {
        if (!CooldownIsUp())
            _attackCooldown -= Time.deltaTime;
    }

    private void StartAttack()
    {
        transform.LookAt(_playerTranfrom);
        _isAttacking = true;
        _animator.PlayAttack();
    }

    private bool CooldownIsUp() => _attackCooldown <= 0;
    private bool CanAttack() => _attackIsActive && !_isAttacking && CooldownIsUp();

    private void OnPlayerCreated() => _playerTranfrom = _factory.PlayerObject.transform;
}