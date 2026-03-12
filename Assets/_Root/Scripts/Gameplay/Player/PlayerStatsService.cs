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
        private Player Player { get; set; }
        
        public event Action OnStatsChanged;
        
        public void UpdateStats(APlayerStats stats)
        {
            if (stats is DamageStats damageStats)
            {
                SaveData.DamageStatLevel++;
                Player.SetDamageCoeff(damageStats.GetValueBy(SaveData.DamageStatLevel));
            }
            else if (stats is HealthStats healthStats)
            {
                SaveData.HealthStatLevel++;
                Player.SetHealthCoeff(healthStats.GetValueBy(SaveData.HealthStatLevel));
            }
            else if (stats is SpeedStats speedStats)
            {
                SaveData.SpeedStatLevel++;
                Player.SetSpeedCoeff(speedStats.GetValueBy(SaveData.SpeedStatLevel));
            }

            OnStatsChanged?.Invoke();
        }
    }
}