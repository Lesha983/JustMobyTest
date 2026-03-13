namespace JustMobyTest.Gameplay
{
    using System;
    using UnityEngine;
    using UnityEngine.AI;

    [RequireComponent(typeof(NavMeshAgent))]
    public class Agent : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private float _startSpeed;
        private float _currentSpeed;
        
        public bool IsMoving => _agent.velocity != Vector3.zero;

        public void Setup(float speed)
        {
            _startSpeed = speed;
            _currentSpeed = speed;
        }

        public void Move(Vector3 direction)
        {
            _agent.Move(direction * _currentSpeed * Time.deltaTime);
        }

        public void SetDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }
        
        public void SetStopped(bool value)
        {
            _agent.isStopped = value;
        }
        
        public void SetSpeedCoefficient(float coeff)
        {
            _currentSpeed = _startSpeed * coeff;
        }

        private void Awake()
        {
            _agent  = GetComponent<NavMeshAgent>();
        }
    }
}