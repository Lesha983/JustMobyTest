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
        [Inject]
        private Player Player { get; set; }
        
        public event Action OnLevelStart;
        public event Action OnLevelStop;

        public void Initialize()
        {
            InputHandler.Disable();
            Player.OnDeath += EndLevel;
            // InputHandler.OnPause += Pause;
            // InputHandler.OnResume += Resume;
        }

        public void Dispose()
        {
            Player.OnDeath -= EndLevel;
            // InputHandler.OnPause -= Pause;
            // InputHandler.OnResume -= Resume;
        }

        public void StartLevel()
        {
            Time.timeScale = 1f;
            EnemyService.Setup();
            InputHandler.Enable();
            Player.Setup();
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
            EnemyService.DestroyAllEnemies();
            Time.timeScale = 0f;
            InputHandler.Disable();
            OnLevelStop?.Invoke();
            PlayerPrefs.DeleteAll();
        }
        
    }
}