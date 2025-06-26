using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DI
{
    public static class DiFactory
    {
        public static T Create<T>() where T : class
        {
            var type = typeof(T);
            var injectableConstructor = type.GetConstructors()
                .SingleOrDefault(x => x.HasAttribute<InjectAttribute>());
            if (injectableConstructor != null)
            {
                var arguments = injectableConstructor
                    .GetParameters()
                    .Select(param => DiContainer.Resolve(param.ParameterType))
                    .ToArray();
                var instance = injectableConstructor.Invoke(arguments);
                DiContainer.InjectDependencies(instance);
                return instance as T;
            }
            
            return Activator.CreateInstance<T>();
        }
        
        public static T Instantiate<T>(T original) where T : MonoBehaviour
        {
            var instance = Object.Instantiate(original);
            DiContainer.InjectDependencies(instance);
            return instance;
        }
        
        public static T Instantiate<T>(T original, Transform parent) where T : MonoBehaviour
        {
            var instance = Object.Instantiate(original, parent);
            DiContainer.InjectDependencies(instance);
            return instance;
        }
    }
}