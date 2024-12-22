using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _max = 100f; // Default value for MaxHP
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
        get => _max;
        set => _max = value;
    }

    private void Awake() => _current = _max;

    public void TakeDamage(float damage)
    {
        if (Current <= 0)
            return;

        Current -= damage;

        HealthChanged?.Invoke();
    }
}
