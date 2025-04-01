using Interfaces;
using UnityEngine;

namespace Factories
{
    public class PrefabFactory<T> : IObjectFactory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;

        public Transform Parent { get; }

        public PrefabFactory(T prefab, Transform parent)
        {
            _prefab = prefab;
            Parent = parent;
        }
        
        public T Create()
        {
            var obj = Object.Instantiate(_prefab, Parent);
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}
