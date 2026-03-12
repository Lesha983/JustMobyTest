namespace JustMobyTest.Gameplay
{
    using Pools;
    using UnityEngine;
    using Zenject;

    public struct EnemySpawnInfo : IReinitializingInfo
    {
        public Vector3 Position;
        public float Health;
    }

    public abstract class AEnemy : CustomPoolable<EnemySpawnInfo>, IDamageReceiver
    {
        [Inject]
        private DamageTextSpawner DamageTextSpawner { get; set; }
        
        [SerializeField]
        private Health health;
        
        public override void Reinitialize(EnemySpawnInfo info)
        {
            transform.position = info.Position;
            health.Setup(info.Health);
        }

        public void Receive(Damage damage)
        {
            health.TakeDamage(damage.Value);
            DamageTextSpawner.Spawn(damage, transform.position);
        }

        protected virtual void OnEnable()
        {
            health.OnDeath += Die;
        }
        
        protected virtual void OnDisable()
        {
            health.OnDeath -= Die;
        }

        protected abstract void Die();
    }
}