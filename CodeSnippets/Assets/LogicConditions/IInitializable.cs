using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ConditionEvaluator
{
    public interface IInitializable
    {
        event Action OnInitialized;
        
        bool IsInitialized { get; }

        void Initialize();
    }

    public interface IInitializable<T>
    {
        event Action OnInitialized;

        bool IsInitialized { get; }

        void Initialize( T argument );
    }
}
