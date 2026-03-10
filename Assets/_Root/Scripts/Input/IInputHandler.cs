namespace JustMobyTest.Input
{
    using System;
    using UnityEngine;

    public interface IInputHandler
    {
        public event Action<Vector2> OnMove;
        public event Action OnAttack;
        public event Action OnStartAim;
        public event Action OnEndAim;
        public event Action<Vector2> OnRotate;
    }
}