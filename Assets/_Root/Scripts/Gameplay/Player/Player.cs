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
        [Inject]
        private PlayerSettings Settings { get; set; }
        [Inject]
        private PlayerStatsService StatsService { get; set; }
        [Inject]
        private PlayerStatsCollection StatsCollection { get; set; }
        [Inject]
        private SaveData SaveData { get; set; }

        public event Action OnDeath;
        public event Action OnStartAim;
        public event Action OnEndAim;
        
        [SerializeField]
        private Agent agent;
        [SerializeField]
        private Health health;
        [SerializeField]
        private Gun gun;
        [SerializeField]
        private LayerMask shootableLayers;
        [Space]
        [SerializeField]
        private Transform aim;
        [SerializeField]
        private Transform hand;
        [SerializeField]
        private Vector3 startPosition;
        
        private float _currentSensitivity;
        private float _verticalRotation;
        private Camera _camera;
        private float _maxDistance = 100f;
        private Vector2 _aimClamp;
        
        public Health Health => health;
        public float CurrentDamage => gun.CurrentDamage;
        public float StartDamage => gun.StartDamage;
        public Transform Transform => transform;

        public void Setup()
        {
            health.Setup(Settings.Health);
            gun.Setup(Settings.Damage);
            agent.Setup(Settings.Speed);
            UpdateStats();
            transform.position = startPosition;
            EndAim();
        }
        
        public void Receive(Damage damage)
        {
            health.TakeDamage(damage.Value);
        }

        public void Move(Vector2 value)
        {
            var direction = transform.right * value.x + transform.forward * value.y;
            agent.Move(direction);
        }

        public void StartAttack()
        {
            Vector3 targetPoint;

            var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out var hit, _maxDistance, shootableLayers))
                targetPoint = hit.point;
            else
                targetPoint = ray.origin + ray.direction * _maxDistance;
            
            var dir = (targetPoint - gun.ShootPoint.position).normalized;
            
            gun.StartAttack(dir);
        }

        public void EndAttack()
        {
            gun.EndAttack();
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
            _verticalRotation = Mathf.Clamp(_verticalRotation, _aimClamp.x, _aimClamp.y);

            aim.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
            hand.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }

        private void Awake()
        {
            _camera = Camera.main;
            _aimClamp = Settings.VerticalRotationClamp;
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
                        gun.SetCoefficient(stats.GetValueBy(SaveData.DamageStatLevel));
                        break;
                    case StatsType.Health:
                        health.SetHealthCoefficient(stats.GetValueBy(SaveData.HealthStatLevel));
                        break;
                    case StatsType.Speed:
                        agent.SetSpeedCoefficient(stats.GetValueBy(SaveData.SpeedStatLevel));
                        break;
                }
            }
        }

        private void Die()
        {
            OnDeath?.Invoke();
        }
    }
}