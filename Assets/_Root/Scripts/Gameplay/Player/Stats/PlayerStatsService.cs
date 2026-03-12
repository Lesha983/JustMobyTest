namespace JustMobyTest.Gameplay
{
    using System;
    using Save;
    using Zenject;

    public class PlayerStatsService
    {
        [Inject]
        private SaveData SaveData { get; set; }
        [Inject]
        private PlayerStatsCollection StatsCollection { get; set; }
        
        public event Action OnStatsChanged;
        
        public void UpgradeStats(APlayerStats stats)
        {
            switch (stats.Type)
            {
                case StatsType.Damage:
                    SaveData.DamageStatLevel++;
                    break;
                case StatsType.Health:
                    SaveData.HealthStatLevel++;
                    break;
                case StatsType.Speed:
                    SaveData.SpeedStatLevel++;
                    break;
            }

            OnStatsChanged?.Invoke();
        }
        
        
    }
}