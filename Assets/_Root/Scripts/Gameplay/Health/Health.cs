namespace JustMobyTest.Gameplay
{
    using System;
    using UnityEngine;

    public class Health : MonoBehaviour
    {
        public event Action OnChanged;
        public event Action OnDeath;
        
        public float MaxHealth => _maxHealth;
        public float HealthAmount { get; private set; }
        public bool IsEmpty => HealthAmount <= 0;
        public bool IsFull => HealthAmount >= MaxHealth;
        
        private float _startHealth;
        private float _maxHealth;

        public void Setup(float health)
        {
            _startHealth = health;
            _maxHealth = health;
            HealthAmount = health;
            OnChanged?.Invoke();
        }

        public void SetHealthCoefficient(float coeff)
        {
            var newHealth = _startHealth * coeff;
            var diff = newHealth - _maxHealth;
            HealthAmount += diff;
            _maxHealth = newHealth;
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