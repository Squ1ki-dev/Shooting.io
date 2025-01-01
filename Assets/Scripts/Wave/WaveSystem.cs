using System.Collections;
using UnityEngine;
using TMPro;
using Zenject;

namespace CodeBase.Wave
{
    public class WaveSystem : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _enemyParent;
        [SerializeField] private EnemySO _weakEnemySO;
        [SerializeField] private EnemySO _normalEnemySO;
        [SerializeField] private EnemySO _bossEnemySO;
        [SerializeField] private WaveSetupSO _waveSetup;
        [SerializeField] private TMP_Text _timerText;

        private GameState _gameState;
        private int _weakEnemyCount;
        private int _normalEnemyCount;
        private float _waveTime;
        private bool _waveActive;
        public int CurrentWave => _waveSetup.CurrentWave;

        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey(Constants.WaveNumber))
            {
                PlayerPrefs.SetInt(Constants.WaveNumber, _waveSetup.CurrentWave);
                PlayerPrefs.Save();
            }

            UpdateEnemyXP(_waveSetup.CurrentWave);

            _waveSetup.CurrentWave = PlayerPrefs.GetInt(Constants.WaveNumber);
            Debug.Log($"Wave {_waveSetup.CurrentWave}");
        }

        private void Update()
        {
            if (!_waveActive) return;

            if (_gameState.CurrentState == GameStates.Game)
            {
                _waveTime -= Time.deltaTime;
                UpdateTimerText(_waveTime);
            }
            else
                _timerText.gameObject.SetActive(false);

            if (_waveTime <= 0)
                EndWave();
        }

        public void StartNextWave()
        {
            _waveTime = CalculateWaveTime(_waveSetup.CurrentWave);
            _waveActive = true;
            UpdateEnemyXP(_waveSetup.CurrentWave);
            SpawnEnemies(_waveSetup.CurrentWave);

            PlayerPrefs.SetInt(Constants.WaveNumber, _waveSetup.CurrentWave);
            _waveSetup.CurrentWave++;
        }

        private void EndWave()
        {
            _waveActive = false;
            _gameState.ChangeState(GameStates.Finish);
            //_timerText.gameObject.SetActive(false);
        }

        private void UpdateTimerText(float time)
        {
            _timerText.gameObject.SetActive(true);
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            _timerText.text = $"{minutes:00}:{seconds:00}";
        }

        private void UpdateEnemyXP(int waveNumber)
        {
            _weakEnemySO.XPValue = CalculateWeakXP();
            _normalEnemySO.XPValue = CalculateNormalXP(waveNumber);
            _bossEnemySO.XPValue = CalculateBossXP(waveNumber);
        }

        private void SpawnEnemies(int waveNumber)
        {
            _weakEnemyCount = _waveSetup.BaseWeakEnemyCount + (waveNumber - 1) * _waveSetup.EnemyIncrementPerWave;
            if(waveNumber >= 5)
                _normalEnemyCount = _waveSetup.BaseNormalEnemyCount + (waveNumber - 1) * _waveSetup.EnemyIncrementPerWave;
            else
                _normalEnemyCount = 0;

            if (IsBossWave(waveNumber))
                SpawnBossEnemy(waveNumber);

            SpawnEnemyGroup(_weakEnemySO, _weakEnemyCount);
            SpawnEnemyGroup(_normalEnemySO, _normalEnemyCount);
        }

        private void SpawnBossEnemy(int waveNumber)
        {
            int bossWaveWeakEnemies = _waveSetup.BossWaveWeakEnemies + (waveNumber - 1) * _waveSetup.EnemyIncrementPerWave;
            int bossWaveNormalEnemies = _waveSetup.BossWaveNormalEnemies + (waveNumber - 1) * _waveSetup.EnemyIncrementPerWave;

            SpawnEnemyGroup(_weakEnemySO, bossWaveWeakEnemies);
            SpawnEnemyGroup(_normalEnemySO, bossWaveNormalEnemies);

            Vector3 bossPosition = GetRandomPositionAround(_spawnPoint.position, _waveSetup.MinSpawnRadius, _waveSetup.MaxSpawnRadius);
            GameObject bossEnemy = ObjectPool.SpawnObject(_bossEnemySO.EnemyPrefab, bossPosition, Quaternion.identity);
            bossEnemy.transform.SetParent(_enemyParent); // Set parent
        }

        private void SpawnEnemyGroup(EnemySO enemySO, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 position = GetRandomPositionAround(_spawnPoint.position, _waveSetup.MinSpawnRadius, _waveSetup.MaxSpawnRadius);
                GameObject enemy = ObjectPool.SpawnObject(enemySO.EnemyPrefab, position, Quaternion.identity);
                enemy.transform.SetParent(_enemyParent); // Set parent
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

        private bool IsBossWave(int waveNumber)
        {
            return waveNumber % _waveSetup.BossWaveInterval == 0;
        }

        private float CalculateWaveTime(int waveNumber)
        {
            return 30 + (waveNumber - 1) * 10;
        }

        private int CalculateWeakXP()
        {
            return 5; // Fixed base XP
        }

        private int CalculateNormalXP(int waveNumber)
        {
            int normalXP = Mathf.RoundToInt(5 * 2 + (waveNumber * 0.5f));
            PlayerPrefs.SetInt(Constants.NormalXP, normalXP);
            return _normalEnemySO.XPValue = normalXP;
        }

        private int CalculateBossXP(int waveNumber)
        {
            int bossXP = Mathf.RoundToInt(5 * 10 + (waveNumber * 5));
            PlayerPrefs.SetInt(Constants.BossXP, bossXP);
            return _bossEnemySO.XPValue = bossXP;
        }
    }
}
