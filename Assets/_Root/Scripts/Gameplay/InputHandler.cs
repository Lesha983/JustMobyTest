namespace JustMobyTest.Gameplay
{
    using System;
    using Input;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Zenject;

    public class InputHandler : IInputHandler, IInitializable, IDisposable, ITickable
    {
        [Inject]
        private IInputProvider InputProvider { get; set; }
        [Inject]
        private Player Player { get; set; }
        
        public event Action<Vector2> OnMove;
        
        public event Action OnAttack;
        public event Action OnStartAim;
        public event Action OnEndAim;
        public event Action<Vector2> OnRotate;
        public event Action OnPause;
        public event Action OnResume;

        private bool _isMoving;
        private bool _isAiming;
        private InputAction.CallbackContext _moveContext;
        private bool _ignoreLookFrame;
        private bool _isEscapePressed;
        
        public void Initialize()
        {
            InputProvider.OnStartMove += StartMove;
            InputProvider.OnEndMove += EndMove;
            InputProvider.OnStartAttack += StartStartAttack;
            InputProvider.OnEndAttack += EndAttack;
            InputProvider.OnSwitchAim += SwitchAim;
            InputProvider.OnRotate += Rotate;
            InputProvider.OnEscape += Escape;
        }

        public void Dispose()
        {
            InputProvider.OnStartMove -= StartMove;
            InputProvider.OnEndMove -= EndMove;
            InputProvider.OnStartAttack -= StartStartAttack;
            InputProvider.OnEndAttack -= EndAttack;
            InputProvider.OnSwitchAim -= SwitchAim;
            InputProvider.OnRotate -= Rotate;
            InputProvider.OnEscape -= Escape;
        }
        
        public void Enable()
        {
            InputProvider.Enable();
            // Cursor.lockState = CursorLockMode.Locked;
            // Cursor.visible = false;
        }

        public void Disable()
        {
            InputProvider.Disable();
            // Cursor.lockState = CursorLockMode.None;
            // Cursor.visible = true;
            _ignoreLookFrame = true;
        }

        public void Tick()
        {
            if(!_isMoving)
                return;
            
            OnMove?.Invoke(_moveContext.ReadValue<Vector2>());
            Player.Move(_moveContext.ReadValue<Vector2>());
        }

        private void StartMove(InputAction.CallbackContext context)
        {
            _moveContext = context;
            _isMoving = true;
        }

        private void EndMove()
        {
            _isMoving = false;
        }

        private void StartStartAttack()
        {
            OnAttack?.Invoke();
            Player.StartAttack();
        }

        private void EndAttack()
        {
            Player.EndAttack();
        }

        private void SwitchAim()
        {
            if (_isAiming)
            {
                OnEndAim?.Invoke();
                Player.EndAim();
            }
            else
            {
                OnStartAim?.Invoke();
                Player.StartAim();
            }

            _isAiming = !_isAiming;
        }

        private void Rotate(Vector2 delta)
        {
            if (_ignoreLookFrame)
            {
                _ignoreLookFrame = false;
                return;
            }

            OnRotate?.Invoke(delta);
            Player.Rotate(delta);
        }

        private void Escape()
        {
            if(_isEscapePressed)
                OnResume?.Invoke();
            else
                OnPause?.Invoke();

            _isEscapePressed = !_isEscapePressed;
        }
    }
}