namespace JustMobyTest.Input
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Zenject;

    public class InputProvider : IInitializable, IInputProvider
    {
        public event Action<Vector2> OnPointerDown;
        public event Action<Vector2> OnPointerMove;
        public event Action<Vector2> OnPointerUp;
        
        public event Action OnStartAttack;
        public event Action OnEndAttack;
        public event Action<InputAction.CallbackContext> OnStartMove;
        public event Action OnEndMove;
        public event Action<Vector2> OnRotate;
        public event Action OnSwitchAim;
        public event Action OnEscape;
        
        public Vector2 PointerPosition => Pointer.current.position.ReadValue();
        public InputActions InputAction { get; private set; }
        
        public void Initialize()
        {
            InputAction = new InputActions();
            BindEvents();
            Enable();
        }
        
        public void Enable()
        {
            InputAction.Enable();
        }

        public void Disable()
        {
            InputAction.Disable();
        }

        private void BindEvents()
        {
            var actionUI = InputAction.UI;
            actionUI.Click.performed += ctx => OnPointerDown?.Invoke(PointerPosition);
            actionUI.Drag.performed += ctx => OnPointerMove?.Invoke(PointerPosition);
            actionUI.Click.canceled += ctx => OnPointerUp?.Invoke(PointerPosition);
            
            var actionPlayer = InputAction.Player;
            actionPlayer.Attack.performed += ctx => OnStartAttack?.Invoke();
            actionPlayer.Attack.canceled += ctx => OnEndAttack?.Invoke();
            actionPlayer.Move.performed += ctx => OnStartMove?.Invoke(ctx);
            actionPlayer.Move.canceled += ctx => OnEndMove?.Invoke();
            actionPlayer.Aim.performed += ctx => OnSwitchAim?.Invoke();
            actionPlayer.Rotate.performed += ctx => OnRotate?.Invoke(ctx.ReadValue<Vector2>());
            actionPlayer.Escape.performed += ctx => OnEscape?.Invoke();
        }
    }
    
}
