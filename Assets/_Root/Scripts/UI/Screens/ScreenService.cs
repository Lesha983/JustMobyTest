namespace JustMobyTest.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using Gameplay;
    using UnityEngine;
    using Zenject;

    public class ScreenService : MonoBehaviour
    {
        [Inject]
        private LevelService LevelService { get; set; }
        [Inject]
        private IInstantiator Instantiator { get; set; }
        
        [SerializeField]
        private List<AUIScreen> screens;
        
        private AUIScreen _activeScreen;

        public T SwitchScreenTo<T>() where T : AUIScreen
        {
            var screen = GetScreenByType<T>();
            if(screen.IsShown)
                return screen;

            if (_activeScreen != null)
                _activeScreen.Close();
            return CreateScreen(screen);
        }

        private void Start()
        {
            SwitchScreenTo<UIMenuScreen>();
        }

        private void OnEnable()
        {
            LevelService.OnLevelStart += ShowGameScreen;
            LevelService.OnLevelStop += ShowLoseScreen;
        }

        private void OnDisable()
        {
            LevelService.OnLevelStart -= ShowGameScreen;
            LevelService.OnLevelStop -= ShowLoseScreen;
        }

        private void ShowGameScreen()
        {
            SwitchScreenTo<UIGameScreen>();
        }

        private void ShowLoseScreen()
        {
            SwitchScreenTo<UILoseScreen>();
        }


        private T CreateScreen<T>(T screen) where T : AUIScreen
        {
            var instance = Instantiator.InstantiatePrefabForComponent<T>(screen, transform);
            instance.Show();
            _activeScreen = instance;
            return instance;
        }
        
        private T GetScreenByType<T>() where T : AUIScreen => 
            (T)screens.FirstOrDefault(x => x.GetType() == typeof(T));
    }
}