namespace JustMobyTest.Gameplay
{
    using System;
    using UnityEngine;

    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float healthAmount;

        public event Action OnChanged;
        public event Action OnDeath;
        
        public float MaxHealth => healthAmount;
        public float HealthAmount { get; private set; }
        public bool IsEmpty => HealthAmount <= 0;
        public bool IsFull => HealthAmount >= MaxHealth;

        public void Setup()
        {
            HealthAmount = MaxHealth;
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