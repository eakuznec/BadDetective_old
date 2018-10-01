using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    [CustomEditor(typeof(LogicEffect))]
    public class eLogicEffect : Editor
    {
        public override void OnInspectorGUI()
        {
            LogicEffect function = (LogicEffect)target;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("type"), GUIContent.none);
            serializedObject.ApplyModifiedProperties();
            if (function.type == LogicEffectType.SINGLE)
            {
                base.OnInspectorGUI();
            }
            else if(function.type == LogicEffectType.ARRAY)
            {
                bool show = true;
                eUtils.DrawEffectsSelector(function.effects, function.transform, ref show);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
