using CodeBase.Player;

namespace CodeBase.Service
{
    public static class UpgradeService
    {
        public const int MaxAmountOfKnives = 5;
        private const float RangeGrowth = 0.05f;
        private const float DamageGrowth = 0.10f;
        private const float MovementSpeedGrowth = 0.02f;
        private const float MaxHPGrowth = 0.10f;
        private const float RegenerationGrowth = 0.02f;

        public static void IncreaseRange(PlayerStatsSO playerConfig)
        {
            playerConfig.AttackRange = CalculateStat(playerConfig.AttackRange, RangeGrowth);
            PlayerStatsService.SavePlayerStats(playerConfig);
        }

        public static void IncreasePower(PlayerStatsSO playerConfig)
        {
            playerConfig.Damage = CalculateStat(playerConfig.Damage, DamageGrowth);
            PlayerStatsService.SavePlayerStats(playerConfig);
        }

        public static void IncreaseSpeed(PlayerStatsSO playerConfig)
        {
            playerConfig.Speed = CalculateStat(playerConfig.Speed, MovementSpeedGrowth);
            PlayerStatsService.SavePlayerStats(playerConfig);
        }

        public static void IncreaseMaxHP(PlayerStatsSO playerConfig)
        {
            playerConfig.MaxHP = CalculateStat(playerConfig.MaxHP, MaxHPGrowth);
            PlayerStatsService.SavePlayerStats(playerConfig);
        }

        public static void IncreaseRegenerationSpeed(PlayerStatsSO playerConfig)
        {
            playerConfig.RegenerationSpeed = CalculateStat(playerConfig.RegenerationSpeed, RegenerationGrowth);
            PlayerStatsService.SavePlayerStats(playerConfig);
        }

        public static void IncreaseAmountOfKnives(PlayerStatsSO playerConfig)
        {
            if (playerConfig.AmountOfKnives < MaxAmountOfKnives)
                playerConfig.AmountOfKnives++;
            else
                playerConfig.AmountOfKnives = MaxAmountOfKnives;
        }
        private static float CalculateStat(float baseValue, float growthRate) => baseValue * (1 + growthRate);
    }
}