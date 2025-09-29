using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionEvaluator
{
    [Serializable]
    public abstract class LevelCondition : ILevelCondition
    {
        public event Action OnInitialized;

        [SerializeField]
        [Tooltip("If true, condition gets inverted (is not)")]
        protected bool isNot;

        public bool IsInitialized { get; private set; }

        protected Transform TargetTransform { get; private set; }

        public void Initialize()
        {
            Initialize(null);
        }

        public void Initialize(Transform argument)
        {
            if (IsInitialized)
            {
                return;
            }

            TargetTransform = argument;
            IsInitialized = true;
            OnInitialized?.Invoke();
        }

        public bool Evaluate()
        {
            bool result = OnEvaluate();

            if (isNot )
            {
                return !result;
            }

            return result;
        }

        protected abstract bool OnEvaluate();
        
    }
}
