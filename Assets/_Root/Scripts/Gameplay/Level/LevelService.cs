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
        private IInputHandler InputHandler { get; set; }
        
        public event Action OnLevelStart;
        public event Action OnLevelStop;

        public void Initialize()
        {
            InputHandler.Disable();
        }

        public void StartLevel()
        {
            EnemyService.TryCreateNewEnemies();
            InputHandler.Enable();
            OnLevelStart?.Invoke();
        }

        public void Pause()
        {
            InputHandler.Disable();
        }
        
        public void Resume()
        {
            InputHandler.Enable();
        }

        public void EndLevel()
        {
            InputHandler.Disable();
            OnLevelStop?.Invoke();
        }
    }
}