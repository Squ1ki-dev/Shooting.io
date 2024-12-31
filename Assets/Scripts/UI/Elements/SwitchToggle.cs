using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CodeBase.UI.Audio;

namespace CodeBase.UI.Elements
{
    public class SwitchToggle : MonoBehaviour
    {
        private Toggle _toggle;
        private Vector2 _handlePosition;

        [SerializeField] private RectTransform _uiHandleRectTransform;
        [SerializeField] private SoundSettingsSO _settingsConfig;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();

            _handlePosition = _uiHandleRectTransform.anchoredPosition;

            _toggle.onValueChanged.AddListener(OnSwitch);

            if (_toggle.isOn)
                OnSwitch(true);
        }

        private void OnSwitch(bool on) => _uiHandleRectTransform.DOAnchorPos(on ? _handlePosition * -1 : _handlePosition, .4f);

        private void OnDestroy() => _toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}