using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    [CustomEditor(typeof(ChooseTemperFunction))]
    public class eChooseTemperFunction : Editor
    {

        public override void OnInspectorGUI()
        {
            ChooseTemperFunction function = (ChooseTemperFunction)target;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("loyaltyInfluence"));
            if(function.rudeOutput != null)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Rude:"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rudeFileNote"));
                EditorGUILayout.EndVertical();
            }
            if (function.prudentOutput != null)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Prudent:"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("prudentFileNote"));
                EditorGUILayout.EndVertical();
            }
            if (function.mercifulOutput != null)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Merciful:"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("mercifulFileNote"));
                EditorGUILayout.EndVertical();
            }
            if (function.cruelOutput != null)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Cruel:"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("cruelFileNote"));
                EditorGUILayout.EndVertical();
            }
            if (function.mercantileOutput != null)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Mercantile:"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("mercantileFileNote"));
                EditorGUILayout.EndVertical();
            }
            if (function.principledOutput != null)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Principled:"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("principledFileNote"));
                EditorGUILayout.EndVertical();
            }

            for (int i=0; i < function.dialogOutputs.Count; i++)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Output {0}:", i), EditorStyles.boldLabel);
                EditorGUILayout.LabelField(function.dialog.GetEnds()[i].chooseText);
                EditorGUILayout.PropertyField(serializedObject.FindProperty(string.Format("dialogTemper.Array.data[{0}]", i)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(string.Format("dialogFileNotes.Array.data[{0}]", i)));
                EditorGUILayout.EndVertical();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
