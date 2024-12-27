using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.SceneManagement;
using CodeBase.Wave;

public class FinishScreen : WindowBase
{
    private const string Init = "Init";
    [SerializeField] private Button nextWaveBtn;
    private WaveSystem _waveSystem;
    private PanelManager _panelManager;
    private GameState _gameState;

    [Inject]
    private void Construct(GameState gameState, WaveSystem waveSystem, PanelManager panelManager)
    {
        _gameState = gameState;
        _waveSystem = waveSystem;
        _panelManager = panelManager;
    }

    private void Start() => nextWaveBtn.onClick.AddListener(NextWave);

    private void NextWave()
    {
        _waveSystem.StartNextWave();
        _panelManager.CloseAllPanels();
        SceneManager.LoadScene(Init);
    }

    private void OnDestroy() => _gameState.OnGameStateEntered -= _waveSystem.StartNextWave;
}
