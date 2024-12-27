using UnityEngine;
using Zenject;

namespace CodeBase.Wave
{
    public class WaveInitializer : MonoBehaviour
    {
        private GameState _mainGame;
        private WaveSystem _waveSystem;

        [Inject]
        private void Construct(GameState mainGame, WaveSystem waveSystem)
        {
            _mainGame = mainGame;
            _waveSystem = waveSystem;
        }

        private void Start() => _mainGame.OnGameStateEntered += _waveSystem.StartNextWave;
        private void OnDestroy() => _mainGame.OnGameStateEntered -= _waveSystem.StartNextWave;
    }
}