using ConditionEvaluator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExampleArchitecture
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private LogicConditionManager m_logicConditionManager;
        [SerializeField]
        private UIManager m_uiManager;

        public bool IsInitialized { get; private set; }

        private static GameManager s_instance;


        public static GameManager Instance
        {
            get
            {
                return s_instance;
            }
        }

        private void Awake()
        {
           if (Instance != null)
            {
                if (Instance != this)
                {
                    DestroyImmediate(gameObject);
                    return;
                }

                if (Instance.IsInitialized)
                {
                    return;
                }
            }

            s_instance = this;
            DontDestroyOnLoad(gameObject);
            RegisterServices();

            IsInitialized = true;
        }

        private void OnDestroy()
        {
            IsInitialized = false;
            UnregisterServices();
        }

        private void Start()
        {

        }

        private void RegisterServices()
        {
            ServiceLocator.Register(this);
            ServiceLocator.Register<IUManager>(m_uiManager);
            ServiceLocator.Register(m_logicConditionManager);
        }

        private void UnregisterServices()
        {
            ServiceLocator.Unregister<GameManager>();
            ServiceLocator.Unregister<IUManager>();
            ServiceLocator.Unregister<LogicConditionManager>();
        }
    }
}
