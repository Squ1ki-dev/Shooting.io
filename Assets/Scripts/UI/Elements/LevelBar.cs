using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeBase.Enemy;
using CodeBase.Player;

namespace CodeBase.UI.Elements
{
    public class LevelBar : MonoBehaviour
    {
        private int _currentXP;
        private GameState _mainGame;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Image _imageCurrent;
        [SerializeField] private int _targetXP;
        [SerializeField] private PlayerStatsSO playerConfig;

        private void Start()
        {
            _mainGame = FindAnyObjectByType<GameState>();
            LootPiece.OnLootPicked += HandleLootPicked;

            Init();
        }

        private void Init()
        {
            if (!PlayerPrefs.HasKey(Constants.Level))
            {
                PlayerPrefs.SetInt(Constants.Level, playerConfig.Level);
                PlayerPrefs.Save();
            }

            _levelText.text = "Level: " + PlayerPrefs.GetInt(Constants.Level);
            playerConfig.Level = PlayerPrefs.GetInt(Constants.Level);

            if (PlayerPrefs.HasKey(Constants.FillAmount))
            {
                _imageCurrent.fillAmount = PlayerPrefs.GetFloat(Constants.FillAmount);
                _currentXP = Mathf.RoundToInt(_imageCurrent.fillAmount * _targetXP);
            }
            else
            {
                _imageCurrent.fillAmount = 0;
                _currentXP = 0;
            }

            Debug.Log("Level " + PlayerPrefs.GetInt(Constants.Level));
        }

        private void OnDestroy() => LootPiece.OnLootPicked -= HandleLootPicked;

        private void Update() => SetValue(_currentXP, _targetXP);

        private void HandleLootPicked(int xp) => _currentXP += xp;

        public void SetValue(float current, float target)
        {
            _levelText.text = "Level: " + playerConfig.Level.ToString();
            _imageCurrent.fillAmount = current / target;

            PlayerPrefs.SetFloat(Constants.FillAmount, _imageCurrent.fillAmount);
            PlayerPrefs.Save();

            if (_currentXP >= _targetXP)
            {
                _currentXP -= _targetXP;
                playerConfig.Level++;
                PlayerPrefs.SetInt(Constants.Level, playerConfig.Level);
                Debug.Log($"Level increased to: {playerConfig.Level}");
                _mainGame.ChangeState(GameStates.Upgrade);

                _targetXP += 50;
                PlayerPrefs.SetInt(Constants.TargetXP, _targetXP);
                PlayerPrefs.Save();
            }
        }
    }
}