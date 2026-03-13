namespace JustMobyTest.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;

    public class EnemyStateMachine
    {
        private IEnemyState _currentState;
        
        public void ChangeState(IEnemyState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}