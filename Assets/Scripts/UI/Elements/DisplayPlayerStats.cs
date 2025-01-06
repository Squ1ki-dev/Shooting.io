using UnityEngine;
using UnityEngine.UI;
using System.IO;
using CodeBase.Player;
using CodeBase.Service;
using TMPro;

namespace CodeBase.UI.Elements
{
    public class DisplayPlayerStats : MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO _playerStats;
        [SerializeField] private TMP_Text _hpText, _speedText;
        [SerializeField] private TMP_Text _damageText, _rangeText;

        private string SaveFilePath => Path.Combine(Application.persistentDataPath, "PlayerStats.json");

        private void Start()
        {
            PlayerStatsService.LoadPlayerStats(_playerStats);
            UpdateStatsDisplay();
        }

        private void UpdateStatsDisplay()
        {
            _hpText.text = $"{_playerStats.MaxHP}";
            _speedText.text = $"{(int)_playerStats.Speed}";
            _damageText.text = $"{_playerStats.Damage:F2}";
            _rangeText.text = $"{_playerStats.AttackRange:F2}";
        }
    }
}