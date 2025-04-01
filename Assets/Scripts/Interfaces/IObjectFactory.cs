using UnityEngine;

namespace Interfaces
{
    public interface IObjectFactory<out T> where T : MonoBehaviour
    {
        T Create();
        Transform Parent { get; }
    }
}
