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
    [SerializeField] private Button _nextWaveBtn, _exitBtn;
    [SerializeField] private WaveSetupSO _waveConfig;
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

    private void Start() 
    {
        _nextWaveBtn.onClick.AddListener(NextWave);
        _exitBtn.onClick.AddListener(NextWave);
    }

    private void NextWave()
    {
        _waveConfig.CurrentWave++;
        PlayerPrefs.SetInt(Constants.WaveNumber, _waveConfig.CurrentWave);
        Debug.Log($"Current Wave " + _waveConfig.CurrentWave);
        SceneManager.LoadScene(Init);
    }

    private void OnDestroy()
    {
        _nextWaveBtn.onClick.RemoveListener(NextWave);
        _exitBtn.onClick.RemoveListener(NextWave);
    }
}
