using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    [CustomEditor(typeof(ChooseMethodFunction))]
    public class eChooseMethodFunction : Editor
    {
        public override void OnInspectorGUI()
        {
            ChooseMethodFunction function = (ChooseMethodFunction)target;
            if (function.brutalOutput != null)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Brutal:"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("brutalFileNote"));
                EditorGUILayout.EndVertical();
            }
            if (function.carefulOutput != null)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Careful:"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("carefulFileNote"));
                EditorGUILayout.EndVertical();
            }
            if (function.diplomatOutput != null)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Diplomatic:"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("diplomatFileNote"));
                EditorGUILayout.EndVertical();
            }
            if (function.scienceOutput != null)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Scientific:"), EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("scienceFileNote"));
                EditorGUILayout.EndVertical();
            }

            for (int i = 0; i < function.dialogOutputs.Count; i++)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(string.Format("Output {0}:", i), EditorStyles.boldLabel);
                EditorGUILayout.LabelField(function.dialog.GetEnds()[i].chooseText);
                EditorGUILayout.PropertyField(serializedObject.FindProperty(string.Format("dialogFileNotes.Array.data[{0}]", i)));
                EditorGUILayout.EndVertical();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
