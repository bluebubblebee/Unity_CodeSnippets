using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionEvaluator
{
    public interface ILevelCondition : IInitializable<Transform>
    {
        bool Evaluate();
    }
}
