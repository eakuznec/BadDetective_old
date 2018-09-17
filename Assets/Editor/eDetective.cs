using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(Detective))]
    public class eDetective : Editor
    {
        public override void OnInspectorGUI()
        {
            Detective detective = (Detective)target;
            GUIStyle boldStyle = new GUIStyle();
            boldStyle.fontStyle = FontStyle.Bold;
            DetectiveManager detectiveManager = DetectiveManager.GetInstantiate();
            if (!detectiveManager.GetDetectives().Contains(detective))
            {
                if(PrefabUtility.GetPrefabType(detective) == PrefabType.Prefab)
                {
                    if (GUILayout.Button("!!! Registrate !!!"))
                    {
                        detectiveManager.Registrate(detective);
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("---Обратитесь к префабу для регистрации в Game!---");
                }
                GUILayout.Space(10);
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(detective.activity.ToString());
            if (detective.activityPlace != null)
            {
                EditorGUILayout.LabelField(detective.activityPlace.GetPlaceName());
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("characterName"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("characterAvatar"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sex"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("age"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("characterStory"));
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("temper"));
            GUILayout.Space(10);
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Parameters", boldStyle);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("curHealth"), new GUIContent("Health"));
            EditorGUILayout.LabelField(string.Format("{0}/{1}", detective.minHealth, detective.maxHealth));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("curStress"), new GUIContent("Stress"));
            EditorGUILayout.LabelField(string.Format("{0}/{1}", detective.minStress, detective.maxStress));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("curLoyalty"), new GUIContent("Loyalty"));
            EditorGUILayout.LabelField(string.Format("{0}/{1}", detective.minLoyalty, detective.maxLoyalty));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("curConfidence"), new GUIContent("Confidence"));
            EditorGUILayout.LabelField(string.Format("{0}/{1}", detective.minConfidence, detective.maxConfidence));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Methods", boldStyle);
            for (int i = 0; i < detective.methods.Count; i++)
            {
                Method method = detective.methods[i];
                GUILayout.BeginHorizontal();
                detective.methodsValues[i] = EditorGUILayout.IntField(method.ToString(), detective.methodsValues[i]);
                EditorGUILayout.LabelField(string.Format("/ {0}", detective.maxMethodsValues[i]));
                GUILayout.EndHorizontal();
            }
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("traits"), true);
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Salary", boldStyle);
            Money salary = detective.salary;
            eUtils.DrawMoneyInspecor(ref salary);
            detective.salary = salary;
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("home"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("priorityWay"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("wayColor"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
