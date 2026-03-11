namespace JustMobyTest.Gameplay
{
    using Pools;
    using UnityEngine;
    
    public struct EnemySpawnInfo : IReinitializingInfo
    {
        public Vector2 Position;
    }

    public abstract class AEnemy : CustomPoolable<EnemySpawnInfo>, IDamageReceiver
    {
        [SerializeField]
        private Health health;
        
        public override void Reinitialize(EnemySpawnInfo info)
        {
            transform.position = info.Position;
        }

        public void Receive(Damage damage)
        {
            health.TakeDamage(damage.Value);
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