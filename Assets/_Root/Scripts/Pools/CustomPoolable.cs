namespace JustMobyTest.Pools
{
    using UnityEngine;
    using Zenject;

    public abstract class CustomPoolable<TInfo> : MonoBehaviour where TInfo : IReinitializingInfo
    {
        [Inject]
        private IPool<TInfo> Pool { get; set; }

        public abstract void Reinitialize(TInfo info);

        public void Despawn()
        {
            Pool.Despawn(this);
        }
    }
}