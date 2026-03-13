namespace JustMobyTest.Gameplay
{
    using System;
    using Input;
    using NaughtyAttributes;
    using Save;
    using UnityEngine;
    using UnityEngine.AI;
    using Zenject;

    public class Player : MonoBehaviour, IDamageReceiver
    {
        // [Inject]
        // private IInputHandler InputHandler { get; set; }
        [Inject]
        private ProjectileSpawner ProjectileSpawner { get; set; }
        [Inject]
        private DamageFactory DamageFactory { get; set; }
        [Inject]
        private PlayerSettings Settings { get; set; }
        [Inject]
        private PlayerStatsService StatsService { get; set; }
        [Inject]
        private PlayerStatsCollection StatsCollection { get; set; }
        [Inject]
        private SaveData SaveData { get; set; }
        
        public event Action OnStartAim;
        public event Action OnEndAim;
        
        [SerializeField]
        private NavMeshAgent agent;
        [SerializeField]
        private Health health;
        [Space]
        [SerializeField]
        private Transform aim;
        [SerializeField]
        private Transform hand;
        [SerializeField]
        private Transform gunBarrel;
        [SerializeField]
        private Projectile projectilePrefab;
        
        private float _currentSensitivity;
        private float _verticalRotation;
        private float _currentDamage;
        private float _currentSpeed;
        
        public Health Health => health;
        public float CurrentDamage => _currentDamage;
        public float StartDamage => Settings.Damage;

        public void Receive(Damage damage)
        {
            health.TakeDamage(damage.Value);
        }

        [Button]
        public void TakeDamage()
        {
            Receive(DamageFactory.Create(10f));
        }

        // public void SetDamageCoeff(float damageCoeff)
        // {
        //     _currentDamage = Settings.Damage * damageCoeff;
        // }
        //
        // public void SetSpeedCoeff(float speedCoeff)
        // {
        //     _currentSpeed = Settings.Speed * speedCoeff;
        // }
        //
        // public void SetHealthCoeff(float healthCoeff)
        // {
        //     health.SetHealthCoefficient(healthCoeff);
        // }

        public void Move(Vector2 value)
        {
            var direction = transform.right * value.x + transform.forward * value.y;
            agent.Move(direction * _currentSpeed * Time.deltaTime);
        }

        public void Attack()
        {
            Vector3 targetPoint;

            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out var hit, 100f))
                targetPoint = hit.point;
            else
                targetPoint = ray.origin + ray.direction * 100f;
            
            var dir = (targetPoint - gunBarrel.position).normalized;
            ProjectileSpawner.Spawn(projectilePrefab, new ProjectileSpawnInfo()
            {
                Position = gunBarrel.position,
                Direction = dir,
                Damage = _currentDamage
            });
        }

        public void StartAim()
        {
            _currentSensitivity = Settings.AimSensitivity;
            OnStartAim?.Invoke();
        }

        public void EndAim()
        {
            _currentSensitivity = Settings.Sensitivity;
            OnEndAim?.Invoke();
        }

        public void Rotate(Vector2 value)
        {
            var mouseX = value.x * _currentSensitivity;
            var mouseY = value.y * _currentSensitivity;

            // вращение игрока (горизонтально)
            transform.Rotate(Vector3.up * mouseX);

            // вращение прицела (вертикально)
            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -70f, 70f);

            aim.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
            hand.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }

        private void Awake()
        {
            health.Setup(Settings.Health);
            UpdateStats();
        }

        private void OnEnable()
        {
            health.OnDeath += Die;
            StatsService.OnStatsChanged += UpdateStats;
        }

        private void OnDisable()
        {
            health.OnDeath -= Die;
            StatsService.OnStatsChanged -= UpdateStats;
        }

        private void Start()
        {
            _currentSensitivity = Settings.Sensitivity;
        }

        private void UpdateStats()
        {
            foreach (var stats in StatsCollection.Stats)
            {
                switch (stats.Type)
                {
                    case StatsType.Damage:
                        _currentDamage = Settings.Damage * stats.GetValueBy(SaveData.DamageStatLevel);
                        break;
                    case StatsType.Health:
                        health.SetHealthCoefficient(stats.GetValueBy(SaveData.HealthStatLevel));
                        break;
                    case StatsType.Speed:
                        _currentSpeed = Settings.Speed * stats.GetValueBy(SaveData.SpeedStatLevel);
                        break;
                }
            }
        }

        private void Die()
        {
            Debug.Log($"Player died!");
        }
    }
}