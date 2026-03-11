namespace JustMobyTest.Gameplay
{
    using Zenject;

    public class ProjectileSpawner
    {
        [Inject]
        private ProjectilesPool Pool { get; set; }
        
        public Projectile Spawn(Projectile prefab, ProjectileSpawnInfo info)
        {
            return Pool.Spawn(prefab, info);
        }
    }
}