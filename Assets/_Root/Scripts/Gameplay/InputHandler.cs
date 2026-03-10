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
        
        public event Action<Vector2> OnMove;
        
        public event Action OnAttack;
        public event Action OnStartAim;
        public event Action OnEndAim;
        public event Action<Vector2> OnRotate;

        private bool _isMoving;
        private bool _isAiming;
        private InputAction.CallbackContext _moveContext;
        
        public void Initialize()
        {
            InputProvider.OnStartMove += StartMove;
            InputProvider.OnEndMove += EndMove;
            InputProvider.OnAttack += Attack;
            InputProvider.OnSwitchAim += SwitchAim;
            InputProvider.OnRotate += Rotate;
        }

        public void Dispose()
        {
            InputProvider.OnStartMove -= StartMove;
            InputProvider.OnEndMove -= EndMove;
            InputProvider.OnAttack -= Attack;
            InputProvider.OnSwitchAim -= SwitchAim;
            InputProvider.OnRotate -= Rotate;
        }

        public void Tick()
        {
            if(!_isMoving)
                return;
            
            OnMove?.Invoke(_moveContext.ReadValue<Vector2>());
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

        private void Attack()
        {
            OnAttack?.Invoke();
        }

        private void SwitchAim()
        {
            if(_isAiming)
                OnEndAim?.Invoke();
            else
                OnStartAim?.Invoke();
            
            _isAiming = !_isAiming;
        }

        private void Rotate(Vector2 delta)
        {
            OnRotate?.Invoke(delta);
        }
    }
}