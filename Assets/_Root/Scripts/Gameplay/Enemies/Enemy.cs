namespace JustMobyTest.Gameplay
{
    using System;
    using UnityEngine;
    using Wallet;
    using Zenject;

    public class Enemy : AEnemy
    {
        [Inject]
        private IWallet Wallet { get; set; }
        [Inject]
        private Player Player { get; set; }
        
        public event Action OnDetected;
        public event Action OnDespawn;
        
        [SerializeField]
        private DetectedZone detectedZone;
        [Space]
        [SerializeField]
        private PatrolState patrolState;
        [SerializeField]
        private AttackState attackState;
        
        private EnemyStateMachine _stateMachine;
        private bool _isTriggered;
        private float _patrolDelay = 1f;
        
        public Health Health => health;
        
        public bool IsMoving => agent.IsMoving;

        public override void Receive(Damage damage)
        {
            Detect(Player);
            base.Receive(damage);
        }

        private void Awake()
        {
            _stateMachine = new EnemyStateMachine();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            detectedZone.OnDetected += Detect;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            detectedZone.OnDetected -= Detect;
        }

        protected override void SetStartState()
        {
            base.SetStartState();
            _isTriggered = false;
            patrolState.Setup(agent, _patrolDelay);
            _stateMachine.ChangeState(patrolState);
        }

        protected override void Die()
        {
            Wallet.AddPoints();
            _stateMachine.Exit();
            OnDespawn?.Invoke();
            Despawn();
        }

        private void Detect(IDamageReceiver target)
        {
            if (_isTriggered) return;
            
            _isTriggered = true;
            attackState.Setup(agent, target.Transform, gun);
            _stateMachine.ChangeState(attackState);
            OnDetected?.Invoke();
        }
    }
}