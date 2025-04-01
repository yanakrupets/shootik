using System.Collections.Generic;
using Managers;
using NaughtyAttributes;
using UnityEngine;

namespace Tests
{
    public class PoolTest : MonoBehaviour
    {
        [SerializeField] private PoolManager poolManager;

        [Space] 
        [SerializeField] private Transform citizenParent;
        [SerializeField] private Transform enemyParent;
    
        private readonly Queue<CitizenItem> _citizenItems = new();
        private readonly Queue<EnemyItem> _enemyItems = new();

        [Button("Get Citizen")]
        private void GetCitizen()
        {
            var pool = poolManager.GetPool<CitizenItem>();
            var citizen = pool.Get();
            _citizenItems.Enqueue(citizen);
        
            citizen.transform.SetParent(citizenParent);
            citizen.transform.localPosition = Vector3.zero;
        }
    
        [Button("Return Citizen")]
        private void ReturnCitizen()
        {
            var pool = poolManager.GetPool<CitizenItem>();
            pool.Return(_citizenItems.Dequeue());
        }
    
        [Button("Get Enemy")]
        private void GetEnemy()
        {
            var pool = poolManager.GetPool<EnemyItem>();
            var enemy = pool.Get();
            _enemyItems.Enqueue(enemy);
        
            enemy.transform.SetParent(enemyParent);
            enemy.transform.localPosition = Vector3.zero;
        }
    
        [Button("Return Enemy")]
        private void ReturnEnemy()
        {
            var pool = poolManager.GetPool<EnemyItem>();
            pool.Return(_enemyItems.Dequeue());
        }
    }
}
