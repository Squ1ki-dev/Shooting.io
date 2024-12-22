using UnityEngine;
using Zenject;

public class GameInitializer : MonoBehaviour
{
    private GameState _mainGame;
    private WaveSystem _waveSystem;

    [Inject]
    private void Construct(GameState mainGame, WaveSystem waveSystem)
    {
        _mainGame = mainGame;
        _waveSystem = waveSystem;
    }

    private void Start()
    {
        if (_waveSystem == null) Debug.LogError("WaveSystem is not injected!");
    
        _mainGame.OnGameStateEntered += _waveSystem.StartNextWave;
    }


    private void OnDestroy()
    {
        _mainGame.OnGameStateEntered -= _waveSystem.StartNextWave;
    }
}
