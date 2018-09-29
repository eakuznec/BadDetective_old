using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(Item))]
    public class eItem : Editor
    {
        private static bool showConditions;

        public override void OnInspectorGUI()
        {
            Item item = (Item)target;
            base.OnInspectorGUI();
            eUtils.DrawItemConditionList(item.conditions, item.weights, item.transform, ref showConditions);
        }
    }
}
