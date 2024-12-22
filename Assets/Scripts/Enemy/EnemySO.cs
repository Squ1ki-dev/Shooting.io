using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "")]
public class EnemySO : ScriptableObject
{
    public int Health;
    public float DamageValue;
    public float AttackCooldown;
    public GameObject EnemyPrefab;
}