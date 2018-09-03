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
            if(questEvent.name != string.Format("QuestEvent_{0}", questEvent.eventName))
            {
                questEvent.name = string.Format("QuestEvent_{0}", questEvent.eventName);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("eventDescription"));
            eUtils.DrawPointOnMapSelector(ref questEvent.tier, ref questEvent.point);
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tasks"), true);
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(questEvent);
        }
    }
}