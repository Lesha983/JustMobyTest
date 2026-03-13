namespace JustMobyTest.Gameplay
{
    using System;
    using UnityEngine;

    public class DetectedZone : MonoBehaviour
    {
        public event Action<IDamageReceiver> OnDetected;
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"DetectedZone - OnTriggerEnter: {other.name}");
            if (other.TryGetComponent<IDamageReceiver>(out var damageReceiver))
                OnDetected?.Invoke(damageReceiver);
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"DetectedZone - OnCollisionEnter: {other.gameObject.name}");
            if (other.gameObject.TryGetComponent<IDamageReceiver>(out var damageReceiver))
                OnDetected?.Invoke(damageReceiver);
        }
    }
}