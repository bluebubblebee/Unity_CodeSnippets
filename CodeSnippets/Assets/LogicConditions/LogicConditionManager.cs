using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConditionEvaluator
{
    [System.Serializable]
    public class GameLogicCondition
    {
        public string FlagName;

        public bool Value;
    }

    [CreateAssetMenu(fileName = "GameData", menuName = "LogicConditionTester/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public List<GameLogicCondition> GameConditions;
    }

    public class LogicConditionManager : MonoBehaviour
    {
        public static LogicConditionManager instance { get; private set; }
        [SerializeField] private GameData gameData;


        protected Dictionary<string, bool> flagValues = new Dictionary<string, bool>();

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            for (int i = 0; i< gameData.GameConditions.Count; i++)
            {
                GameLogicCondition condition = gameData.GameConditions[i];

                if (!flagValues.ContainsKey(condition.FlagName))
                {
                    bool randValue = true;
                    if (Random.Range(0,100) < 50)
                    {
                        randValue = false;
                    }

                    flagValues.Add(condition.FlagName, randValue);
                }
            }
        }

        public bool TryGetFlag(string flagName, out bool value)
        {
            if (flagValues.TryGetValue(flagName, out value))
            {
                return true;
            }

            Debug.Log("<color=red>"+ "[LogicConditionManager.TryGetFlag] ERROR! Unable to find flag " + flagName + "</color>");
            return false;
        }
    }
}
