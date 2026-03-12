namespace JustMobyTest.Gameplay
{
    using UnityEngine;
    using Zenject;

    public class DamageTextSpawner
    {
        [Inject]
        private DamageTextPool Pool { get; set; }
        [Inject]
        private DamageTextSettings Settings { get; set; }
        
        public void Spawn(Damage damage, Vector3 position)
        {
            var info = new DamageTextInfo()
            {
                Position = position,
                Amount = damage.Value,
            };
            Pool.Spawn(Settings.DamageTextPrefab, info);
        }
    }
}