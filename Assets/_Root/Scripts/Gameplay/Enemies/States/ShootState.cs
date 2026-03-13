namespace JustMobyTest.Gameplay
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ShootState : MonoBehaviour, IEnemyState
    {
        public event Action OnShootEnded;
        
        private Gun _gun;
        private Transform _target;
        private float _reloadTime = 1f;
        private Coroutine _shootCoroutine;
        
        public void Setup(Gun gun, Transform target, float reloadTime)
        {
            _gun = gun;
            _target = target;
            _reloadTime = reloadTime;
        }
        
        public void Enter()
        {
            _shootCoroutine = StartCoroutine(nameof(ShootRoutine));
        }
        
        public void Exit()
        {
            if (_shootCoroutine != null)
                StopCoroutine(_shootCoroutine);
        }

        private IEnumerator ShootRoutine()
        {
            var delay = new WaitForSeconds(_reloadTime);
            var isActive = CanShoot();
            while (isActive)
            {
                RotateToPlayer();
                var direction = (_target.position - _gun.ShootPoint.position).normalized;
                _gun.StartAttack(direction);
                yield return delay;
                isActive = CanShoot();
            }
            
            OnShootEnded?.Invoke();
        }
        
        void RotateToPlayer()
        {
            var dir = _target.position - transform.position;
            dir.y = 0;
            
            transform.rotation = Quaternion.LookRotation(dir);
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