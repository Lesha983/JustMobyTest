namespace JustMobyTest.UI
{
    using System;
    using Input;
    using UnityEngine;

    public class UIInputHandler : IInputHandler
    {
        public event Action<Vector2> OnMove;
        public event Action OnAttack;
        public event Action OnStartAim;
        public event Action OnEndAim;
        public event Action<Vector2> OnRotate;
    }
}