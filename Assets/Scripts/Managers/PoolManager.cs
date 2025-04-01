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

        private readonly Dictionary<Type, object> _pools = new();

        private void Awake()
        {
            var citizenFactory = new PrefabFactory<CitizenItem>(citizenPrefab, citizenParent);
            var citizenPool = new ObjectPool<CitizenItem>(citizenFactory);
            _pools.Add(typeof(CitizenItem), citizenPool);
            
            var enemyFactory = new PrefabFactory<EnemyItem>(enemyPrefab, enemyParent);
            var enemyPool = new ObjectPool<EnemyItem>(enemyFactory);
            _pools.Add(typeof(EnemyItem), enemyPool);
        }

        public ObjectPool<T> GetPool<T>() where T : MonoBehaviour, IPoolableItem
        {
            return (ObjectPool<T>)_pools[typeof(T)];
        }
    }
}
