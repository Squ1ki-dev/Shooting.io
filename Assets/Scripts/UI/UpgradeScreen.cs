using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeScreen : WindowBase
{
    [SerializeField] private Button _rangeBtn, _powerBtn, _speedBtn;
    [SerializeField] private PlayerStatsSO _playerConfig;
    private PanelManager _panelManager;
    private GameState _gameState;

    [Inject]
    private void Construct(GameState gameState, PanelManager panelManager)
    {
        _gameState = gameState;
        _panelManager = panelManager;
    }

    private void Start()
    {
        _rangeBtn.onClick.AddListener(IncreaseRange);
        _powerBtn.onClick.AddListener(IncreasePower);
        _speedBtn.onClick.AddListener(IncreaseSpeed);
    }

    private void IncreaseRange()
    {
        _playerConfig.AttackRange += _playerConfig.AttackRange + 10;
        _panelManager.CloseAllPanels();
        _gameState.ChangeState(GameStates.Game);
    }

    private void IncreasePower()
    {
        _playerConfig.Damage += _playerConfig.Damage + 10;
        ContinueGame();
    }

    private void IncreaseSpeed()
    {
        _playerConfig.Speed += _playerConfig.Speed + 2;
        ContinueGame();
    }

    private void ContinueGame()
    {
        _panelManager.CloseAllPanels();
        _gameState.ChangeState(GameStates.Game);
    }
}
