using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(QuestTask))]
    public class eQuestTask : Editor
    {
        public override void OnInspectorGUI()
        {
            QuestTask questTask = (QuestTask)target;
            base.OnInspectorGUI();
            questTask.name = string.Format("QuestTask_{0}", questTask.taskName);
            if (eUtils.isPrefab(questTask))
            {
                EditorGUILayout.LabelField("---Для редактирования вынести на сцену!---");
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("logicMap"));
                if (questTask.logicMap == null)
                {
                    if (GUILayout.Button("Create", new GUILayoutOption[] { GUILayout.Width(100)}))
                    {
                        GameObject goFolder = null;
                        if (questTask.transform.Find("LogicMaps"))
                        {
                            goFolder = questTask.transform.Find("LogicMaps").gameObject;
                        }
                        if (goFolder == null)
                        {
                            goFolder = new GameObject("LogicMaps");
                            goFolder.transform.parent = questTask.transform;
                        }
                        GameObject newLogicMap = new GameObject(string.Format("LogicMap_{0}", questTask.taskName));
                        newLogicMap.transform.parent = goFolder.transform;
                        questTask.logicMap = newLogicMap.AddComponent<LogicMap.LogicMap>();
                        if (LogicMap.LogicMapEditor.editor != null)
                        {
                            LogicMap.LogicMapEditor.editor.Close();
                        }
                        LogicMap.LogicMapEditor.logicMap = questTask.logicMap;
                        LogicMap.LogicMapEditor.ShowEditor();
                    }
                }
                else
                {
                    if (GUILayout.Button("Edit", new GUILayoutOption[] { GUILayout.Width(100) }))
                    {
                        if (LogicMap.LogicMapEditor.editor != null)
                        {
                            LogicMap.LogicMapEditor.editor.Close();
                        }
                        LogicMap.LogicMapEditor.logicMap = questTask.logicMap;
                        LogicMap.LogicMapEditor.ShowEditor();
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(questTask.gameObject);
        }
    }
}
