using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.SceneManagement;
using CodeBase.Wave;
using System.Linq;
using CodeBase.Service;
using CodeBase.Player;

namespace CodeBase.UI.Screens
{
    public class FinishScreen : WindowBase
    {
        private const string Init = "Init";
        [SerializeField] private Button _rangeBtn, _powerBtn, _speedBtn, _maxHPBtn, _healingHPBtn, _knivesBtn;
        [SerializeField] private Button _nextWaveBtn, _exitBtn;
        [SerializeField] private List<Button> _upgradesList = new List<Button>();
        [SerializeField] private WaveSetupSO _waveConfig;
        [SerializeField] private PlayerStatsSO _playerConfig;
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
            ActivateNextButton(false);
            ShowRandomButtons(3);

            _rangeBtn.onClick.AddListener(() => IncreaseRange());
            _powerBtn.onClick.AddListener(() => IncreasePower());
            _speedBtn.onClick.AddListener(() => IncreaseSpeed());
            _maxHPBtn.onClick.AddListener(() => IncreaseMaxHP());
            _healingHPBtn.onClick.AddListener(() => IncreaseRegenerationSpeed());
            _knivesBtn.onClick.AddListener(() => HandleKnivesUpgrade());
            _nextWaveBtn.onClick.AddListener(() => HandleNextWave());
            _exitBtn.onClick.AddListener(() => ExitToMenu());
        }

        private void ShowRandomButtons(int count)
        {
            foreach (var button in _upgradesList)
            {
                button.gameObject.SetActive(false);
            }

            var randomButtons = _upgradesList
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
            ActivateNextButton(true);
        }

        private void IncreasePower()
        {
            UpgradeService.IncreasePower(_playerConfig);
            ActivateNextButton(true);
        }

        private void IncreaseSpeed()
        {
            UpgradeService.IncreaseSpeed(_playerConfig);
            ActivateNextButton(true);
        }

        private void IncreaseMaxHP()
        {
            UpgradeService.IncreaseMaxHP(_playerConfig);
            ActivateNextButton(true);
        }

        private void IncreaseRegenerationSpeed()
        {
            UpgradeService.IncreaseRegenerationSpeed(_playerConfig);
            ActivateNextButton(true);
        }

        private void HandleKnivesUpgrade()
        {
            UpgradeService.IncreaseAmountOfKnives(_playerConfig);

            if (_playerConfig.AmountOfKnives >= UpgradeService.MaxAmountOfKnives)
            {
                _knivesBtn.enabled = false;
                _knivesBtn.interactable = false;
            }

            ActivateNextButton(true);
        }

        private void ActivateNextButton(bool isActive) 
        {
            foreach (var item in _upgradesList)
            {
                item.gameObject.SetActive(false);
            }
            
            _nextWaveBtn.gameObject.SetActive(isActive);
        }

        private void ExitToMenu()
        {
            _panelManager.CloseAllPanels();
            SceneManager.LoadScene(Init);
        }

        private void HandleNextWave()
        {
            _panelManager.CloseAllPanels();
            _waveConfig.CurrentWave++;
            PlayerPrefs.SetInt(Constants.WaveNumber, _waveConfig.CurrentWave);
            Debug.Log($"Current Wave " + _waveConfig.CurrentWave);
            SceneManager.LoadScene(Init);
        }

        private void OnDestroy()
        {
            _rangeBtn.onClick.RemoveListener(() => IncreaseRange());
            _powerBtn.onClick.RemoveListener(() => IncreasePower());
            _speedBtn.onClick.RemoveListener(() => IncreaseSpeed());
            _maxHPBtn.onClick.RemoveListener(() => IncreaseMaxHP());
            _healingHPBtn.onClick.RemoveListener(() => IncreaseRegenerationSpeed());
            _knivesBtn.onClick.RemoveListener(() => HandleKnivesUpgrade());
            _nextWaveBtn.onClick.RemoveListener(() => HandleNextWave());
            _exitBtn.onClick.RemoveListener(() => ExitToMenu());
        }
    }
}