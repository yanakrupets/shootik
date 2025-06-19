using System;
using System.Collections.Generic;
using Factories;
using Interfaces;
using Pool;
using UnityEngine;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private CitizenItem citizenPrefab;
        [SerializeField] private EnemyItem enemyPrefab;
        [SerializeField] private HeartItem heartPrefab;

        private readonly Dictionary<Type, IObjectPoolBase> _pools = new();

        public void Awake()
        {
            CreatePool(citizenPrefab);
            CreatePool(enemyPrefab);
            CreatePool(heartPrefab);
        }

        public IObjectPool<T> GetPool<T>() where T : MonoBehaviour
        {
            return (IObjectPool<T>)_pools[typeof(T)];
        }

        private void CreatePool<T>(T prefab) where T : MonoBehaviour
        {
            if (prefab is null)
            {
                Debug.LogError($"Prefab for type {typeof(T).Name} is not assigned!");
                return;
            }
            
            var parent = new GameObject($"{typeof(T).Name}Pool");
            parent.transform.SetParent(transform);
            
            var factory = new PrefabFactory<T>(prefab);
            var pool = new ObjectPool<T>(factory, parent.transform);
            _pools.Add(typeof(T), pool);
        }
    }
}
