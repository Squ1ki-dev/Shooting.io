using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float Cleavage; // Radius of attack
    [SerializeField] private float AttackDuration = 0.5f; // Time for one attack to finish
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private Transform attackPoint;

    private IGameFactory _factory;
    private Transform _playerTranfrom;
    private float _attackCooldown;
    private bool _isAttacking;
    private bool _attackIsActive;
    private int _layerMask;
    private Collider[] _hits = new Collider[1];
    private float _attackTimer;

    private void Awake()
    {
        _layerMask = 1 << LayerMask.NameToLayer("Player");
        _factory = AllServices.Container.Single<IGameFactory>();
        _factory.PlayerCreated += OnPlayerCreated;

        _attackCooldown = 0f; // Initialize to ready state
    }

    private void Update()
    {
        UpdateCooldown();
        UpdateAttack();

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
        }
    }

    private bool Hit(out Collider hit)
    {
        int hitsCount = Physics.OverlapSphereNonAlloc(attackPoint.position, Cleavage, _hits, _layerMask);
        hit = _hits.FirstOrDefault();

        return hitsCount > 0;
    }

    public void EnableAttack() => _attackIsActive = true;
    public void DisableAttack() => _attackIsActive = false;

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
        transform.LookAt(_playerTranfrom);
        _isAttacking = true;
        _attackTimer = AttackDuration; // Set the timer for attack duration
        OnAttack(); // Simulate attack logic
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

        if (enemySO != null)
            _attackCooldown = enemySO.AttackCooldown;
        else
            _attackCooldown = 1.0f; // Default cooldown
    }

    private bool CooldownIsUp() => _attackCooldown <= 0;
    private bool CanAttack() => _attackIsActive && !_isAttacking && CooldownIsUp();

    private void OnPlayerCreated() => _playerTranfrom = _factory.PlayerObject.transform;
}


// private void OnAttackEnded()
// {
//     _attackCooldown = enemySO.AttackCooldown;
//     _isAttacking = false;
// }


// private void OnAttack()
// {
//     if(Hit(out Collider hit))
//         PhysicsDebug.DrawDebug(attackPoint.position, Cleavage, 3);
// }