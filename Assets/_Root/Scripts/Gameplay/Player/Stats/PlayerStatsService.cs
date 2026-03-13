namespace JustMobyTest.Gameplay
{
    using System;
    using System.Collections.Generic;
    using Save;
    using Zenject;

    public class PlayerStatsService
    {
        [Inject]
        private SaveData SaveData { get; set; }
        [Inject]
        private PlayerStatsCollection StatsCollection { get; set; }
        
        public event Action OnStatsChanged;
        
        public void UpgradeStats(APlayerStats stats, int upgradesCount)
        {
            switch (stats.Type)
            {
                case StatsType.Damage:
                    SaveData.DamageStatLevel += upgradesCount;
                    break;
                case StatsType.Health:
                    SaveData.HealthStatLevel += upgradesCount;
                    break;
                case StatsType.Speed:
                    SaveData.SpeedStatLevel += upgradesCount;
                    break;
            }

            OnStatsChanged?.Invoke();
        }

        public int GetStatLevel(APlayerStats stats)
        {
            switch (stats.Type)
            {
                case StatsType.Damage:
                    return SaveData.DamageStatLevel;
                case StatsType.Health:
                    return SaveData.HealthStatLevel;
                case StatsType.Speed:
                    return SaveData.SpeedStatLevel;
            }

            return -1;
        }

        public List<APlayerStats> GetAvailableStats()
        {
            var stats = new List<APlayerStats>();
            foreach (var stat in StatsCollection.Stats)
            {
                if(IsMaxLevel(stat))
                    continue;
                stats.Add(stat);
            }
            return stats;
        }

        public bool IsMaxLevel(APlayerStats stats)
        {
            switch (stats.Type)
            {
                case StatsType.Damage:
                    return SaveData.DamageStatLevel >= stats.MaxLevel;
                case StatsType.Health:
                    return SaveData.HealthStatLevel >= stats.MaxLevel;
                case StatsType.Speed:
                    return SaveData.SpeedStatLevel >= stats.MaxLevel;
            }
            
            return false;
        }
    }
}