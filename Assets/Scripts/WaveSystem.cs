using UnityEngine;
using TMPro;
using Zenject;

public class WaveSystem : MonoBehaviour
{
    private GameState _mainGame;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private EnemySO _weakEnemySO;
    [SerializeField] private EnemySO _normalEnemySO;
    [SerializeField] private EnemySO _bossEnemySO;
    [SerializeField] private WaveSetupSO _waveSetup;
    [SerializeField] private TMP_Text _timerText;
    
    private int currentWave = 1;
    public int CurrentWave { get { return currentWave; } }
    private float waveTime;
    private bool waveActive = false;

    [Inject]
    private void Construct(GameState mainGame)
    {
        _mainGame = mainGame;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("WaveNumber"))
        {
            PlayerPrefs.SetInt("WaveNumber", currentWave);
            PlayerPrefs.Save();
        }
        
        Debug.Log("Wave " + PlayerPrefs.GetInt("WaveNumber"));
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
                _mainGame.ChangeState(GameStates.Finish);
                _timerText.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        _timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StartNextWave()
    {
        waveTime = GetWaveTime(currentWave);
        waveActive = true;

        SpawnEnemies(currentWave);
        PlayerPrefs.SetInt("WaveNumber", currentWave);
        currentWave++;
    }

    private void SpawnEnemies(int waveNumber)
    {
        int weakEnemyCount = _waveSetup.BaseWeakEnemyCount + (waveNumber - 1) * _waveSetup.EnemyIncrementPerWave;
        int normalEnemyCount =  _waveSetup.BaseNormalEnemyCount + (waveNumber - 1) * _waveSetup.EnemyIncrementPerWave;

        if (waveNumber % _waveSetup.BossWaveInterval == 0) // Boss wave
        {
            weakEnemyCount = _waveSetup.BossWaveWeakEnemies + (waveNumber - 1) * _waveSetup.EnemyIncrementPerWave;
            normalEnemyCount = _waveSetup.BossWaveNormalEnemies + (waveNumber - 1) * _waveSetup.EnemyIncrementPerWave;

            Vector3 bossPosition = GetRandomPositionAround(_spawnPoint.position, _waveSetup.MinSpawnRadius, _waveSetup.MaxSpawnRadius);
            ObjectPool.SpawnObject(_bossEnemySO.EnemyPrefab, bossPosition, Quaternion.identity);
        }

        for (int i = 0; i < weakEnemyCount; i++)
        {
            Vector3 weakEnemyPosition = GetRandomPositionAround(_spawnPoint.position, _waveSetup.MinSpawnRadius, _waveSetup.MaxSpawnRadius);
            ObjectPool.SpawnObject(_weakEnemySO.EnemyPrefab, weakEnemyPosition, Quaternion.identity);
        }

        for (int i = 0; i < normalEnemyCount; i++)
        {
            Vector3 normalEnemyPosition = GetRandomPositionAround(_spawnPoint.position, _waveSetup.MinSpawnRadius, _waveSetup.MaxSpawnRadius);
            ObjectPool.SpawnObject(_normalEnemySO.EnemyPrefab, normalEnemyPosition, Quaternion.identity);
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
