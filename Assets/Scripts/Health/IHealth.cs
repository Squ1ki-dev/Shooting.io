using System;
using TMPro;

public interface IHealth
{
    event Action HealthChanged;
    float Current { get; set; }
    float Max { get; set; }
    void TakeDamage(float damage);
}