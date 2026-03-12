namespace JustMobyTest.Gameplay
{
    using System;
    using UnityEngine;

    [Serializable]
    public abstract class APlayerStats
    {
        [field: SerializeField]
        public StatsType Type { get; private set; }
        
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract float GetValueBy(int level);
    }
    
    [Serializable]
    public class DamageStats : APlayerStats
    {
        public float DamageCoeff;
        public override string Name => "Damage";
        public override string Description => $"Increase damage by {DamageCoeff}";
        
        public override float GetValueBy(int level) => 
            MathF.Pow(DamageCoeff, level);
    }
    
    [Serializable]
    public class HealthStats : APlayerStats
    {
        public float HealthCoeff;
        public override string Name => "Health";
        public override string Description => $"Increase health by {HealthCoeff}";
        
        public override float GetValueBy(int level) => 
            MathF.Pow(HealthCoeff, level);
    }

    [Serializable]
    public class SpeedStats : APlayerStats
    {
        public float SpeedCoeff;
        public override string Name => "Speed";
        public override string Description => $"Increase speed by {SpeedCoeff}";
        
        public override float GetValueBy(int level) => 
            MathF.Pow(SpeedCoeff, level);
    }
}