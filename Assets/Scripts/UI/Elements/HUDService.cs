using System.Collections.Generic;
using CodeBase.Wave;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class HUDService : MonoBehaviour
    {
        [SerializeField] private TMP_Text _waveNumberText;
        [SerializeField] private GameObject _joystick;
        [SerializeField] private WaveSetupSO _waveConfig;
        private GameState _gameState;

        private void Start()
        {
            _gameState = FindObjectOfType<GameState>();
            _waveNumberText.text = "Wave: " + _waveConfig.CurrentWave.ToString();

            CheckForValidState();
        }

        private void Update() => CheckForValidState();

        private void CheckForValidState()
        {
            if(_gameState.CurrentState == GameStates.Game)
                _joystick.SetActive(true);
            else
                _joystick.SetActive(false);
        }
    }
}
