using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    [CustomEditor(typeof(WaitFunction))]
    public class eWaitFunction : Editor
    {
        public override void OnInspectorGUI()
        {
            WaitFunction function = (WaitFunction)target;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("waitType"), GUIContent.none);
            float min = EditorGUILayout.FloatField("Minutes", function.waitTime.minutes);
            int hour = EditorGUILayout.IntField("Hours", function.waitTime.hours);
            int day = 0;
            int week = 0;
            int month = 0;
            if (function.waitType == WaitType.RELATION || function.waitType == WaitType.ABSOLUTE)
            {
                day = EditorGUILayout.IntField("Days", function.waitTime.days);
                week = EditorGUILayout.IntField("Weeks", function.waitTime.weeks);
                month = EditorGUILayout.IntField("Months", function.waitTime.months);
            }
            GameTime gameTime = new GameTime
            {
                minutes = min,
                hours = hour,
                days = day,
                weeks = week,
                months = month
            };
            function.waitTime = GameTime.Convert(gameTime);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
