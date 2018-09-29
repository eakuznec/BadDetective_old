using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(Equipment))]
    public class eEquipment : Editor
    {
        private static bool showConditions;

        public override void OnInspectorGUI()
        {
            Equipment item = (Equipment)target;
            base.OnInspectorGUI();
            eUtils.DrawItemConditionList(item.conditions, item.weights, item.transform, ref showConditions);
        }
    }
}
