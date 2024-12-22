using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private EnemySO _enemySO;
    private float _current;
    public event Action HealthChanged;

    public float Current
    {
        get => _current;
        set
        {
            _current = Mathf.Clamp(value, 0, Max); // Ensure Current doesn't exceed Max or go below 0
            HealthChanged?.Invoke();
        }
    }

    public float Max
    {
        get => _enemySO.Health;
        set => _enemySO.Health = value; 
    }

    private void Awake() => _current = _enemySO.Health;

    public void TakeDamage(float damage)
    {
        if(Current <= 0)
            return;
        
        Current -= damage;

        HealthChanged?.Invoke();
    }
}
