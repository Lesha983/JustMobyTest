namespace JustMobyTest.UI
{
    using Gameplay;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class UIMenuScreen : AUIScreen
    {
        [Inject]
        private LevelService LevelService { get; set; }
        
        [SerializeField]
        private Button playButton;

        private void OnEnable()
        {
            playButton.onClick.AddListener(StartPlay);
        }
        
        private void OnDisable()
        {
            playButton.onClick.RemoveListener(StartPlay);
        }

        private void StartPlay()
        {
            LevelService.StartLevel();
        }
    }
}