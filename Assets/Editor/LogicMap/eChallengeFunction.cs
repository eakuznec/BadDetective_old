using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    [CustomEditor (typeof(ChallengeFunction))]
    public class eChallengeFunction : Editor
    {
        public static bool showSuccessEffect;
        public static bool showSuccessHardEffect;
        public static bool showFailEffect;
        public static bool showFailHardEffect;


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
                EditorGUILayout.Separator();

            }
            else if (function.challenge.type == ChallengeType.METHOD)
            {
                EditorGUILayout.PropertyField(soChallenge.FindProperty("executor"));
                EditorGUILayout.PropertyField(soChallenge.FindProperty("method"));
                EditorGUILayout.PropertyField(soChallenge.FindProperty("_tag"));
                EditorGUILayout.PropertyField(soChallenge.FindProperty("level"));
                EditorGUILayout.PropertyField(soChallenge.FindProperty("difficulty"));
                EditorGUILayout.Separator();
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("Success:");
                EditorGUILayout.PropertyField(serializedObject.FindProperty("trueFileNote"), GUIContent.none);
                eUtils.DrawDetectiveEffectList(function.challenge.successEffects, function.challenge.transform, ref showSuccessEffect, "Success Effect", "SuccessEffects");
                eUtils.DrawDetectiveEffectList(function.challenge.successHardEffects, function.challenge.transform, ref showSuccessHardEffect, "Success Hard Effect", "SuccessHardEffects");
                EditorGUILayout.EndVertical();
                EditorGUILayout.Separator();
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("Fail:");
                EditorGUILayout.PropertyField(serializedObject.FindProperty("falseFileNote"), GUIContent.none);
                eUtils.DrawDetectiveEffectList(function.challenge.failEffects, function.challenge.transform, ref showFailEffect, "Fail Effect", "FailEffects");
                eUtils.DrawDetectiveEffectList(function.challenge.failHardEffects, function.challenge.transform, ref showFailHardEffect, "Fail Hard Effect", "FailHardEffects");
                EditorGUILayout.EndVertical();
            }
            soChallenge.ApplyModifiedProperties();
            serializedObject.ApplyModifiedProperties();
        }
    }
}