namespace JustMobyTest.Gameplay
{
    using System;
    using Input;
    using Zenject;

    public class LevelService : IInitializable
    {
        [Inject]
        private EnemyService EnemyService { get; set; }
        [Inject]
        private IInputProvider InputProvider { get; set; }
        
        public event Action OnLevelStart;
        public event Action OnLevelStop;

        public void Initialize()
        {
            InputProvider.Disable();
        }

        public void StartLevel()
        {
            EnemyService.TryCreateNewEnemies();
            InputProvider.Enable();
            OnLevelStart?.Invoke();
        }

        public void Pause()
        {
            InputProvider.Disable();
        }

        public void EndLevel()
        {
            InputProvider.Disable();
            OnLevelStop?.Invoke();
        }
    }
}