namespace JustMobyTest.Gameplay
{
    using System;
    using UnityEngine;
    using UnityEngine.AI;
    using Zenject;
    using Random = UnityEngine.Random;

    public class EnemyService : IInitializable, IDisposable
    {
        [Inject]
        private Player Player { get; set; }
        [Inject]
        private EnemySpawner EnemySpawner { get; set; }
        
        private float _spawnRange = 50f;
        private int _maxEnemies = 5;

        public void Initialize()
        {
            EnemySpawner.OnDespawn += TryCreateNewEnemies;
        }

        public void Dispose()
        {
            EnemySpawner.OnDespawn -= TryCreateNewEnemies;
        }

        public void TryCreateNewEnemies()
        {
            var targetCount = Mathf.Clamp(_maxEnemies - EnemySpawner.ActiveEnemies.Count, 0, _maxEnemies);
            for (var i = 0; i < targetCount; i++)
            {
                var position = GetSpawnPosition();
                var health = Player.StartDamage * Random.Range(1, 11);
                EnemySpawner.Create(position, health);
            }
        }

        private Vector3 GetSpawnPosition()
        {
            var navMeshData = NavMesh.CalculateTriangulation();

            var index = Random.Range(0, navMeshData.indices.Length - 3);

            var v1 = navMeshData.vertices[navMeshData.indices[index]];
            var v2 = navMeshData.vertices[navMeshData.indices[index + 1]];
            var v3 = navMeshData.vertices[navMeshData.indices[index + 2]];

            var point = (v1 + v2 + v3) / 3f;
            
            return point;
        }
    }
}