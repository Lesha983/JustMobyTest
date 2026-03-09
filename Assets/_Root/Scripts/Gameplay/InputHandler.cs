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
        
        private bool _isMoving;
        private InputAction.CallbackContext _moveContext;
        
        public void Initialize()
        {
            InputProvider.OnStartMove += StartMove;
            InputProvider.OnEndMove += EndMove;
            InputProvider.OnAttack += Attack;
        }

        public void Dispose()
        {
            InputProvider.OnStartMove -= StartMove;
            InputProvider.OnEndMove -= EndMove;
            InputProvider.OnAttack -= Attack;
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
    }
}