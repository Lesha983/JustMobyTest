namespace JustMobyTest.Gameplay
{
    using UnityEngine;
    using Zenject;

    public class Gun : MonoBehaviour
    {
        [Inject]
        private ProjectileSpawner ProjectileSpawner { get; set; }
        
        [SerializeField]
        private Transform barrel;
        [SerializeField]
        private Projectile projectilePrefab;
        
        public float StartDamage => _startDamage;
        public float CurrentDamage => _currentDamage;
        public Transform ShootPoint => barrel;
        
        private float _startDamage;
        private float _currentDamage;
        
        public void Setup(float damage)
        {
            _startDamage = damage;
            _currentDamage = damage;
        }
        
        public void SetCoefficient(float coeff)
        {
            _currentDamage = _startDamage * coeff;
        }
        
        public void StartAttack(Vector3 direction)
        {
            ProjectileSpawner.Spawn(projectilePrefab, new ProjectileSpawnInfo()
            {
                Position = barrel.position,
                Direction = direction,
                Damage = _currentDamage
            });
        }

        public void EndAttack()
        {
            
        }
    }
}