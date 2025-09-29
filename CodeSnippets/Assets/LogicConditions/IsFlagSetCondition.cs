using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionEvaluator
{
    [System.Serializable]
    public class IsFlagSetCondition : LevelCondition
    {
        [SerializeField]
        private string flagName;

        [SerializeField]
        private bool expectedValue = true;
        

        protected override bool OnEvaluate()
        {
           if (LogicConditionManager.instance.TryGetFlag(flagName, out bool value) )
           {
                return expectedValue == value;
           }

           return false;
        }
    }
}
