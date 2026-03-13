namespace JustMobyTest.Gameplay
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;
    using Zenject;

    public class ChaseState : MonoBehaviour, IEnemyState
    {
        [Inject]
        private Player Player { get; set; }

        public event Action OnCatch;
        
        private Agent _agent;
        private Transform _target;
        private Coroutine _chaseCoroutine;
        private float _checkDelay = 0.5f;
        
        public void Setup(Agent agent, Transform target)
        {
            _agent = agent;
            _target = target;
        }
        
        public void Enter()
        {
            _chaseCoroutine = StartCoroutine(nameof(ChaseRoutine));
            _agent.SetStopped(false);
        }

        public void Exit()
        {
            if (_chaseCoroutine != null)
                StopCoroutine(_chaseCoroutine);
        }

        private IEnumerator ChaseRoutine()
        {
            var delay = new WaitForSeconds(_checkDelay);
            var isActive = !CanShoot();
            
            while (isActive)
            {
                _agent.SetDestination(_target.position);
                yield return delay;
                isActive = !CanShoot();
            }
            
            _agent.SetStopped(true);
            OnCatch?.Invoke();
        }
        
        private bool CanShoot()
        {
            var origin = transform.position;
            var dir = (_target.position - origin).normalized;
            var distance = Vector3.Distance(origin, _target.position);

            if (Physics.Raycast(origin, dir, out var hit, distance))
            {
                return hit.transform == _target;
            }

            return false;
        }
    }
}