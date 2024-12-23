using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(15);
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
        Physics.OverlapBoxNonAlloc(attackPoint.position, boxSize / 2, _hits, Quaternion.identity, _layerMask);

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPoint.position, boxSize);
        }
    }
}
