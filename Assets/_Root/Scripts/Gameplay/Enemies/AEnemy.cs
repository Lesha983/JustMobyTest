namespace JustMobyTest.Gameplay
{
    using Pools;
    using UnityEngine;
    using Zenject;

    public struct EnemySpawnInfo : IReinitializingInfo
    {
        public Vector3 Position;
        public float Health;
        public float Speed;
        public float Damage;
    }

    public abstract class AEnemy : CustomPoolable<EnemySpawnInfo>, IDamageReceiver
    {
        [Inject]
        private DamageTextSpawner DamageTextSpawner { get; set; }
        
        [SerializeField]
        protected Health health;
        [SerializeField]
        protected Agent agent;
        [SerializeField]
        protected Gun gun;
        public Transform Transform => transform;
        
        public override void Reinitialize(EnemySpawnInfo info)
        {
            transform.position = info.Position;
            health.Setup(info.Health);
            agent.Setup(info.Speed);
            gun.Setup(info.Damage);
            SetStartState();
        }

        public virtual void Receive(Damage damage)
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

        protected virtual void SetStartState()
        {
        }
        
        protected abstract void Die();
    }
}