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
        bool showLogicMaps;
        bool showDialogs;
        bool showObjectives;
        bool showEvents;

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
                EditorGUILayout.PropertyField(serializedObject.FindProperty("questName"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("shortDescription"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("client"));
                GUILayout.Space(10);
                EditorGUILayout.LabelField("Reward", boldStyle);
                eUtils.DrawMoneyInspecor(ref quest.reward);
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
                int startIndex = EditorGUILayout.Popup("Start Logic Map", quest.GetLogicMaps().IndexOf(quest.startLogicMap), quest.GetLogicMapNames().ToArray());
                if (startIndex != -1)
                {
                    quest.startLogicMap = quest.logicMaps[startIndex];
                }
                GUILayout.Space(10);
                eUtils.DrawLogicMapList(quest.logicMaps, quest.transform, ref showLogicMaps, serializedObject);
                GUILayout.Space(10);
                eUtils.DrawDialogList(quest.dialogs, quest.transform, ref showDialogs);
                GUILayout.Space(10);
                eUtils.DrawQuestStateList(quest.questStates, quest.transform, ref showStates, "Quest States");
                GUILayout.Space(10);
                eUtils.DrawQuestObjectiveList(quest.questObjectives, quest.transform, ref showObjectives);
                GUILayout.Space(10);
                eUtils.DrawQuestEventList(quest.questEvents, quest.transform, ref showEvents);
                GUILayout.Space(10);
                if (GUILayout.Button(string.Format("File Notes ({0})", quest.notes.Count)))
                {
                    showNotes = !showNotes;
                }
                if (showNotes)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("notes"), true);
                    GUI.skin.label.wordWrap = true;
                    foreach (FileNoteContainer note in quest.notes)
                    {
                        GUILayout.Label(note.GetText());
                    }
                }

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(quest);
            }
        }
    }
}
