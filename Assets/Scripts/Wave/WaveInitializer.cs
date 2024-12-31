using UnityEngine;
using Zenject;

namespace CodeBase.Wave
{
    public class WaveInitializer : MonoBehaviour
    {
        private GameState _gameState;
        private WaveSystem _waveSystem;

        [Inject]
        private void Construct(GameState gameState, WaveSystem waveSystem)
        {
            _gameState = gameState;
            _waveSystem = waveSystem;
        }

        private void Start()
        {
            _waveSystem.StartNextWave();
        }
    }
}