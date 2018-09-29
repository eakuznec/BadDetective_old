using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(QuestTask))]
    public class eQuestTask : Editor
    {
        bool showLogicMaps;

        public override void OnInspectorGUI()
        {
            QuestTask questTask = (QuestTask)target;
            base.OnInspectorGUI();
            int startIndex = EditorGUILayout.Popup("Start Logic Map", questTask.GetLogicMaps().IndexOf(questTask.startLogicMap), questTask.GetLogicMapNames().ToArray());
            if (startIndex != -1)
            {
                questTask.startLogicMap = questTask.logicMaps[startIndex];
            }
            if (eUtils.isPrefab(questTask))
            {
                EditorGUILayout.LabelField("---Для редактирования вынести на сцену!---");
            }
            else
            {
                EditorGUILayout.Separator();
                eUtils.DrawLogicMapList(questTask.logicMaps, questTask.transform, ref showLogicMaps, serializedObject);
            }
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(questTask.gameObject);
        }
    }
}
