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
        [SerializeField] private TMP_Text _rangeText, _damageText;

        private string SaveFilePath => Path.Combine(Application.persistentDataPath, "PlayerStats.json");

        private void Start()
        {
            PlayerStatsService.LoadPlayerStats(_playerStats);
            UpdateStatsDisplay();
        }

        private void UpdateStatsDisplay()
        {
            _rangeText.text = $"{_playerStats.AttackRange:F2}";
            _speedText.text = $"{_playerStats.Speed}";
            _damageText.text = $"{_playerStats.Damage}";
            _hpText.text = $"{_playerStats.MaxHP}";
        }
    }
}