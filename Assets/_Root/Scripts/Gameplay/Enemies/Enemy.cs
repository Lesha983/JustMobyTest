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
        
        [SerializeField]
        private Agent agent;
        [SerializeField]
        private DetectedZone detectedZone;
        [SerializeField]
        private Gun gun;
        [Space]
        [SerializeField]
        private PatrolState patrolState;
        [SerializeField]
        private ChaseState chaseState;
        [SerializeField]
        private ShootState shootState;
        
        private EnemyStateMachine _stateMachine;
        private bool _isTriggered;
        private IDamageReceiver _target;
        private float _patrolDelay = 1f;
        private float _shootDelay = 0.7f;
        private float _damage = 30f;

        public override void Receive(Damage damage)
        {
            base.Receive(damage);
            Detect(Player);
        }

        private void Awake()
        {
            gun.Setup(_damage);
            _stateMachine = new EnemyStateMachine();
            patrolState.Setup(agent, _patrolDelay);
            chaseState.Setup(agent, Player.Transform);
            shootState.Setup(gun, Player.Transform, _shootDelay);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            detectedZone.OnDetected += Detect;
            chaseState.OnCatch += StartShoot;
            shootState.OnShootEnded += EndShoot;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            detectedZone.OnDetected -= Detect;
            chaseState.OnCatch -= StartShoot;
            shootState.OnShootEnded -= EndShoot;
        }

        protected override void SetStartState()
        {
            base.SetStartState();
            _isTriggered = false;
            _stateMachine.ChangeState(patrolState);
        }

        protected override void Die()
        {
            Wallet.AddPoints();
            Despawn();
        }

        private void Detect(IDamageReceiver target)
        {
            Debug.Log($"{name}.Detect()");
            if (_isTriggered) return;
            
            _isTriggered = true;
            _stateMachine.ChangeState(chaseState);
        }

        private void StartShoot()
        {
            _stateMachine.ChangeState(shootState);
        }

        private void EndShoot()
        {
            _stateMachine.ChangeState(chaseState);
        }
    }
}