using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(Timeline))]
    public class eTimeline : Editor
    {
        bool showAction;
        public override void OnInspectorGUI()
        {
            Timeline timeline = (Timeline)target;
            GUILayout.Label(timeline.GetTimelineState().ToString());
            eUtils.DrawTimeInspector(timeline.GetTime());
            Repaint();
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("secondsInGameHour"));
            GUILayout.Space(10);
            if(GUILayout.Button(string.Format("Actions ({0})", timeline.GetActions().Count)))
            {
                showAction = !showAction;
            }
            if (showAction)
            {
                foreach(TimelineAction action in timeline.GetActions())
                {
                    EditorGUILayout.BeginHorizontal();
                    eUtils.DrawTimeInspector(action.timer);
                    EditorGUILayout.LabelField(action.name);
                EditorGUILayout.EndHorizontal();
                }
            }
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("actions"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}