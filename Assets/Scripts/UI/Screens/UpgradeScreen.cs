using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using CodeBase.Player;
using CodeBase.Service;

namespace CodeBase.UI.Screens
{
    public class UpgradeScreen : WindowBase
    {
        [SerializeField] private Button _rangeBtn, _powerBtn, _speedBtn, _maxHPBtn, _healingHPBtn, _knivesBtn;
        [SerializeField] private PlayerStatsSO _playerConfig;

        private List<Button> upgradesList;
        private PanelManager _panelManager;
        private GameState _gameState;

        private const int _maxAmountOfKnives = 5;
        private const float _rangeGrowth = 0.05f;
        private const float _damageGrowth = 0.10f;
        private const float _movementSpeedGrowth = 0.02f;
        private const float _maxHPGrowth = 0.10f;

        [Inject]
        private void Construct(GameState gameState, PanelManager panelManager)
        {
            _gameState = gameState;
            _panelManager = panelManager;
        }

        private void Start()
        {
            PlayerStatsService.LoadPlayerStats(_playerConfig);

            upgradesList = new List<Button> { _rangeBtn, _powerBtn, _speedBtn, _maxHPBtn, _healingHPBtn, _knivesBtn };
            ShowRandomButtons(3);

            _rangeBtn.onClick.AddListener(IncreaseRange);
            _powerBtn.onClick.AddListener(IncreasePower);
            _speedBtn.onClick.AddListener(IncreaseSpeed);
            _maxHPBtn.onClick.AddListener(IncreaseMaxHP);
            _healingHPBtn.onClick.AddListener(IncreaseRegenerationSpeed);
            _knivesBtn.onClick.AddListener(IncreaseAmountOfKnives);
        }

        private void ShowRandomButtons(int count)
        {
            foreach (var button in upgradesList)
            {
                button.gameObject.SetActive(false);
            }

            var randomButtons = upgradesList
                .OrderBy(x => UnityEngine.Random.value)
                .Take(count)
                .ToList();

            foreach (var button in randomButtons)
            {
                button.gameObject.SetActive(true);
            }
        }

        private void IncreaseRange()
        {
            _playerConfig.AttackRange = CalculateStat(_playerConfig.AttackRange, _rangeGrowth);
            PlayerStatsService.SavePlayerStats(_playerConfig);
            ContinueGame();
        }

        private void IncreasePower()
        {
            _playerConfig.Damage = CalculateStat(_playerConfig.Damage, _damageGrowth);
            PlayerStatsService.SavePlayerStats(_playerConfig);
            ContinueGame();
        }

        private void IncreaseSpeed()
        {
            _playerConfig.Speed = CalculateStat(_playerConfig.Speed, _movementSpeedGrowth);
            PlayerStatsService.SavePlayerStats(_playerConfig);
            ContinueGame();
        }

        private void IncreaseMaxHP()
        {
            _playerConfig.MaxHP = CalculateStat(_playerConfig.MaxHP, _maxHPGrowth);
            PlayerStatsService.SavePlayerStats(_playerConfig);
            ContinueGame();
        }

        private void IncreaseRegenerationSpeed()
        {
            _playerConfig.RegenerationSpeed = CalculateStat(_playerConfig.RegenerationSpeed, _movementSpeedGrowth);
            ContinueGame();
        }

        private void IncreaseAmountOfKnives()
        {
            if(_playerConfig.AmountOfKnives != _maxAmountOfKnives)
                _playerConfig.AmountOfKnives++;
            else
            {
                _playerConfig.AmountOfKnives = _maxAmountOfKnives;
                _knivesBtn.enabled = false;
            }

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

        private void OnDestroy()
        {
            _rangeBtn.onClick.RemoveListener(IncreaseRange);
            _powerBtn.onClick.RemoveListener(IncreasePower);
            _speedBtn.onClick.RemoveListener(IncreaseSpeed);
            _maxHPBtn.onClick.RemoveListener(IncreaseMaxHP);
            _healingHPBtn.onClick.RemoveListener(IncreaseRegenerationSpeed);
            _knivesBtn.onClick.RemoveListener(IncreaseAmountOfKnives);
        }
    }
}