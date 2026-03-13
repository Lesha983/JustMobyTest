namespace JustMobyTest.Gameplay
{
    using System;
    using Input;
    using UnityEngine;
    using Zenject;

    public class LevelService : IInitializable, IDisposable
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

        public void Dispose()
        {
            
        }

        public void StartLevel()
        {
            Time.timeScale = 1f;
            EnemyService.TryCreateNewEnemies();
            InputHandler.Enable();
            OnLevelStart?.Invoke();
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            InputHandler.Disable();
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            InputHandler.Enable();
        }

        public void EndLevel()
        {
            Time.timeScale = 0f;
            InputHandler.Disable();
            OnLevelStop?.Invoke();
        }
    }
}