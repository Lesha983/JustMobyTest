namespace JustMobyTest.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Enemies/Enemies Collection", fileName = nameof(EnemiesCollection))]
    public class EnemiesCollection : ScriptableObject
    {
        [field: SerializeField]
        public List<AEnemy> Enemies { get; private set; }

        public AEnemy GetRandomEnemy()
        {
            return Enemies[Random.Range(0, Enemies.Count)];
        }
    }
}