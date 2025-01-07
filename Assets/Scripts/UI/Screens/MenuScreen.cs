using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using GameAnalyticsSDK;

namespace CodeBase.UI.Screens
{
    public class MenuScreen : WindowBase
    {
        [SerializeField] private Button _playBtn, _settingsBtn;
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        [SerializeField] private PanelManager _panelManager;

        private void Start()
        {
            TinySauce.OnGameStarted("StartGame");
            _playBtn.enabled = true;
            _playBtn.onClick.AddListener(OnPlayButtonPressed);
            _settingsBtn.onClick.AddListener(OpenSettings);
        }

        private void OpenSettings() => _panelManager.OpenPanelByIndex(1);
        private void OnPlayButtonPressed()
        {
            _playBtn.enabled = false;
            _gameBootstrapper.Run();
            TinySauce.OnGameStarted(Constants.WaveNumber);
            GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, "World_01", Constants.WaveNumber);
        }

        private void OnDestroy()
        {
            _playBtn.onClick.RemoveListener(OnPlayButtonPressed);
            _settingsBtn.onClick.RemoveListener(OpenSettings);
        }
    }
}