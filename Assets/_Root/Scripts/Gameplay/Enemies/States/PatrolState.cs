namespace JustMobyTest.Gameplay
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.AI;

    public class PatrolState : MonoBehaviour, IEnemyState
    {
        private Agent _agent;
        private float _reloadTime = 1f;
        private Coroutine _patrolCoroutine;
        private bool _isActive;

        public void Setup(Agent agent, float reloadTime)
        {
            _agent = agent;
            _reloadTime = reloadTime;
        }
        
        public void Enter()
        {
            _isActive = true;
            _patrolCoroutine = StartCoroutine(Patrol());
        }

        public void Exit()
        {
            _isActive = false;
            StopCoroutine(_patrolCoroutine);
        }

        private IEnumerator Patrol()
        {
            var reloadDelay = new WaitForSeconds(_reloadTime);
            while (_isActive)
            {
                SetPatrolPoint();
                yield return new WaitUntil(() => !_agent.IsMoving);
                yield return reloadDelay;
            }
        }
        
        private void SetPatrolPoint()
        {
            var random = _agent.transform.position + Random.insideUnitSphere * 10f;

            if (NavMesh.SamplePosition(random, out var hit, 10f, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
            }
        }
    }
}