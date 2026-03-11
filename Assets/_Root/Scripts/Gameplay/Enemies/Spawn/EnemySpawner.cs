namespace JustMobyTest.Gameplay
{
    using UnityEngine;
    using Zenject;

    public class EnemySpawner
    {
        [Inject]
        private EnemiesCollection EnemiesCollection { get; set; }
        [Inject]
        private EnemiesPool Pool { get; set; }

        public AEnemy Create(AEnemy prefab, Vector3 position)
        {
            var spawnInfo = new EnemySpawnInfo()
            {
                Position = position
            };
            
            return Pool.Spawn(prefab, spawnInfo);
        }

        public AEnemy Create(Vector3 position)
        {
            return Create(EnemiesCollection.GetRandomEnemy(), position);
        }
    }
}