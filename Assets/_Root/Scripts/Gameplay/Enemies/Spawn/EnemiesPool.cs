namespace JustMobyTest.Gameplay
{
    using System.Collections.Generic;
    using Pools;
    using UnityEngine;

    public class EnemiesPool : UniversalPool<AEnemy, EnemySpawnInfo>
    {
        public List<AEnemy> ActiveEnemies => _active;
        
        public void DeactivateAll()
        {
            var enemies = new List<AEnemy>();
            enemies.AddRange(_active);
            foreach (var enemy in enemies)
            {
                enemy.Despawn();
            }
        }
    }
}