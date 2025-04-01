using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Pool
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolableItem
    {
        private readonly IObjectFactory<T> _factory;
        private readonly Queue<T> _objects = new Queue<T>();

        public ObjectPool(IObjectFactory<T> factory)
        {
            _factory = factory;
        }
    
        public T Get()
        {
            if (_objects.Count > 0)
            {
                var obj = _objects.Dequeue();
                obj.gameObject.SetActive(true);
                obj.OnGet();
                return obj;
            }
            else
            {
                var obj = _factory.Create();
                obj.gameObject.SetActive(true);
                obj.OnGet();
                return obj;
            }
        }

        public void Return(T obj)
        {
            obj.OnReturn();
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_factory.Parent);
            obj.transform.localPosition = Vector3.zero;
            _objects.Enqueue(obj);
        }
    }
}
