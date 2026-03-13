namespace JustMobyTest.Gameplay
{
    using System;
    using UnityEngine;

    public class DetectedZone : MonoBehaviour
    {
        public event Action<IDamageReceiver> OnDetected;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageReceiver>(out var damageReceiver))
                OnDetected?.Invoke(damageReceiver);
        }
    }
}