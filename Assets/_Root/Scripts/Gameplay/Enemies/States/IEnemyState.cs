namespace JustMobyTest.Gameplay
{
    using System;

    public interface IEnemyState
    {
        void Enter();
        void Exit();
    }
    
    public class Transition
    {
        public IEnemyState From;
        public IEnemyState To;
        public Func<bool> Condition;

        public Transition(IEnemyState from, IEnemyState to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}