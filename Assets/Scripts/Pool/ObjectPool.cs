using System.Collections.Generic;
using Extensions;
using Interfaces;
using UnityEngine;

namespace Pool
{
    public class ObjectPool<T> : IObjectPool<T> where T : MonoBehaviour
    {
        private readonly IObjectFactory<T> _factory;
        private readonly Queue<T> _objects = new();
        private readonly Transform _poolParent;

        public ObjectPool(IObjectFactory<T> factory, Transform parent)
        {
            _factory = factory;
            _poolParent = parent;
        }
    
        public T Get()
        {
            if (_objects.Count > 0)
            {
                var obj = _objects.Dequeue();
                return obj;
            }
            else
            {
                var obj = _factory.Create();
                obj.transform.SetParent(_poolParent, Vector3.zero);
                return obj;
            }
        }

        public void Return(T obj)
        {
            obj.transform.SetParent(_poolParent, Vector3.zero);
            _objects.Enqueue(obj);
        }
    }
}
