namespace JustMobyTest.Gameplay
{
    using UnityEngine;

    public interface IDamageReceiver
    {
        public void Receive(Damage damage);
        public Transform Transform { get; }
    }
}