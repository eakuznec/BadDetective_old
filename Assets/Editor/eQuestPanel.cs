using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.UI
{
    [CustomEditor (typeof (QuestPanel))]
    public class eQuestPanel : Editor
    {
        public override void OnInspectorGUI()
        {
            QuestPanel questPanel = (QuestPanel)target;
            if (eUtils.isPrefab(questPanel))
            {
                EditorGUILayout.LabelField("---Для редактирования вынесите на сцену!---");
            }
            else
            {
                base.OnInspectorGUI();
                GUILayout.Space(10);
                QuestManager questManager = QuestManager.GetInstantiate();
                int index = EditorGUILayout.Popup(questManager.GetQuests().IndexOf(questPanel.quest), questManager.GetQuestNames());
                if (index != -1)
                {
                    questPanel.quest = questManager.GetQuests()[index];
                }
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("ignorLogicMap"));
                if (questPanel.ignorLogicMap == null)
                {
                    if (GUILayout.Button("Create", new GUILayoutOption[] { GUILayout.Width(100) }))
                    {
                        GameObject goFolder = null;
                        if (questPanel.transform.Find("LogicMaps"))
                        {
                            goFolder = questPanel.transform.Find("LogicMaps").gameObject;
                        }
                        if (goFolder == null)
                        {
                            goFolder = new GameObject("LogicMaps");
                            goFolder.transform.parent = questPanel.transform;
                        }
                        GameObject newLogicMap = new GameObject(string.Format("LogicMap_{0}_Ignore", questPanel.quest.questName));
                        newLogicMap.transform.parent = goFolder.transform;
                        questPanel.ignorLogicMap = newLogicMap.AddComponent<LogicMap.LogicMap>();
                        if (LogicMap.LogicMapEditor.editor != null)
                        {
                            LogicMap.LogicMapEditor.editor.Close();
                        }
                        LogicMap.LogicMapEditor.logicMap = questPanel.ignorLogicMap;
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
                        LogicMap.LogicMapEditor.logicMap = questPanel.ignorLogicMap;
                        LogicMap.LogicMapEditor.ShowEditor();
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("acceptLogicMap"));
                if (questPanel.acceptLogicMap == null)
                {
                    if (GUILayout.Button("Create", new GUILayoutOption[] { GUILayout.Width(100) }))
                    {
                        GameObject goFolder = null;
                        if (questPanel.transform.Find("LogicMaps"))
                        {
                            goFolder = questPanel.transform.Find("LogicMaps").gameObject;
                        }
                        if (goFolder == null)
                        {
                            goFolder = new GameObject("LogicMaps");
                            goFolder.transform.parent = questPanel.transform;
                        }
                        GameObject newLogicMap = new GameObject(string.Format("LogicMap_{0}_Accept", questPanel.quest.questName));
                        newLogicMap.transform.parent = goFolder.transform;
                        questPanel.acceptLogicMap = newLogicMap.AddComponent<LogicMap.LogicMap>();
                        if (LogicMap.LogicMapEditor.editor != null)
                        {
                            LogicMap.LogicMapEditor.editor.Close();
                        }
                        LogicMap.LogicMapEditor.logicMap = questPanel.acceptLogicMap;
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
                        LogicMap.LogicMapEditor.logicMap = questPanel.acceptLogicMap;
                        LogicMap.LogicMapEditor.ShowEditor();
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorUtility.SetDirty(questManager);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
