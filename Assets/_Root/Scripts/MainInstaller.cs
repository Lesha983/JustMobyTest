namespace JustMobyTest
{
    using Gameplay;
    using Input;
    using UnityEngine;
    using Zenject;

    public class MainInstaller : MonoInstaller
    {
        [SerializeField] 
        private EnemiesPool enemiesPool;
        [SerializeField]
        private ProjectilesPool projectilesPool;
        
        private static readonly string SettingsPath = "Settings";
        private static readonly string EnemiesCollectionPath = SettingsPath + "/EnemiesCollection";

        public override void InstallBindings()
        {
            BindSettings();
            Bind();
        }

        private void BindSettings()
        {
            Container.BindInterfacesAndSelfTo<InputProvider>().AsSingle();
#if UNITY_ANDROID || UNITY_IOS
                Container.BindInterfacesAndSelfTo<UIHandler>().AsSingle();
#else
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
#endif
            
            Container.Bind<EnemiesCollection>().FromResources(EnemiesCollectionPath).AsSingle();
        }

        private void Bind()
        {
            Container.BindInterfacesAndSelfTo<EnemiesPool>().FromInstance(enemiesPool).AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectilesPool>().FromInstance(projectilesPool).AsSingle();
        }
    }
}