using UnityEngine;

namespace Interfaces
{
    public interface IObjectPool<T> : IObjectPoolBase where T : MonoBehaviour, IPoolableItem
    {
        public T Get();
        public void Return(T obj);
    }
}
