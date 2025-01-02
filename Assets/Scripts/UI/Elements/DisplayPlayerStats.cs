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
        [SerializeField] private TMP_Text _rangeText;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _speedText;

        private string SaveFilePath => Path.Combine(Application.persistentDataPath, "PlayerStats.json");

        private void Start()
        {
            PlayerStatsService.LoadPlayerStats(_playerStats);
            UpdateStatsDisplay();
        }

        private void UpdateStatsDisplay()
        {
            _rangeText.text = $"Range: {_playerStats.AttackRange:F2}";
            _damageText.text = $"Damage: {_playerStats.Damage}";
            _speedText.text = $"Speed: {_playerStats.Speed}";
        }
    }
}