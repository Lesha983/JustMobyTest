namespace JustMobyTest.UI
{
    using System;
    using Gameplay;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class UIPlayerHealthBar : MonoBehaviour
    {
        [Inject]
        private Player Player { get; set; }
        
        [SerializeField]
        private Slider healthBar;
        
        private Health _health;

        private void Awake()
        {
            _health = Player.Health;
        }

        private void OnEnable()
        {
            _health.OnChanged += UpdateView;
        }
        
        private void OnDisable()
        {
            _health.OnChanged -= UpdateView;
        }

        private void UpdateView()
        {
            var maxValue = _health.MaxHealth;
            var currentValue = _health.HealthAmount;
            
            healthBar.value = currentValue / maxValue;
        }
    }
}