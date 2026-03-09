namespace JustMobyTest.Input
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public interface IInputProvider
    {
        event Action<Vector2> OnPointerDown;
        event Action<Vector2> OnPointerMove;
        event Action<Vector2> OnPointerUp;
        event Action OnAttack;
        event Action<InputAction.CallbackContext> OnStartMove;
        event Action OnEndMove;
        event Action OnSwitchAim;
        Vector2 PointerPosition { get; }
        void Enable();
        void Disable();
    }
}