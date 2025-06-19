using DI;
using Interfaces;
using UnityEngine;

namespace Factories
{
    public class PrefabFactory<T> : IObjectFactory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;

        public PrefabFactory(T prefab)
        {
            _prefab = prefab;
        }
        
        public T Create()
        {
            var obj = DiFactory.Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}
