namespace JustMobyTest.Gameplay
{
    using System;
    using UnityEngine;

    public class Health : MonoBehaviour
    {
        public event Action OnChanged;
        public event Action OnDeath;
        
        public float MaxHealth => _health;
        public float HealthAmount { get; private set; }
        public bool IsEmpty => HealthAmount <= 0;
        public bool IsFull => HealthAmount >= MaxHealth;
        
        private float _health;

        public void Setup(float health)
        {
            _health = health;
            HealthAmount = health;
            OnChanged?.Invoke();
        }
        
        public void TakeDamage(float amount)
        {
            HealthAmount -= amount;
            OnChanged?.Invoke();
            if (IsEmpty)
                OnDeath?.Invoke();
        }
    }
}