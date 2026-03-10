namespace JustMobyTest.Gameplay
{
    using Input;
    using UnityEngine;
    using Zenject;

    public class Player : MonoBehaviour
    {
        [Inject]
        private IInputHandler InputHandler { get; set; }
        
        [SerializeField]
        private CharacterController controller;
        [Space]
        [SerializeField]
        private Transform aim;
        [SerializeField]
        private Transform hand;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float sensitivity = 1.5f;
        [SerializeField]
        private float aimSensitivity = 0.75f;
        
        private float _currentSensitivity;
        private float _verticalRotation;

        private void OnEnable()
        {
            InputHandler.OnMove += OnMove;
            InputHandler.OnAttack += OnAttack;
            InputHandler.OnRotate += OnRotate;
            InputHandler.OnStartAim += OnStartAim;
            InputHandler.OnEndAim += OnEndAim;
        }

        private void OnDisable()
        {
            InputHandler.OnMove -= OnMove;
            InputHandler.OnAttack -= OnAttack;
            InputHandler.OnRotate -= OnRotate;
            InputHandler.OnStartAim -= OnStartAim;
            InputHandler.OnEndAim -= OnEndAim;
        }
        
        private void Start()
        {
            _currentSensitivity = sensitivity;
        }

        private void OnMove(Vector2 value)
        {
            var direction = transform.right * value.x + transform.forward * value.y;
            controller.Move(direction * speed * Time.deltaTime);
            // transform.position += direction * speed * Time.deltaTime;
        }

        private void OnAttack()
        {
            Debug.Log($"Attack!");
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