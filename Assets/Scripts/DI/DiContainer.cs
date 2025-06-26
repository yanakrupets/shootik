using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;

namespace DI
{
    public class DiContainer
    {
        private static DiContainer _instance;
        private readonly Dictionary<Type, object> _registrations = new();

        private DiContainer() { }
        
        private static DiContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DiContainer();
                }
                return _instance;
            }
        }
        
        public static void Bind<T>(T instance)
        {
            Instance._registrations[typeof(T)] = instance;
        }
        
        public static object Resolve(Type type)
        {
            if (Instance._registrations.TryGetValue(type, out object value))
            {
                return value;
            }
            throw new KeyNotFoundException($"Dependency of type {type} not registered in DI container.");
        }
        
        public static void InjectDependencies<T>(T target)
        {
            var type = target.GetType();
        
            foreach (var field in type.GetFields(
                         BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (field.HasAttribute(typeof(InjectAttribute)))
                {
                    var dependency = DiContainer.Resolve(field.FieldType);
                    field.SetValue(target, dependency);
                }
            }

            foreach (var method in type.GetMethods(
                         BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (method.HasAttribute(typeof(InjectAttribute)))
                {
                    var arguments = method
                        .GetParameters()
                        .Select(param => DiContainer.Resolve(param.ParameterType))
                        .ToArray();
                    method.Invoke(target, arguments);
                }
            }
        }

        //public static void Bind<T>(T instance) => _instance._registrations[typeof(T)] = instance;
        //public static T Resolve<T>() => (T)Resolve(typeof(T));
        //public static object Resolve(Type type) => _instance._registrations[type];
    }
}