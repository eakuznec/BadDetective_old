using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(Quest))]
    public class eQuest : Editor
    {
        bool showStates;
        bool showNotes;
        bool showLogicMap;

        public override void OnInspectorGUI()
        {
            Quest quest = (Quest)target;
            QuestManager questManager = QuestManager.GetInstantiate();
            List<Quest> allQuests = questManager.GetQuests();
            GUIStyle boldStyle = new GUIStyle();
            boldStyle.fontStyle = FontStyle.Bold;
            if (!allQuests.Contains(quest))
            {
                if (eUtils.isPrefab(quest.gameObject))
                {
                    if (GUILayout.Button("!!! Registrate !!!"))
                    {
                        questManager.Registrate(quest);
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("---Обратитесь к префабу для регистрации в Game!---");
                }
                GUILayout.Space(10);
            }
            if (eUtils.isPrefab(quest.gameObject))
            {
                EditorGUILayout.LabelField("---Для редактирования квеста вынесите его на сцену!---");
            }
            else
            {
                if (GUILayout.Button("Edit"))
                {
                    QuestEditor.quest = quest;
                    QuestEditor.ShowEditor();
                }
                GUILayout.Space(10);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("questName"));
                    quest.gameObject.name = string.Format("Quest_{0}", quest.questName);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shortDescription"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("client"));
                GUILayout.Space(10);
                EditorGUILayout.LabelField("Reward", boldStyle);
                eUtils.DrawMoneyInspecor(ref quest.reward);
                GUILayout.Space(10);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("haveStartTime"));
                if (quest.haveStartTime)
                {
                    eUtils.DrawTimeInspector(quest.startTime);
                    eUtils.DrawTimeSelector(ref quest.startTime);
                }
                GUILayout.Space(10);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("withDeadline"));
                if (quest.withDeadline)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("relativeDeadline"));
                }
                else
                {
                    EditorGUILayout.LabelField("");
                }
                EditorGUILayout.EndHorizontal();
                if (quest.withDeadline)
                {
                    eUtils.DrawTimeInspector(quest.deadline);
                    eUtils.DrawTimeSelector(ref quest.deadline);
                }
                GUILayout.Space(10);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("canBeRegistrated"));
                if (quest.canBeRegistrated)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("registrated"));
                }
                GUILayout.Space(10);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("type"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("mainState"));
                GUILayout.Space(10);
                if (GUILayout.Button("Logic Maps"))
                {
                    showLogicMap = !showLogicMap;
                }
                if (showLogicMap)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("startLogicMap"));
                    if (quest.startLogicMap == null)
                    {
                        if(GUILayout.Button("Create", new GUILayoutOption[] { GUILayout.Width(100) }))
                        {
                            GameObject goFolder = null;
                            if (quest.transform.Find("LogicMaps"))
                            {
                                goFolder = quest.transform.Find("LogicMaps").gameObject;
                            }
                            if (goFolder == null)
                            {
                                goFolder = new GameObject("LogicMaps");
                                goFolder.transform.parent = quest.transform;
                            }
                            GameObject newLogicMap = new GameObject(string.Format("LogicMap_{0}_Start", quest.questName));
                            newLogicMap.transform.parent = goFolder.transform;
                            quest.startLogicMap = newLogicMap.AddComponent<LogicMap.LogicMap>();
                            if (LogicMap.LogicMapEditor.editor != null)
                            {
                                LogicMap.LogicMapEditor.editor.Close();
                            }
                            LogicMap.LogicMapEditor.logicMap = quest.startLogicMap;
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
                            LogicMap.LogicMapEditor.logicMap = quest.startLogicMap;
                            LogicMap.LogicMapEditor.ShowEditor();
                        }
                    }

                    EditorGUILayout.EndHorizontal();
                }
                GUILayout.Space(10);
                if (GUILayout.Button(string.Format("File Notes ({0})", quest.notes.Count)))
                {
                    showNotes = !showNotes;
                }
                if (showNotes)
                {
                    GUI.skin.label.wordWrap = true;
                    foreach(FileNote note in quest.notes)
                    {
                        GUILayout.Label(note.note);
                    }
                }
                GUILayout.Space(10);
                if (GUILayout.Button(string.Format("Quest States ({0})", quest.questStates.Count)))
                    {
                        showStates = !showStates;
                    }
                    if (showStates)
                    {
                        for(int i=0; i< quest.questStates.Count; i++)
                        {
                            QuestState state = quest.questStates[i];
                            SerializedObject soState = new SerializedObject(state);
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.BeginVertical();
                            EditorGUILayout.PropertyField(soState.FindProperty("stateName"));
                            if(state.stateName != state.name)
                            {
                                state.name = state.stateName;
                            }
                            EditorGUILayout.PropertyField(soState.FindProperty("type"));
                            if(state.type == QuestStateType.INT)
                            {
                                EditorGUILayout.PropertyField(soState.FindProperty("intValue"));
                            }
                            else if(state.type == QuestStateType.BOOL)
                            {
                                EditorGUILayout.PropertyField(soState.FindProperty("boolValue"));
                            }
                            else if(state.type == QuestStateType.SPECIAL)
                            {
                                int index = state.possibleValue.IndexOf(state.specialValue);
                                index = EditorGUILayout.Popup(index, state.possibleValue.ToArray());
                                if (index == -1)
                                {
                                    state.specialValue = "";
                                }
                                else
                                {
                                    state.specialValue = state.possibleValue[index];
                                }
                                EditorGUILayout.PropertyField(soState.FindProperty("possibleValue"), true);
                            }
                            EditorGUILayout.EndVertical();
                            soState.ApplyModifiedProperties();
                            if (GUILayout.Button("Delete", new GUILayoutOption[] { GUILayout.Width(60) }))
                            {
                                quest.questStates.RemoveAt(i);
                                DestroyImmediate(state.gameObject);
                                i--;
                            }
                            EditorGUILayout.EndHorizontal();
                            GUILayout.Box("", new GUILayoutOption[] { GUILayout.Height(1), GUILayout.ExpandWidth(true) });
                        }
                    if (GUILayout.Button("Add Quest State"))
                    {
                        AddQuestState();
                    }
                }
                GUILayout.Space(10);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("questEvents"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("notes"), true);
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(quest);
            }
        }

        private void AddQuestState()
        {
            Quest quest = (Quest)target;
            GameObject goFolder;
            if (quest.transform.Find("QuestStates") != null)
            {
                goFolder = quest.transform.Find("QuestStates").gameObject;
            }
            else
            {
                goFolder = new GameObject("QuestStates");
                goFolder.transform.parent = quest.transform;
            }
            GameObject goState = new GameObject("QuestState");
            goState.transform.parent = goFolder.transform;
            QuestState state = goState.AddComponent<QuestState>();
            state.stateName = state.name;
            quest.questStates.Add(state);
        }
    }
}
