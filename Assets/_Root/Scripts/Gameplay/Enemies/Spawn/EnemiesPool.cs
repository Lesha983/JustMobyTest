namespace JustMobyTest.Gameplay
{
    using System.Collections.Generic;
    using Pools;
    using UnityEngine;

    public class EnemiesPool : UniversalPool<AEnemy, EnemySpawnInfo>
    {
        public List<AEnemy> ActiveEnemies => _active;
    }
}