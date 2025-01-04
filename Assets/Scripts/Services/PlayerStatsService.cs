using System.IO;
using UnityEngine;
using CodeBase.Player;

namespace CodeBase.Service
{
    public static class PlayerStatsService
    {
        private static string SaveFilePath => Path.Combine(Application.persistentDataPath, "PlayerStats.json");

        public static void SavePlayerStats(PlayerStatsSO playerConfig)
        {
            string json = JsonUtility.ToJson(playerConfig);
            File.WriteAllText(SaveFilePath, json);
        }

        public static void LoadPlayerStats(PlayerStatsSO playerConfig)
        {
            if (File.Exists(SaveFilePath))
            {
                string json = File.ReadAllText(SaveFilePath);
                JsonUtility.FromJsonOverwrite(json, playerConfig);
                Debug.Log($"Player stats loaded from: {SaveFilePath}");
            }
            else
                Debug.LogWarning($"No player stats found at: {SaveFilePath}");
        }
    }
}