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

        private int _currentXP;
        private int _targetXP;

        private const int DefaultTargetXP = 100;

        private void Start() => Initialize();

        private void Initialize()
        {
            int savedLevel = PlayerPrefs.GetInt(Constants.Level, 1);
            _playerConfig.Level = savedLevel;
            _levelText.text = savedLevel.ToString();

            float savedFillAmount = PlayerPrefs.GetFloat(Constants.FillAmount, 0);
            _imageCurrent.fillAmount = savedFillAmount;

            _targetXP = PlayerPrefs.GetInt(Constants.TargetXP, DefaultTargetXP);

            // Calculate _currentXP based on fillAmount and target XP
            _currentXP = Mathf.RoundToInt(savedFillAmount * _targetXP);

            UpdateXPText();
        }


        private void Update() => UpdateUI();

        private void UpdateUI()
        {
            _levelText.text = _playerConfig.Level.ToString();
            _imageCurrent.fillAmount = PlayerPrefs.GetFloat(Constants.FillAmount, 0);
        }

        private void UpdateXPText()
        {
            _xpText.text = $"{_currentXP}/{_targetXP}";
        }
    }
}