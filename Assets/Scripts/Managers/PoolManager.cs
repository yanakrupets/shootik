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
        [SerializeField] private Transform citizenParent;

        [SerializeField] private EnemyItem enemyPrefab;
        [SerializeField] private Transform enemyParent;

        private readonly Dictionary<Type, IObjectPoolBase> _pools = new();

        private void Awake()
        {
            CreatePool(citizenPrefab, citizenParent);
            CreatePool(enemyPrefab, enemyParent);
        }

        public IObjectPool<T> GetPool<T>() where T : MonoBehaviour, IPoolableItem
        {
            return (IObjectPool<T>)_pools[typeof(T)];
        }

        private void CreatePool<T>(T prefab, Transform parent) where T : MonoBehaviour, IPoolableItem
        {
            var factory = new PrefabFactory<T>(prefab);
            var pool = new ObjectPool<T>(factory, parent);
            _pools.Add(typeof(T), pool);
        }
    }
}
