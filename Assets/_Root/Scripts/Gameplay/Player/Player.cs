namespace JustMobyTest.Gameplay
{
    using Input;
    using NaughtyAttributes;
    using UnityEngine;
    using UnityEngine.AI;
    using Zenject;

    public class Player : MonoBehaviour, IDamageReceiver
    {
        [Inject]
        private IInputHandler InputHandler { get; set; }
        [Inject]
        private ProjectileSpawner ProjectileSpawner { get; set; }
        [Inject]
        private DamageFactory DamageFactory { get; set; }
        [Inject]
        private PlayerSettings Settings { get; set; }

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

        public void Receive(Damage damage)
        {
            health.TakeDamage(damage.Value);
        }

        [Button]
        public void TakeDamage()
        {
            Receive(DamageFactory.Create(10f));
        }

        public void SetDamageCoeff(float damageCoeff)
        {
            _currentDamage = Settings.Damage * damageCoeff;
        }
        
        public void SetSpeedCoeff(float speedCoeff)
        {
            _currentSpeed = Settings.Speed * speedCoeff;
        }

        public void SetHealthCoeff(float healthCoeff)
        {
            health.SetHealthCoefficient(healthCoeff);
        }

        private void OnEnable()
        {
            InputHandler.OnMove += OnMove;
            InputHandler.OnAttack += OnAttack;
            InputHandler.OnRotate += OnRotate;
            InputHandler.OnStartAim += OnStartAim;
            InputHandler.OnEndAim += OnEndAim;

            health.OnDeath += Die;
        }

        private void OnDisable()
        {
            InputHandler.OnMove -= OnMove;
            InputHandler.OnAttack -= OnAttack;
            InputHandler.OnRotate -= OnRotate;
            InputHandler.OnStartAim -= OnStartAim;
            InputHandler.OnEndAim -= OnEndAim;

            health.OnDeath -= Die;
        }

        private void Start()
        {
            _currentSensitivity = Settings.Sensitivity;
            health.Setup(100);
        }

        private void Die()
        {
            Debug.Log($"Player died!");
        }

        private void OnMove(Vector2 value)
        {
            var direction = transform.right * value.x + transform.forward * value.y;
            agent.Move(direction * _currentSpeed * Time.deltaTime);
            // controller.Move(direction * speed * Time.deltaTime);
            // transform.position += direction * speed * Time.deltaTime;
        }

        private void OnAttack()
        {
            ProjectileSpawner.Spawn(projectilePrefab, new ProjectileSpawnInfo()
            {
                Position = gunBarrel.position,
                Direction = gunBarrel.forward,
                Damage = _currentDamage
            });
        }

        private void OnStartAim()
        {
            _currentSensitivity = Settings.AimSensitivity;
        }

        private void OnEndAim()
        {
            _currentSensitivity = Settings.Sensitivity;
        }

        private void OnRotate(Vector2 value)
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
    }
}