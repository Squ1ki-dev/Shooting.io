using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using CodeBase.Player;
using CodeBase.Service;

public class UpgradeScreen : WindowBase
{
    [SerializeField] private Button _rangeBtn, _powerBtn, _speedBtn;
    [SerializeField] private PlayerStatsSO _playerConfig;
    private PanelManager _panelManager;
    private GameState _gameState;

    private const float _rangeGrowth = 0.05f;
    private const float _damageGrowth = 0.10f;
    private const float _movementSpeedGrowth = 0.02f;

    private string SaveFilePath => Path.Combine(Application.persistentDataPath, "PlayerStats.json");

    [Inject]
    private void Construct(GameState gameState, PanelManager panelManager)
    {
        _gameState = gameState;
        _panelManager = panelManager;
    }

    private void Start()
    {
        PlayerStatsService.LoadPlayerStats(_playerConfig); // Use the utility class
        _rangeBtn.onClick.AddListener(IncreaseRange);
        _powerBtn.onClick.AddListener(IncreasePower);
        _speedBtn.onClick.AddListener(IncreaseSpeed);
    }

    private void IncreaseRange()
    {
        _playerConfig.AttackRange = CalculateStat(_playerConfig.AttackRange, _rangeGrowth);
        PlayerStatsService.SavePlayerStats(_playerConfig); // Use the utility class
        ContinueGame();
    }

    private void IncreasePower()
    {
        _playerConfig.Damage = CalculateStat(_playerConfig.Damage, _damageGrowth);
        PlayerStatsService.SavePlayerStats(_playerConfig); // Use the utility class
        ContinueGame();
    }

    private void IncreaseSpeed()
    {
        _playerConfig.Speed = CalculateStat(_playerConfig.Speed, _movementSpeedGrowth);
        PlayerStatsService.SavePlayerStats(_playerConfig); // Use the utility class
        ContinueGame();
    }

    private void ContinueGame()
    {
        _panelManager.CloseAllPanels();
        _gameState.ChangeState(GameStates.Game);
    }

    private float CalculateStat(float baseValue, float growthRate)
    {
        return baseValue * (1 + growthRate);
    }

    private void SavePlayerStats()
    {
        string json = JsonUtility.ToJson(_playerConfig);
        File.WriteAllText(SaveFilePath, json);
    }

    private void LoadPlayerStats()
    {
        if (File.Exists(SaveFilePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            JsonUtility.FromJsonOverwrite(json, _playerConfig);
        }
    }

    private void OnDestroy()
    {
        _rangeBtn.onClick.RemoveListener(IncreaseRange);
        _powerBtn.onClick.RemoveListener(IncreasePower);
        _speedBtn.onClick.RemoveListener(IncreaseSpeed);
    }
}