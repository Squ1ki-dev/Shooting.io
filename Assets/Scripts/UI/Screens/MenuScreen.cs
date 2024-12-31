using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuScreen : WindowBase
{
    [SerializeField] private Button _playBtn, _settingsBtn;
    [SerializeField] private GameBootstrapper _gameBootstrapper;
    [SerializeField] private PanelManager _panelManager;

    private void Start()
    {
        _playBtn.onClick.AddListener(OnPlayButtonPressed);
        _settingsBtn.onClick.AddListener(OpenSettings);
    }

    private void OpenSettings() => _panelManager.OpenPanelByIndex(1);
    private void OnPlayButtonPressed() => _gameBootstrapper.Run();

    private void OnDestroy()
    {
        _playBtn.onClick.RemoveListener(OnPlayButtonPressed);
        _settingsBtn.onClick.RemoveListener(OpenSettings);
    }
}
