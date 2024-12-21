using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public int numberOfWave;
    [SerializeField] private GameObject _enemyPrefab;
    public void StartWave()
    {
        Debug.Log("Wave Started!");
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }
}
