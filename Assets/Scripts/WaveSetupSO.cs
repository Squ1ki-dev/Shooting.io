using UnityEngine;

[CreateAssetMenu(fileName = "WaveSystem", menuName = "")]
public class WaveSetupSO : ScriptableObject
{
    public int BaseWeakEnemyCount;
    public int BaseNormalEnemyCount;
    public int EnemyIncrementPerWave;
    public int BossWaveInterval;
    public int BossWaveWeakEnemies;
    public int BossWaveNormalEnemies;
    public float MinSpawnRadius;
    public float MaxSpawnRadius;
}