using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "")]
public class EnemySO : ScriptableObject
{
    public int XPValue;
    public float Health;
    public float DamageValue;
    public float AttackCooldown;
    public GameObject EnemyPrefab;
}