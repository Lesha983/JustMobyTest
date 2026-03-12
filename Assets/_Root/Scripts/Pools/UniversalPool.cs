namespace JustMobyTest.Pools
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    public interface IPool<TInfo> where TInfo : IReinitializingInfo
    {
        public void Despawn(CustomPoolable<TInfo> poolable);
    }
    
    public abstract class UniversalPool<TId, TInfo> : MonoBehaviour, IPool<TInfo>, IInitializable, IDisposable
        where TInfo : IReinitializingInfo
        where TId : CustomPoolable<TInfo>
    {
        [Inject] 
        private IInstantiator Instantiator { get; set; }
        
        public event Action<TId> OnSpawn;
        public event Action<TId> OnDespawn;

        protected Dictionary<int, Queue<TId>> _pools = new();
        protected Dictionary<TId, int> _itemToId = new();
        protected List<TId> _active = new();

        public void Initialize() {}

        public void Dispose() {}

        public virtual TId Spawn(TId prefab, TInfo info)
        {
            var obj = Get(prefab);
            obj.gameObject.SetActive(true);
            obj.Reinitialize(info);
            _active.Add(obj);
            OnSpawn?.Invoke(obj);
            return obj;
        }

        public void Despawn(CustomPoolable<TInfo> poolable)
        {
            poolable.gameObject.SetActive(false);
            var customPoolable = poolable as TId;
            if (!_itemToId.TryGetValue(customPoolable, out var id))
            {
                Debug.LogError($"CustomPoolable {customPoolable} not found in pool");
                return;
            }

            var pool = _pools[id];
            pool.Enqueue(customPoolable);
            _active.Remove(customPoolable);
            OnDespawn?.Invoke(customPoolable);
        }
        
        private TId Get(TId prefab)
        {
            var key = prefab.GetInstanceID();
            if (!_pools.ContainsKey(key))
                CreatePool(prefab);
            
            var pool = _pools[key];
            if (pool.Count == 0)
                Create(prefab, pool);
    
            if (pool.TryDequeue(out var enemy))
                return enemy;
    
            return null;
        }
    
        private void Create(TId prefab, Queue<TId> pool)
        {
            var obj = Instantiator.InstantiatePrefabForComponent<TId>(prefab, transform);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
            _itemToId.Add(obj, prefab.GetInstanceID());
        }
        
        private void CreatePool(TId prefab)
        {
            _pools.Add(prefab.GetInstanceID(), new Queue<TId>());
            // var obj = Instantiator.CreateEmptyGameObject($"{prefab.name}Pool");;
            // obj.transform.SetParent(transform);
            // var pool = obj.AddComponent<CustomPooled<TId, TInfo>>();
            // pool.Initialize(prefab);
            // _pools.Add(prefab, pool);
        }
    }
}