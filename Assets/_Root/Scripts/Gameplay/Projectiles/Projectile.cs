namespace JustMobyTest.Gameplay
{
    using System;
    using Pools;
    using UnityEngine;
    using Zenject;

    public struct ProjectileSpawnInfo : IReinitializingInfo
    {
        public Vector2 Position;
        public float Damage;
    }
    
    public class Projectile : CustomPoolable<ProjectileSpawnInfo>, IDamager
    {
        [Inject]
        private DamageFactory DamageFactory { get; set; }
        
        private float _damage;
        
        public override void Reinitialize(ProjectileSpawnInfo info)
        {
            transform.position = info.Position;
            _damage = info.Damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.TryGetComponent<IDamageReceiver>(out var damageReceiver))
                return;
            
            damageReceiver.Receive(DamageFactory.Create(_damage));
            Despawn();
        }
    }
}