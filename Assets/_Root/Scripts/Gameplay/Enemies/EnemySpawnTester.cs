namespace JustMobyTest.Gameplay
{
    using NaughtyAttributes;
    using UnityEngine;
    using Zenject;

    public class EnemySpawnTester : MonoBehaviour
    {
        [Inject]
        private EnemySpawner EnemySpawner { get; set; }

        [Button]
        public void Spawn()
        {
            EnemySpawner.Create(transform.position, 100f);
        }
    }
}