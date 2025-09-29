using ExampleArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionEvaluator
{
    public class ObjectConditionToggler : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> targetGameObjects;

        [SerializeReference] 
        private List<ILevelCondition> conditions = new List<ILevelCondition>();

        private Coroutine testRoutine;

        private IUManager m_ui;

        private void Start()
        {
           
            testRoutine = StartCoroutine(TestCondition());           
        }

        private IEnumerator TestCondition()
        {
            yield return new WaitForSeconds(0.1f);

            m_ui = ServiceLocator.Get<IUManager>();

            m_ui.Show();

            for (int i = 0; i < conditions.Count; i++)
            {
                ILevelCondition condition = conditions[i];

                if (condition != null)
                {
                    condition.Initialize(transform);
                }
                
            }

            yield return new WaitForSeconds(3.0f);

            bool state = EvaluateConditions();
            SetObjectState(state);
        }

        private bool EvaluateConditions()
        {
            for (int i = 0; i < conditions.Count; i++)
            {
                ILevelCondition condition = conditions[i];
                if (!condition.Evaluate())
                {
                    return false;
                }
            }

            return true;
        }

        private void SetObjectState(bool state)
        {
            for (int i = 0; i< targetGameObjects.Count; i++)
            {
                targetGameObjects[i].SetActive(state);
            }
        }
    }
}
