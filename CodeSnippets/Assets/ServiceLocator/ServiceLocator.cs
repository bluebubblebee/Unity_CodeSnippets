using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ExampleArchitecture
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> s_services = new Dictionary<Type, object>();

        public static void Register<T> (T service) where T : class
        {
            if( !s_services.TryAdd(typeof(T), service))
            {
                Debug.LogError("Unable to register service type " + nameof(T) + ", already registered");
            }
        }

        public static bool Unregister<T>() where T : class
        {
            return s_services.Remove(typeof(T));
        }

        public static T Get<T>() where T: class
        {
            if (s_services.TryGetValue(typeof(T), out object service))
            {
                return service as T;
            }

            throw new NullReferenceException($"Unable to find service with type {typeof(T).Name}");
        }

        public static  bool  TryGet<T>(out T service) where T : class
        {
            if (s_services.TryGetValue(typeof(T), out object serviceObject))
            {
                service = serviceObject as T;
                return true;
            }

            service = null;
            return false;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            s_services.Clear();
        }
    }
}
