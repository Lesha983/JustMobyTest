namespace JustMobyTest.Gameplay
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.AI;
    using Random = UnityEngine.Random;

    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private Enemy enemy;
        [SerializeField]
        private DetectedZone detectedZone;
        
        private bool _isTriggered;
        private Transform _target;
        private float _patrolDelay = 1f;
        private float _shootDelay = 0.7f;

        public void Setup()
        {
            
        }

        private void OnEnable()
        {
            detectedZone.OnDetected += Detect;
        }

        private void OnDisable()
        {
            detectedZone.OnDetected -= Detect;
        }

        private IEnumerator LifeRoutine()
        {
            var delay = new WaitForSeconds(_patrolDelay);
            while (!_isTriggered)
            {
                SetPatrolPoint();
                yield return new WaitUntil(() => !enemy.IsMoving);
                yield return delay;
            }
            
            
        }
        
        private void Detect(IDamageReceiver target)
        {
            if (_isTriggered) return;
            
            _isTriggered = true;
            // _stateMachine.ChangeState(chaseState);
        }
        
        private void SetPatrolPoint()
        {
            var random = transform.position + Random.insideUnitSphere * 10f;

            if (NavMesh.SamplePosition(random, out var hit, 10f, NavMesh.AllAreas))
            {
                // enemy.SetDestination(hit.position);
            }
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