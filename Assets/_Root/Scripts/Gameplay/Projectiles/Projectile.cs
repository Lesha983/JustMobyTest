namespace JustMobyTest.Gameplay
{
    using Pools;
    using UnityEngine;
    using Zenject;

    public struct ProjectileSpawnInfo : IReinitializingInfo
    {
        public Vector3 Position;
        public Vector3 Direction;
        public float Damage;
    }
    
    public class Projectile : CustomPoolable<ProjectileSpawnInfo>, IDamager
    {
        [Inject]
        private DamageFactory DamageFactory { get; set; }
        
        [SerializeField] 
        private Rigidbody rigidbody;
        [SerializeField]
        private float lifeTime;
        [SerializeField]
        private float speed;
        
        private float _damage;
        private bool _isActive;
        private float _time;
        private Vector3 _direction;
        
        public override void Reinitialize(ProjectileSpawnInfo info)
        {
            transform.position = info.Position;
            _direction = info.Direction;
            _damage = info.Damage;
            _isActive = true;
            _time = lifeTime;
        }

        private void FixedUpdate()
        {
            if(!_isActive)
                return;
            
            Movement();
            _time -= Time.fixedDeltaTime;
            if(_time <= 0)
                Hide();
        }

        private void Movement()
        {
            rigidbody.MovePosition(rigidbody.position + _direction * speed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageReceiver>(out var damageReceiver))
                damageReceiver.Receive(DamageFactory.Create(_damage));
            
            Hide();
        }

        private void Hide()
        {
            _isActive = false;
            Despawn();
        }
    }
}