namespace JustMobyTest.Gameplay
{
    using Zenject;

    public class LevelService
    {
        [Inject]
        private EnemyService EnemyService { get; set; }
        
        public void StartLevel()
        {
            EnemyService.TryCreateNewEnemies();
        }

        public void Pause()
        {
            
        }

        public void EndLevel()
        {
            
        }
    }
}