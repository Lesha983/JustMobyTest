namespace JustMobyTest.Gameplay
{
    using Input;
    using UnityEngine;
    using UnityEngine.AI;
    using Zenject;

    public class Player : MonoBehaviour, IDamageReceiver
    {
        [Inject]
        private IInputHandler InputHandler { get; set; }
        [Inject]
        private ProjectileSpawner ProjectileSpawner { get; set; }

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
        private float speed;
        [SerializeField]
        private float sensitivity = 1.5f;
        [SerializeField]
        private float aimSensitivity = 0.75f;
        [SerializeField]
        private Projectile projectilePrefab;
        [SerializeField]
        private float damage;
        
        private float _currentSensitivity;
        private float _verticalRotation;

        public void Receive(Damage damage)
        {
            
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
            _currentSensitivity = sensitivity;
        }

        private void Die()
        {
            
        }

        private void OnMove(Vector2 value)
        {
            var direction = transform.right * value.x + transform.forward * value.y;
            agent.Move(direction * speed * Time.deltaTime);
            // controller.Move(direction * speed * Time.deltaTime);
            // transform.position += direction * speed * Time.deltaTime;
        }

        private void OnAttack()
        {
            ProjectileSpawner.Spawn(projectilePrefab, new ProjectileSpawnInfo()
            {
                Position = gunBarrel.position,
                Direction = gunBarrel.forward,
                Damage = damage
            });
        }

        private void OnStartAim()
        {
            _currentSensitivity = aimSensitivity;
        }

        private void OnEndAim()
        {
            _currentSensitivity = sensitivity;
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