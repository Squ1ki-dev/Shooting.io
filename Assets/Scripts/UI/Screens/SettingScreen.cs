using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScreen : WindowBase
{
    [SerializeField] private Button _closeBtn;
    [SerializeField] private PanelManager _panelManager;

    private void Start() => _closeBtn.onClick.AddListener(OpenMenu);
    private void OpenMenu() => _panelManager.OpenPanelByIndex(0);
    private void OnDestroy() => _closeBtn.onClick.RemoveListener(OpenMenu);
}
