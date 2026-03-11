namespace JustMobyTest.Pools
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    public class CustomPooled<TId, TInfo> : MonoBehaviour, IPool<TInfo>
        where TInfo : IReinitializingInfo
        where TId : CustomPoolable<TInfo>
    {
        [Inject] 
        private IInstantiator Instantiator { get; set; }

        public event Action<TId> OnDespawn; 
        
        private Queue<TId> _pool = new();
        private List<TId> _active = new();
        
        private TId _prefab;

        public void Initialize(TId prefab)
        {
            _prefab = prefab;
        }
        
        public virtual TId Spawn(TInfo info)
        {
            var obj = Get();
            obj.gameObject.SetActive(true);
            obj.Reinitialize(info);
            _active.Add(obj);
            return obj;
        }
    
        public void Despawn(CustomPoolable<TInfo> poolable)
        {
            poolable.gameObject.SetActive(false);
            var customPoolable = poolable as TId;
            _pool.Enqueue(customPoolable);
            _active.Remove(customPoolable);
            OnDespawn?.Invoke(customPoolable);
        }
    
        private TId Get()
        {
            if (_pool.Count == 0)
                Create();
    
            if (_pool.TryDequeue(out var enemy))
                return enemy;
    
            return null;
        }
    
        private void Create()
        {
            var obj = Instantiator.InstantiatePrefabForComponent<TId>(_prefab, transform);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}