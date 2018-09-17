using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    [CustomEditor (typeof(ChallengeFunction))]
    public class eChallengeFunction : Editor
    {
        public override void OnInspectorGUI()
        {
            ChallengeFunction function = (ChallengeFunction)target;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("waitType"));
            EditorGUILayout.LabelField(function.waitTime.ToString());
            EditorGUILayout.PropertyField(serializedObject.FindProperty("waitTime"), true);
            EditorGUILayout.Separator();
            SerializedObject soChallenge = new SerializedObject(function.challenge);
            EditorGUILayout.PropertyField(soChallenge.FindProperty("type"));
            if(function.challenge.type == ChallengeType.HAVE_TAG)
            {
                EditorGUILayout.PropertyField(soChallenge.FindProperty("executor"));
                EditorGUILayout.PropertyField(soChallenge.FindProperty("_tag"));
            }
            else if (function.challenge.type == ChallengeType.METHOD)
            {
                EditorGUILayout.PropertyField(soChallenge.FindProperty("executor"));
                EditorGUILayout.PropertyField(soChallenge.FindProperty("method"));
                EditorGUILayout.PropertyField(soChallenge.FindProperty("_tag"));
                EditorGUILayout.PropertyField(soChallenge.FindProperty("level"));
                EditorGUILayout.PropertyField(soChallenge.FindProperty("difficulty"));
            }
            soChallenge.ApplyModifiedProperties();
            serializedObject.ApplyModifiedProperties();
        }
    }
}