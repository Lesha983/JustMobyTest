namespace JustMobyTest.Gameplay
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    public class EnemySpawner : IInitializable, IDisposable
    {
        [Inject]
        private EnemiesCollection EnemiesCollection { get; set; }
        [Inject]
        private EnemiesPool Pool { get; set; }
        
        public event Action OnSpawn;
        public event Action OnDespawn;
        
        public List<AEnemy> ActiveEnemies => Pool.ActiveEnemies;
        
        public void Initialize()
        {
            Pool.OnSpawn += Spawn;
            Pool.OnDespawn += Despawn;
        }

        public void Dispose()
        {
            Pool.OnSpawn -= Spawn;
            Pool.OnDespawn -= Despawn;
        }

        public AEnemy Create(AEnemy prefab, Vector3 position, float health = 100f, float speed = 5f, float damage = 10f)
        {
            var spawnInfo = new EnemySpawnInfo()
            {
                Position = position,
                Health = health,
                Speed = speed,
                Damage = damage
            };
            
            return Pool.Spawn(prefab, spawnInfo);
        }

        public AEnemy Create(Vector3 position, float health)
        {
            return Create(EnemiesCollection.GetRandomEnemy(), position, health);
        }

        public void DeactivateAll()
        {
            Pool.DeactivateAll();
        }

        private void Spawn(AEnemy enemy)
        {
            OnSpawn?.Invoke();
        }

        private void Despawn(AEnemy enemy)
        {
            OnDespawn?.Invoke();
        }
    }
}