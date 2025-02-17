using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CandyCoded.HapticFeedback;
using CodeBase.Wave;
using GameAnalyticsSDK;

namespace CodeBase.UI.Screens
{
    public class LoseScreen : WindowBase
    {
        private const string Init = "Init";
        [SerializeField] private WaveSetupSO _waveConfig;
        [SerializeField] private Button _restartBtn, _exitBtn;
        private void Start() 
        {
            TinySauce.OnGameFinished(false, _waveConfig.CurrentWave);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "World_01", Constants.WaveNumber, _waveConfig.CurrentWave);

            _restartBtn.onClick.AddListener(Restart);
            _exitBtn.onClick.AddListener(Restart);
            if (PlayerPrefs.GetInt(Constants.VibrationParameter) == 1)
                HapticFeedback.MediumFeedback();
        }

        private void Restart() 
        {
            SceneManager.LoadScene(Init);
            SaveWaveNumber();
        }

        private void SaveWaveNumber()
        {
            PlayerPrefs.SetInt(Constants.WaveNumber, 1);
            PlayerPrefs.Save();
        }

        private void OnDestroy()
        {
            _restartBtn.onClick.RemoveListener(Restart);
            _restartBtn.onClick.RemoveListener(Restart);
        }
    }
}