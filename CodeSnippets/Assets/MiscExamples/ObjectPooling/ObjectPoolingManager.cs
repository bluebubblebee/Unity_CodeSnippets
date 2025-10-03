using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace ObjectPoolExample
{
    public class ObjectPoolingManager : MonoBehaviour
    {
        [SerializeField]
        private TestPoolInstance m_instancePrefab;

        [SerializeField]
        private Transform m_parentTransform;

        [SerializeField]
        private int m_poolSize = 20;

        private UnityObjectPool<TestPoolInstance> m_objectPoolTest;
        private Coroutine m_testPoolRoutine;
        private int m_indexInstance;
        private Vector3 m_currentInstancePosition;


        void Start()
        {
            m_objectPoolTest = new UnityObjectPool<TestPoolInstance>(InstanceTest, m_instancePrefab, DisposeTest, m_poolSize);

            m_indexInstance = 0;
            m_currentInstancePosition = Vector3.zero;
            m_testPoolRoutine = StartCoroutine(TestPool());
        }

        private void OnDestroy()
        {
            m_objectPoolTest.Dispose();

            if (m_testPoolRoutine != null)
            {
                StopCoroutine(m_testPoolRoutine);
                m_testPoolRoutine = null;
            }
        }

        private IEnumerator TestPool()
        {
            while (m_indexInstance < m_poolSize)
            {              

                TestPoolInstance instance = m_objectPoolTest.Get();

                if (instance != null)
                {
                    instance.Setup(m_currentInstancePosition);

                    m_currentInstancePosition.x += 2;
                }

                yield return new WaitForSeconds(2.0f);
            }    
        }

        private TestPoolInstance InstanceTest(TestPoolInstance prefab)
        {
            TestPoolInstance instance = Instantiate(prefab, m_parentTransform, true);
            instance.Initialize();
            return instance;
        }

        private void DisposeTest(TestPoolInstance instance)
        {
            Destroy(instance.gameObject);
        }        
    }
}