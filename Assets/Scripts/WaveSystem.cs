using UnityEngine;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemySO weakEnemySO;
    [SerializeField] private EnemySO normalEnemySO;
    [SerializeField] private EnemySO bossEnemySO;
    [SerializeField] private WaveSetupSO waveSetup;
    [SerializeField] private TMP_Text timerText;

    private int currentWave = 1;
    public int CurrentWave { get { return currentWave; } }
    private float waveTime;
    private bool waveActive = false;

    private void Start()
    {
        Debug.Log("Wave" + PlayerPrefs.GetInt("WaveNumber"));
    }

    private void Update()
    {
        if (waveActive)
        {
            waveTime -= Time.deltaTime;
            UpdateTimerText(waveTime);

            if (waveTime <= 0)
            {
                waveActive = false;
                StartNextWave();
            }
        }
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StartNextWave()
    {
        waveTime = GetWaveTime(currentWave);
        waveActive = true;

        SpawnEnemies(currentWave);

        currentWave++;
        PlayerPrefs.SetInt("WaveNumber", currentWave);
    }

    private void SpawnEnemies(int waveNumber)
    {
        int weakEnemyCount = waveSetup.BaseWeakEnemyCount + (waveNumber - 1) * waveSetup.EnemyIncrementPerWave;
        int normalEnemyCount =  waveSetup.BaseNormalEnemyCount + (waveNumber - 1) * waveSetup.EnemyIncrementPerWave;

        if (waveNumber % waveSetup.BossWaveInterval == 0) // Boss wave
        {
            weakEnemyCount = waveSetup.BossWaveWeakEnemies + (waveNumber - 1) * waveSetup.EnemyIncrementPerWave;
            normalEnemyCount = waveSetup.BossWaveNormalEnemies + (waveNumber - 1) * waveSetup.EnemyIncrementPerWave;

            Vector3 bossPosition = GetRandomPositionAround(spawnPoint.position, waveSetup.MinSpawnRadius, waveSetup.MaxSpawnRadius);
            ObjectPool.SpawnObject(bossEnemySO.EnemyPrefab, bossPosition, Quaternion.identity);
        }

        for (int i = 0; i < weakEnemyCount; i++)
        {
            Vector3 weakEnemyPosition = GetRandomPositionAround(spawnPoint.position, waveSetup.MinSpawnRadius, waveSetup.MaxSpawnRadius);
            ObjectPool.SpawnObject(weakEnemySO.EnemyPrefab, weakEnemyPosition, Quaternion.identity);
        }

        for (int i = 0; i < normalEnemyCount; i++)
        {
            Vector3 normalEnemyPosition = GetRandomPositionAround(spawnPoint.position, waveSetup.MinSpawnRadius, waveSetup.MaxSpawnRadius);
            ObjectPool.SpawnObject(normalEnemySO.EnemyPrefab, normalEnemyPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionAround(Vector3 center, float minRadius, float maxRadius)
    {
        float radius = Random.Range(minRadius, maxRadius);
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        float xOffset = Mathf.Cos(angle) * radius;
        float zOffset = Mathf.Sin(angle) * radius;

        return new Vector3(center.x + xOffset, center.y, center.z + zOffset);
    }

    private float GetWaveTime(int waveNumber)
    {
        return 20 + (waveNumber - 1) * 10;
    }
}
