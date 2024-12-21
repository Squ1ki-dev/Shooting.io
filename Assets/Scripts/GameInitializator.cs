using UnityEngine;
using Zenject;

public class GameInitializer : MonoBehaviour
{
    private MainGame _mainGame;
    private WaveSystem _waveSystem;

    [Inject]
    private void Construct(MainGame mainGame, WaveSystem waveSystem)
    {
        _mainGame = mainGame;
        _waveSystem = waveSystem;
    }

    private void Start()
    {
        if (_waveSystem == null) Debug.LogError("WaveSystem is not injected!");
    
        _mainGame.OnGameStateEntered += _waveSystem.StartWave;
    }


    private void OnDestroy()
    {
        _mainGame.OnGameStateEntered -= _waveSystem.StartWave;
    }
}
