using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.Player;

namespace CodeBase.UI.Elements
{
    public class MenuLevelBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText, _xpText;
        [SerializeField] private Image _imageCurrent;
        [SerializeField] private PlayerStatsSO _playerConfig;

        private int _savedLevel;
        private int _currentXP;
        private int _targetXP;
        private float _savedFillAmount;
        private const int DefaultTargetXP = 100;

        private void Start() => Initialize();

        private void Initialize()
        {
            _savedLevel = PlayerPrefs.GetInt(Constants.Level, 1);
            _playerConfig.Level = _savedLevel;
            _levelText.text = _savedLevel.ToString();

            _savedFillAmount = PlayerPrefs.GetFloat(Constants.FillAmount, 0);
            _imageCurrent.fillAmount = _savedFillAmount;

            _targetXP = PlayerPrefs.GetInt(Constants.TargetXP, DefaultTargetXP);
            _currentXP = Mathf.RoundToInt(_savedFillAmount * _targetXP);

            UpdateXPText();
        }

        private void Update() => UpdateUI();

        private void UpdateUI()
        {
            _levelText.text = _playerConfig.Level.ToString();
            _imageCurrent.fillAmount = PlayerPrefs.GetFloat(Constants.FillAmount, 0);
        }

        private void UpdateXPText() => _xpText.text = $"{_currentXP}/{_targetXP}";
    }
}