namespace JustMobyTest.Gameplay
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AttackState : MonoBehaviour, IEnemyState
    {
        private Agent _agent;
        private Transform _target;
        private Gun _gun;
        private float _checkDelay = 0.5f;
        private float _reloadTime = 1f;

        private Coroutine _attackCoroutine;
        private bool _isActive;
        
        public void Setup(Agent agent, Transform target, Gun gun)
        {
            _agent = agent;
            _target = target;
            _gun = gun;
        }
        
        public void Enter()
        {
            _isActive = true;
            if (_attackCoroutine == null)
                _attackCoroutine = StartCoroutine(AttackRoutine());
        }

        public void Exit()
        {
            _isActive = false;
            
            if(_attackCoroutine == null)
                return;
            
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        private IEnumerator AttackRoutine()
        {
            var checkDelay = new WaitForSeconds(_checkDelay);
            var reloadDelay = new WaitForSeconds(_reloadTime);

            while (_isActive)
            {
                _agent.SetStopped(false);
                while (!CanShoot())
                {
                    _agent.SetDestination(_target.position);
                    yield return checkDelay;
                }
                
                _agent.SetStopped(true);
                var direction = (_target.position - _gun.ShootPoint.position).normalized;
                _gun.StartAttack(direction);
                yield return reloadDelay;
            }
        }
        
        private bool CanShoot()
        {
            var origin = _gun.ShootPoint.position;
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