using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ObjectPoolExample
{
    public class UnityObjectPool<T> : IDisposable where T : Object
    {
        private readonly int m_initialPoolSize;
        private readonly int m_maxPoolSize;
        private readonly T m_objectPrefab;

        private readonly Func<T, T> m_createPoolFunc; // Delegate type (basically a function pointer in C#). It represents a function that:  Takes one parameter of type Teturns a value of type
        private readonly Action<T> m_onDisposeEvent;

        private Queue<T> m_poolQueue = new Queue<T>();

        public int PoolSize => m_poolQueue.Count;

        /// <summary>
        /// Constructor which requires one method to create or instnatiated the Object type T
        /// </summary>
        /// <param name="createPoolFunc">Method that returns the Objects instantiated</param>
        /// <param name="objectPrefab">Prefab to instance</param>
        /// <param name="disposeDelegate">Event to handles disposing the object</param>
        /// <param name="initialPoolSize">The initial pool size</param>
        /// <param name="maxPoolSize">Maximun pool size</param>
        public UnityObjectPool(Func<T, T> createPoolFunc, T objectPrefab, Action<T> disposeDelegate = null, int initialPoolSize = 0, int maxPoolSize = 100 )
        {
            if (createPoolFunc == null)
            {
                throw new ArgumentException(nameof(createPoolFunc));
            }

            m_createPoolFunc = createPoolFunc;

            if (objectPrefab == null)
            {
                throw new ArgumentException(nameof(objectPrefab));
            }

            m_objectPrefab = objectPrefab;

            if (maxPoolSize < 1)
            {
                throw new ArgumentException("Max pool size must be at least 1.", nameof(maxPoolSize));
            }

            m_maxPoolSize = maxPoolSize;

            if (initialPoolSize > maxPoolSize)
            {
                throw new ArgumentException("Initial Pool size cannot be greater than MaxPoolSize.", nameof(initialPoolSize));
            }

            m_initialPoolSize = initialPoolSize;
            m_onDisposeEvent = disposeDelegate;

            InitializePool();
        }

        public T Get()
        {
            if ( !m_poolQueue.TryDequeue(out T obj))
            {
                obj = m_createPoolFunc(m_objectPrefab);
            }

            return obj;
        }

        public void Release(T obj)
        {
            if (m_poolQueue.Count >= m_maxPoolSize)
            {
                m_onDisposeEvent?.Invoke(obj);
                return;
            }

            m_poolQueue.Enqueue(obj);
        }

        public void Dispose()
        {
            List<T> poolList = new List<T>(m_poolQueue);

            if (m_onDisposeEvent != null)
            {
                for (int i = 0; i< poolList.Count; i++)
                {
                    m_onDisposeEvent.Invoke(poolList[i]);
                }
            }

            m_poolQueue.Clear();
        }

        private void InitializePool()
        {
            m_poolQueue = new Queue<T>(m_initialPoolSize);

            for (int i = 0; i< m_initialPoolSize; i++)
            {
                T obj = m_createPoolFunc(m_objectPrefab);
                m_poolQueue.Enqueue(obj);
            }
        }
    }
}
