namespace JustMobyTest
{
    using Gameplay;
    using Input;
    using Zenject;

    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputProvider>().AsSingle();
#if UNITY_ANDROID || UNITY_IOS
                Container.BindInterfacesAndSelfTo<UIHandler>().AsSingle();
#else
                Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
#endif
        }
    }
}