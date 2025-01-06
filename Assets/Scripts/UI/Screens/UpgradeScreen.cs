using System;
using System.Linq;
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
        [SerializeField] private List<Button> upgradesList = new List<Button>();

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
            PlayerStatsService.LoadPlayerStats(_playerConfig);

            ShowRandomButtons(3);

            _rangeBtn.onClick.AddListener(() => IncreaseRange());
            _powerBtn.onClick.AddListener(() => IncreasePower());
            _speedBtn.onClick.AddListener(() => IncreaseSpeed());
            _maxHPBtn.onClick.AddListener(() => IncreaseMaxHP());
            _healingHPBtn.onClick.AddListener(() => IncreaseRegenerationSpeed());
            _knivesBtn.onClick.AddListener(() => HandleKnivesUpgrade());
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
            UpgradeService.IncreaseRange(_playerConfig);
            ContinueGame();
        }

        private void IncreasePower()
        {
            UpgradeService.IncreasePower(_playerConfig);
            ContinueGame();
        }

        private void IncreaseSpeed()
        {
            UpgradeService.IncreaseSpeed(_playerConfig);
            ContinueGame();
        }

        private void IncreaseMaxHP()
        {
            UpgradeService.IncreaseMaxHP(_playerConfig);
            ContinueGame();
        }

        private void IncreaseRegenerationSpeed()
        {
            UpgradeService.IncreaseRegenerationSpeed(_playerConfig);
            ContinueGame();
        }

        private void HandleKnivesUpgrade()
        {
            UpgradeService.IncreaseAmountOfKnives(_playerConfig);

            if (_playerConfig.AmountOfKnives >= UpgradeService.MaxAmountOfKnives)
            {
                _knivesBtn.enabled = false;
                _knivesBtn.interactable = false;
            }

            ContinueGame();
        }

        private void ContinueGame()
        {
            _panelManager.CloseAllPanels();
            _gameState.ChangeState(GameStates.Game);
        }

        private void OnDestroy()
        {
            _rangeBtn.onClick.RemoveListener(() => IncreaseRange());
            _powerBtn.onClick.RemoveListener(() => IncreasePower());
            _speedBtn.onClick.RemoveListener(() => IncreaseSpeed());
            _maxHPBtn.onClick.RemoveListener(() => IncreaseMaxHP());
            _healingHPBtn.onClick.RemoveListener(() => IncreaseRegenerationSpeed());
            _knivesBtn.onClick.RemoveListener(() => HandleKnivesUpgrade());
        }
    }
}