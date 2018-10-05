using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(QuestEvent))]
    public class eQuestEvent : Editor
    {
        public override void OnInspectorGUI()
        {
            QuestEvent questEvent = (QuestEvent)target;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("eventName"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("eventDescription"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("stressPerHour"));
            EditorGUILayout.Separator();
            eUtils.DrawPointOnMapSelector(ref questEvent.tier, ref questEvent.point);
            GUILayout.Space(10);
            eUtils.DrawQuestTaskList(questEvent.tasks, questEvent.transform);
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(questEvent);
        }
    }
}