using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class QuestState : MonoBehaviour
    {
        public string stateName;
        public QuestStateType type;
        public int intValue;
        public bool boolValue;
        public string specialValue;
        public List<string> possibleValue = new List<string>();

    }
}
