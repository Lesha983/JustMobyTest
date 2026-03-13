namespace JustMobyTest
{
    using Gameplay;
    using Input;
    using Save;
    using UnityEngine;
    using Wallet;
    using Zenject;

    public class MainInstaller : MonoInstaller
    {
        [SerializeField] 
        private EnemiesPool enemiesPool;
        [SerializeField]
        private ProjectilesPool projectilesPool;
        [SerializeField]
        private DamageTextPool damageTextPool;
        [SerializeField]
        private Player player;
        
        private static readonly string SettingsPath = "Settings";
        private static readonly string EnemiesCollectionPath = SettingsPath + "/EnemiesCollection";
        private static readonly string DamageTextSettingsPath = SettingsPath + "/DamageTextSettings";
        private static readonly string PlayerSettingsPath = SettingsPath + "/PlayerSettings";
        private static readonly string PlayerStatsCollectionPath = SettingsPath + "/PlayerStatsCollection";

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
            Container.Bind<DamageTextSettings>().FromResources(DamageTextSettingsPath).AsSingle();
            Container.Bind<PlayerSettings>().FromResources(PlayerSettingsPath).AsSingle();
            Container.Bind<PlayerStatsCollection>().FromResources(PlayerStatsCollectionPath).AsSingle();
        }

        private void Bind()
        {
            Container.Bind<Player>().FromInstance(player).AsSingle();
            
            Container.BindInterfacesAndSelfTo<LevelService>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemiesPool>().FromInstance(enemiesPool).AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectilesPool>().FromInstance(projectilesPool).AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectileSpawner>().AsSingle();
            
            Container.Bind<DamageOperator>().AsSingle();
            Container.Bind<DamageFactory>().AsSingle();
            
            Container.Bind<DamageTextSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<DamageTextPool>().FromInstance(damageTextPool).AsSingle();
            
            Container.Bind<SaveData>().AsSingle();
            
            Container.Bind<PlayerStatsService>().AsSingle();
            Container.BindInterfacesTo<Wallet.Wallet>().AsSingle();
            // Container.BindInterfacesTo<BackPack>().AsSingle();
        }
    }
}