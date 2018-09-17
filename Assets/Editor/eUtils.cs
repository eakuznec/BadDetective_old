using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    public class eUtils : Editor
    {
        public static void DrawMoneyInspecor(ref Money money)
        {
            Money newMoney = new Money();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Crown", new GUILayoutOption[] { GUILayout.Width(50) });
            newMoney.crown = EditorGUILayout.DelayedIntField(money.crown, new GUILayoutOption[] { GUILayout.Width(50)});
            GUILayout.FlexibleSpace();
            GUILayout.Label("Libra", new GUILayoutOption[] { GUILayout.Width(50) });
            newMoney.libra = EditorGUILayout.DelayedIntField(money.libra, new GUILayoutOption[] { GUILayout.Width(50) });
            GUILayout.FlexibleSpace();
            GUILayout.Label("Penny", new GUILayoutOption[] { GUILayout.Width(50) });
            newMoney.penny = EditorGUILayout.DelayedIntField(money.penny, new GUILayoutOption[] { GUILayout.Width(50) });
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            money = Utility.SetMoney(newMoney);
        }

        public static void DrawTimeInspector(float time)
        {
            int weeks;
            int days;
            int hours;
            int minutes;
            weeks = (int)(time / 7 / 24);
            days = (int)((time - weeks * 7 * 24) / 24);
            hours = (int)(time - weeks * 7 * 24 - days * 24);
            minutes = (int)(((time - weeks * 7 * 24 - days * 24) - hours) * 60);
            GUILayout.Label(string.Format("{0} weeks, {1} days, {2}:{3}", weeks, days, hours, minutes.ToString("00")));
        }

        public static void DrawTimeSelector(ref float time)
        {
            int weeks;
            int days;
            int hours;
            int minutes;
            weeks = (int)(time / 7 / 24);
            days = (int)((time - weeks * 7) / 24);
            hours = (int)(time - weeks * 7 * 24 - days * 24);
            minutes = (int)(((time - weeks * 7 * 24 - days * 24) - hours) * 60);
            EditorGUILayout.BeginHorizontal();
            weeks = EditorGUILayout.DelayedIntField(weeks, new GUILayoutOption[] { GUILayout.Width(20) });
            EditorGUILayout.LabelField("weeks,", new GUILayoutOption[] { GUILayout.Width(50) });
            days = EditorGUILayout.DelayedIntField(days, new GUILayoutOption[] { GUILayout.Width(20) });
            EditorGUILayout.LabelField("days,", new GUILayoutOption[] { GUILayout.Width(50) });
            hours = EditorGUILayout.DelayedIntField(hours, new GUILayoutOption[] { GUILayout.Width(20) });
            EditorGUILayout.LabelField(":", new GUILayoutOption[] { GUILayout.Width(10) });
            minutes = EditorGUILayout.DelayedIntField(minutes, new GUILayoutOption[] { GUILayout.Width(20) });
            EditorGUILayout.EndHorizontal();
            time = (float)minutes / 60 + hours + days * 24 + weeks * 7 * 24;
        }

        public static void DrawConditionsSelector(List<Condition> conditions, Transform parent, ref bool show)
        {
            EditorGUILayout.Separator();
            if (GUILayout.Button(string.Format("Conditions ({0}):", conditions.Count)))
            {
                show = !show;
            }
            if (show)
            {
                for (int i = 0; i < conditions.Count; i++)
                {
                    GUILayout.BeginHorizontal("box", new GUILayoutOption[] { GUILayout.Height(48), GUILayout.ExpandWidth(true) });
                    if (GUILayout.Button("Del", new GUILayoutOption[] { GUILayout.Width(45), GUILayout.ExpandHeight(true) }))
                    {
                        EditorGUI.FocusTextInControl("");
                        DestroyImmediate(conditions[i].gameObject);
                        conditions.RemoveAt(i);
                        i--;
                        break;
                    }
                    GUILayout.BeginVertical(new GUILayoutOption[] { GUILayout.Width(45) });
                    if (GUILayout.Button("Up"))
                    {
                        EditorGUI.FocusTextInControl("");
                        if (i > 0)
                        {
                            conditions.Insert(i - 1, conditions[i]);
                            conditions.RemoveAt(i + 1);
                        }
                    }
                    if (GUILayout.Button("Down"))
                    {
                        EditorGUI.FocusTextInControl("");
                        if (i < conditions.Count - 1)
                        {
                            conditions.Insert(i + 2, conditions[i]);
                            conditions.RemoveAt(i);
                        }
                    }
                    GUILayout.EndVertical();
                    DrawConditionSelector(conditions[i]);
                    GUILayout.EndHorizontal();
                }
                if (GUILayout.Button("Add condition"))
                {
                    GameObject goCondition;
                    if (parent.transform.Find("Conditions") == null)
                    {
                        goCondition = new GameObject("Conditions");
                        goCondition.transform.parent = parent;
                    }
                    else
                    {
                        goCondition = parent.Find("Conditions").gameObject;
                    }
                    GameObject go = new GameObject("Condition");
                    go.transform.parent = goCondition.transform;
                    Condition condition = go.AddComponent<Condition>();
                    conditions.Add(condition);
                }
            }
        }

        public static void DrawPointOnMapSelector(ref Tier tier, ref PointOnMap point)
        {
            MapManager mapManager = MapManager.GetInstantiate();
            Ring ring = mapManager.ring;
            List<Tier> tiers = ring.GetTiers();
            int tierIndex = tiers.IndexOf(tier);
            int pointIndex = -1;
            if (tierIndex != -1)
            {
                pointIndex = tier.GetPoints().IndexOf(point);
            }
            tierIndex = EditorGUILayout.Popup(tierIndex, ring.GetTierNames().ToArray());
            if (tierIndex != -1)
            {
                tier = tiers[tierIndex];
                if (tiers[tierIndex].GetPoints().Count <= pointIndex)
                {
                    pointIndex = -1;
                }
                if (tiers[tierIndex].GetPoints().Count > 0)
                {
                    pointIndex = EditorGUILayout.Popup(pointIndex, tiers[tierIndex].GetPointNames().ToArray());
                    if (pointIndex != -1)
                    {
                        point = tiers[tierIndex].GetPoints()[pointIndex];
                    }
                    else
                    {
                        point = null;
                    }
                }
                else
                {
                    point = null;
                }
            }
            else
            {
                tier = null;
                point = null;
            }
        }

        public static void DrawEffectsSelector(List<Effect> effects, Transform parent, ref bool show)
        {
            EditorGUILayout.Separator();
            if (GUILayout.Button(string.Format("Effects ({0}):", effects.Count)))
            {
                show = !show;
            }
            if (show)
            {
                for (int i = 0; i < effects.Count; i++)
                {
                    GUILayout.BeginHorizontal("box", new GUILayoutOption[] { GUILayout.Height(48), GUILayout.ExpandWidth(true) });
                    if (GUILayout.Button("Del", new GUILayoutOption[] { GUILayout.Width(45), GUILayout.ExpandHeight(true) }))
                    {
                        EditorGUI.FocusTextInControl("");
                        DestroyImmediate(effects[i].gameObject);
                        effects.RemoveAt(i);
                        i--;
                        break;
                    }
                    GUILayout.BeginVertical(new GUILayoutOption[] { GUILayout.Width(45) });
                    if (GUILayout.Button("Up"))
                    {
                        EditorGUI.FocusTextInControl("");
                        if (i > 0)
                        {
                            effects.Insert(i - 1, effects[i]);
                            effects.RemoveAt(i + 1);
                        }
                    }
                    if (GUILayout.Button("Down"))
                    {
                        EditorGUI.FocusTextInControl("");
                        if (i < effects.Count - 1)
                        {
                            effects.Insert(i + 2, effects[i]);
                            effects.RemoveAt(i);
                        }
                    }
                    GUILayout.EndVertical();
                    DrawEffectSelector(effects[i]);
                    GUILayout.EndHorizontal();
                }
                if (GUILayout.Button("Add effect"))
                {
                    GameObject goCondition;
                    if (parent.transform.Find("Effects") == null)
                    {
                        goCondition = new GameObject("Effects");
                        goCondition.transform.parent = parent;
                    }
                    else
                    {
                        goCondition = parent.Find("Effects").gameObject;
                    }
                    GameObject go = new GameObject("Effect");
                    go.transform.parent = goCondition.transform;
                    Effect effect = go.AddComponent<Effect>();
                    effects.Add(effect);
                }
            }
        }

        public static void DrawConditionSelector(Condition condition)
        {
            QuestManager questManager = QuestManager.GetInstantiate();
            SerializedObject soCondition = new SerializedObject(condition);
            EditorGUILayout.BeginVertical();
            SerializedProperty parameter = soCondition.FindProperty("type");
            EditorGUILayout.PropertyField(parameter, new GUIContent(""));
            if (condition.type == ConditionType.CUR_TIME_IN_HOUR_RANGE)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Min", new GUILayoutOption[] { GUILayout.Width(40) });
                GUILayout.FlexibleSpace();
                condition.minTime.hours = EditorGUILayout.IntField(GUIContent.none, condition.minTime.hours, new GUILayoutOption[] { GUILayout.Width(40) });
                EditorGUILayout.LabelField("h", new GUILayoutOption[] { GUILayout.Width(20) });
                condition.minTime.minutes = EditorGUILayout.FloatField(GUIContent.none, condition.minTime.minutes, new GUILayoutOption[] { GUILayout.Width(40) });
                EditorGUILayout.LabelField("m", new GUILayoutOption[] { GUILayout.Width(20) });
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Max", new GUILayoutOption[] { GUILayout.Width(40) });
                GUILayout.FlexibleSpace();
                condition.maxTime.hours = EditorGUILayout.IntField(GUIContent.none, condition.maxTime.hours, new GUILayoutOption[] { GUILayout.Width(40) });
                EditorGUILayout.LabelField("h", new GUILayoutOption[] { GUILayout.Width(20) });
                condition.maxTime.minutes = EditorGUILayout.FloatField(GUIContent.none, condition.maxTime.minutes, new GUILayoutOption[] { GUILayout.Width(40) });
                EditorGUILayout.LabelField("m", new GUILayoutOption[] { GUILayout.Width(20) });
                EditorGUILayout.EndHorizontal();
            }
            else if (condition.type == ConditionType.TASK_MAINSTATE)
            {
                Quest quest = condition.GetQuest();
                if (quest != null)
                {
                    int eventIndex = EditorGUILayout.Popup(quest.GetEvents().IndexOf(condition.questEvent), quest.GetEventNames());
                    if (eventIndex != -1)
                    {
                        condition.questEvent = quest.GetEvents()[eventIndex];
                    }
                    if (condition.questEvent != null)
                    {
                        int taskIndex = EditorGUILayout.Popup(condition.questEvent.GetTask().IndexOf(condition.task), condition.questEvent.GetTaskNames());
                        if (taskIndex != -1)
                        {
                            condition.task = condition.questEvent.GetTask()[taskIndex];
                        }
                        if (condition.task != null)
                        {
                            EditorGUILayout.PropertyField(soCondition.FindProperty("mainState"), GUIContent.none);
                        }
                    }
                }
            }
            else if (condition.type == ConditionType.QUEST_STATE)
            {
                Quest quest = condition.GetQuest();
                if (quest != null)
                {
                    int index = EditorGUILayout.Popup(quest.GetQuestStates().IndexOf(condition.questState), quest.GetQuestStateName().ToArray());
                    if(index != -1)
                    {
                        condition.questState = quest.GetQuestStates()[index];
                        if(condition.questState.type == QuestStateType.BOOL)
                        {
                            EditorGUILayout.PropertyField(soCondition.FindProperty("boolValue"), GUIContent.none);
                        }
                        else if (condition.questState.type == QuestStateType.INT)
                        {
                            EditorGUILayout.PropertyField(soCondition.FindProperty("comparator"), GUIContent.none);
                            EditorGUILayout.PropertyField(soCondition.FindProperty("intValue"), GUIContent.none);
                        }
                        else if(condition.questState.type == QuestStateType.SPECIAL)
                        {
                            index = EditorGUILayout.Popup(condition.questState.possibleValue.IndexOf(condition.stringValue), condition.questState.possibleValue.ToArray());
                            if (index == -1)
                            {
                                condition.stringValue = condition.questState.possibleValue[index];
                            }
                        }
                    }
                }
            }
            else if (condition.type == ConditionType.DIALOG_STATE)
            {
                Dialog.Dialog dialog = condition.GetDialog();
                if (dialog != null)
                {
                    int index = EditorGUILayout.Popup(dialog.GetDialogStates().IndexOf(condition.questState), dialog.GetDialogStateNames().ToArray());
                    if (index != -1)
                    {
                        condition.questState = dialog.GetDialogStates()[index];
                        if (condition.questState.type == QuestStateType.BOOL)
                        {
                            EditorGUILayout.PropertyField(soCondition.FindProperty("boolValue"), GUIContent.none);
                        }
                        else if (condition.questState.type == QuestStateType.INT)
                        {
                            EditorGUILayout.PropertyField(soCondition.FindProperty("comparator"), GUIContent.none);
                            EditorGUILayout.PropertyField(soCondition.FindProperty("intValue"), GUIContent.none);
                        }
                        else if (condition.questState.type == QuestStateType.SPECIAL)
                        {
                            index = EditorGUILayout.Popup(condition.questState.possibleValue.IndexOf(condition.stringValue), condition.questState.possibleValue.ToArray());
                            if (index == -1)
                            {
                                condition.stringValue = condition.questState.possibleValue[index];
                            }
                        }
                    }
                }
            }
            else if (condition.type == ConditionType.GLOBAL_STATE)
            {
                Agency agency = Agency.GetInstantiate();
                int index = EditorGUILayout.Popup(agency.GetGlobalStates().IndexOf(condition.questState), agency.GetGlobalStateNames().ToArray());
                if (index != -1)
                {
                    condition.questState = agency.GetGlobalStates()[index];
                    if (condition.questState.type == QuestStateType.BOOL)
                    {
                        EditorGUILayout.PropertyField(soCondition.FindProperty("boolValue"), GUIContent.none);
                    }
                    else if (condition.questState.type == QuestStateType.INT)
                    {
                        EditorGUILayout.PropertyField(soCondition.FindProperty("comparator"), GUIContent.none);
                        EditorGUILayout.PropertyField(soCondition.FindProperty("intValue"), GUIContent.none);
                    }
                    else if (condition.questState.type == QuestStateType.SPECIAL)
                    {
                        index = EditorGUILayout.Popup(condition.questState.possibleValue.IndexOf(condition.stringValue), condition.questState.possibleValue.ToArray());
                        if (index == -1)
                        {
                            condition.stringValue = condition.questState.possibleValue[index];
                        }
                    }
                }
            }
            EditorGUILayout.EndVertical();
            soCondition.ApplyModifiedProperties();
            EditorUtility.SetDirty(condition.gameObject);
        }

        public static void DrawEffectSelector(Effect effect)
        {
            SerializedObject soEffect = new SerializedObject(effect);
            QuestManager questManager = QuestManager.GetInstantiate();
            ItemManager itemManager = ItemManager.GetInstantiate();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(soEffect.FindProperty("type"), GUIContent.none);
            if (effect.type == EffectType.INSTANTIATE_QUEST)
            {
                int index = EditorGUILayout.Popup(questManager.GetQuests().IndexOf(effect.quest), questManager.GetQuestNames());
                if (index != -1)
                {
                    effect.quest = questManager.GetQuests()[index];
                }
            }
            else if (effect.type == EffectType.CHANGE_QUEST)
            {
                Quest quest = effect.GetQuest();
                if (quest != null)
                {
                    EditorGUILayout.PropertyField(soEffect.FindProperty("mainState"), GUIContent.none);
                }
            }
            else if (effect.type == EffectType.CHANGE_TASK)
            {
                Quest quest = effect.GetQuest();
                if (quest != null)
                {
                    int eventIndex = EditorGUILayout.Popup(quest.GetEvents().IndexOf(effect.questEvent), quest.GetEventNames());
                    if (eventIndex != -1)
                    {
                        effect.questEvent = quest.GetEvents()[eventIndex];
                    }
                    if (effect.questEvent != null)
                    {
                        int taskIndex = EditorGUILayout.Popup(effect.questEvent.GetTask().IndexOf(effect.task), effect.questEvent.GetTaskNames());
                        if (taskIndex != -1)
                        {
                            effect.task = effect.questEvent.GetTask()[taskIndex];
                        }
                        if (effect.task != null)
                        {
                            EditorGUILayout.PropertyField(soEffect.FindProperty("mainState"), GUIContent.none);
                        }
                    }
                }
            }
            else if (effect.type == EffectType.CHANGE_OBJECTIVE)
            {
                Quest quest = effect.GetQuest();
                if (quest != null)
                {
                    int index = EditorGUILayout.Popup(quest.GetQuestObjectives().IndexOf(effect.objective), quest.GetQuestObjectiveNames().ToArray());
                    if(index != -1)
                    {
                        effect.objective = quest.GetQuestObjectives()[index];
                    }
                    EditorGUILayout.PropertyField(soEffect.FindProperty("mainState"), GUIContent.none);
                }
            }
            else if (effect.type == EffectType.CHANGE_QUEST_STATE)
            {
                Quest quest = effect.GetQuest();
                if (quest != null)
                {
                    int index = EditorGUILayout.Popup(quest.GetQuestStates().IndexOf(effect.questState), quest.GetQuestStateName().ToArray());
                    if (index != -1)
                    {
                        effect.questState = quest.GetQuestStates()[index];
                        if (effect.questState.type == QuestStateType.BOOL)
                        {
                            EditorGUILayout.PropertyField(soEffect.FindProperty("boolValue"), GUIContent.none);
                        }
                        else if (effect.questState.type == QuestStateType.INT)
                        {
                            EditorGUILayout.PropertyField(soEffect.FindProperty("intValue"), GUIContent.none);
                        }
                        else if (effect.questState.type == QuestStateType.SPECIAL)
                        {
                            index = EditorGUILayout.Popup(effect.questState.possibleValue.IndexOf(effect.stringValue), effect.questState.possibleValue.ToArray());
                            if (index == -1)
                            {
                                effect.stringValue = effect.questState.possibleValue[index];
                            }
                        }
                    }
                }
            }
            else if (effect.type == EffectType.CHANGE_DIALOG_STATE)
            {
                Dialog.Dialog dialog = effect.GetDialog();
                if (dialog != null)
                {
                    int index = EditorGUILayout.Popup(dialog.GetDialogStates().IndexOf(effect.questState), dialog.GetDialogStateNames().ToArray());
                    if (index != -1)
                    {
                        effect.questState = dialog.GetDialogStates()[index];
                        if (effect.questState.type == QuestStateType.BOOL)
                        {
                            EditorGUILayout.PropertyField(soEffect.FindProperty("boolValue"), GUIContent.none);
                        }
                        else if (effect.questState.type == QuestStateType.INT)
                        {
                            EditorGUILayout.PropertyField(soEffect.FindProperty("intValue"), GUIContent.none);
                        }
                        else if (effect.questState.type == QuestStateType.SPECIAL)
                        {
                            index = EditorGUILayout.Popup(effect.questState.possibleValue.IndexOf(effect.stringValue), effect.questState.possibleValue.ToArray());
                            if (index == -1)
                            {
                                effect.stringValue = effect.questState.possibleValue[index];
                            }
                        }
                    }
                }
            }
            else if (effect.type == EffectType.CHANGE_GLOBAL_STATE)
            {
                Agency agency = Agency.GetInstantiate();
                int index = EditorGUILayout.Popup(agency.GetGlobalStates().IndexOf(effect.questState), agency.GetGlobalStateNames().ToArray());
                if (index != -1)
                {
                    effect.questState = agency.GetGlobalStates()[index];
                    if (effect.questState.type == QuestStateType.BOOL)
                    {
                        EditorGUILayout.PropertyField(soEffect.FindProperty("boolValue"), GUIContent.none);
                    }
                    else if (effect.questState.type == QuestStateType.INT)
                    {
                        EditorGUILayout.PropertyField(soEffect.FindProperty("intValue"), GUIContent.none);
                    }
                    else if (effect.questState.type == QuestStateType.SPECIAL)
                    {
                        index = EditorGUILayout.Popup(effect.questState.possibleValue.IndexOf(effect.stringValue), effect.questState.possibleValue.ToArray());
                        if (index == -1)
                        {
                            effect.stringValue = effect.questState.possibleValue[index];
                        }
                    }
                }
            }
            else if (effect.type == EffectType.ADD_FILE_NOTE)
            {
                Quest quest = effect.GetQuest();
                if (quest != null)
                {
                    EditorGUILayout.PropertyField(soEffect.FindProperty("fileNote"), GUIContent.none);
                }
            }
            else if (effect.type == EffectType.ADD_ITEM)
            {
                int index = EditorGUILayout.Popup(itemManager.GetItems().IndexOf(effect.item), itemManager.GetItemsName().ToArray());
                if (index != -1)
                {
                    effect.item = itemManager.GetItems()[index];
                }
            }
            else if (effect.type == EffectType.REALIZE_LOGIC_MAP)
            {
                EditorGUILayout.PropertyField(soEffect.FindProperty("logicMapOwner"), GUIContent.none);
                if (effect.logicMapOwner == LogicMap.LogicMapOwnerType.QUEST)
                {
                    Quest quest = effect.GetQuest();
                    if(quest != null)
                    {
                        int index = EditorGUILayout.Popup(quest.GetLogicMaps().IndexOf(effect.logicMap), quest.GetLogicMapNames().ToArray());
                        if (index != -1)
                        {
                            effect.logicMap = quest.GetLogicMaps()[index];
                        }
                    }
                }
                else if (effect.logicMapOwner == LogicMap.LogicMapOwnerType.QUEST_TASK)
                {
                    Quest quest = effect.GetQuest();
                    if (quest != null)
                    {
                        int eventIndex = EditorGUILayout.Popup(quest.GetEvents().IndexOf(effect.questEvent), quest.GetEventNames());
                        if (eventIndex != -1)
                        {
                            effect.questEvent = quest.GetEvents()[eventIndex];
                        }
                        if (effect.questEvent != null)
                        {
                            int taskIndex = EditorGUILayout.Popup(effect.questEvent.GetTask().IndexOf(effect.task), effect.questEvent.GetTaskNames());
                            if (taskIndex != -1)
                            {
                                effect.task = effect.questEvent.GetTask()[taskIndex];
                            }
                            if (effect.task != null)
                            {
                                int index = EditorGUILayout.Popup(effect.task.GetLogicMaps().IndexOf(effect.logicMap), effect.task.GetLogicMapNames().ToArray());
                                if (index != -1)
                                {
                                    effect.logicMap = effect.task.GetLogicMaps()[index];
                                }
                            }
                        }
                    }
                }
            }
            else if (effect.type == EffectType.REALIZE_TASK)
            {
                Quest quest = effect.GetQuest();
                if (quest != null)
                {
                    int eventIndex = EditorGUILayout.Popup(quest.GetEvents().IndexOf(effect.questEvent), quest.GetEventNames());
                    if (eventIndex != -1)
                    {
                        effect.questEvent = quest.GetEvents()[eventIndex];
                    }
                    if (effect.questEvent != null)
                    {
                        int taskIndex = EditorGUILayout.Popup(effect.questEvent.GetTask().IndexOf(effect.task), effect.questEvent.GetTaskNames());
                        if (taskIndex != -1)
                        {
                            effect.task = effect.questEvent.GetTask()[taskIndex];
                        }
                    }
                }
            }
            else if (effect.type == EffectType.START_DIALOG)
            {
                EditorGUILayout.PropertyField(soEffect.FindProperty("dialog"), GUIContent.none);
                effect.dialog.questOwner = effect.GetQuest();
            }
            else if (effect.type == EffectType.CHECK_QUEST)
            {
            }
            EditorGUILayout.EndVertical();
            soEffect.ApplyModifiedProperties();
        }

        public static void DrawLogicMapList(List<LogicMap.LogicMap> logicMaps, Transform parent, ref bool show, SerializedObject serializedObject)
        {
            if (GUILayout.Button(string.Format("Logic Maps ({0})", logicMaps.Count)))
            {
                show = !show;
            }
            if (show)
            {
                for (int i = 0; i < logicMaps.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (logicMaps[i] != null)
                    {
                        logicMaps[i].logicMapName = EditorGUILayout.TextField(logicMaps[i].logicMapName);
                    }
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(string.Format("logicMaps.Array.data[{0}]", i)), GUIContent.none, new GUILayoutOption[] { GUILayout.Width(100) });
                    if (logicMaps[i] == null)
                    {
                        if (GUILayout.Button("Create", new GUILayoutOption[] { GUILayout.Width(100) }))
                        {
                            GameObject goFolder = null;
                            if (parent.Find("LogicMaps"))
                            {
                                goFolder = parent.Find("LogicMaps").gameObject;
                            }
                            if (goFolder == null)
                            {
                                goFolder = new GameObject("LogicMaps");
                                goFolder.transform.parent = parent;
                            }
                            GameObject newLogicMap = new GameObject(string.Format("LogicMap_{0}", parent.name));
                            newLogicMap.transform.parent = goFolder.transform;
                            logicMaps[i] = newLogicMap.AddComponent<LogicMap.LogicMap>();
                            LogicMap.LogicMapEditor.logicMap = logicMaps[i];
                            if (LogicMap.LogicMapEditor.editor == null)
                            {
                                LogicMap.LogicMapEditor.ShowEditor();
                            }
                            else
                            {
                                LogicMap.LogicMapEditor.editor.LoadLogicMap();
                            }
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Edit", new GUILayoutOption[] { GUILayout.Width(60) }))
                        {
                            LogicMap.LogicMapEditor.logicMap = logicMaps[i];
                            if(LogicMap.LogicMapEditor.editor == null)
                            {
                                LogicMap.LogicMapEditor.ShowEditor();
                            }
                            else
                            {
                                LogicMap.LogicMapEditor.editor.LoadLogicMap();
                            }
                        }
                        if (GUILayout.Button("Delete", new GUILayoutOption[] { GUILayout.Width(60) }))
                        {
                            DestroyImmediate(logicMaps[i].gameObject);
                            logicMaps.RemoveAt(i);
                            break;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                if (GUILayout.Button("Add Logic Map"))
                {
                    GameObject goFolder = null;
                    if (parent.Find("LogicMaps"))
                    {
                        goFolder = parent.Find("LogicMaps").gameObject;
                    }
                    if (goFolder == null)
                    {
                        goFolder = new GameObject("LogicMaps");
                        goFolder.transform.parent = parent;
                    }
                    GameObject newLogicMap = new GameObject(string.Format("LogicMap_{0}", parent.name));
                    newLogicMap.transform.parent = goFolder.transform;
                    logicMaps.Add(newLogicMap.AddComponent<LogicMap.LogicMap>());
                }
            }
        }

        public static void DrawDialogList(List<Dialog.Dialog> dialogs, Transform parent, ref bool show) 
        {
            if (GUILayout.Button(string.Format("Dialogs ({0})", dialogs.Count)))
            {
                show = !show;
            }
            if (show)
            {
                for (int i = 0; i < dialogs.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (dialogs[i] != null)
                    {
                        dialogs[i].dialogName = EditorGUILayout.TextField(dialogs[i].dialogName);
                        if (GUILayout.Button("Edit", new GUILayoutOption[] { GUILayout.Width(60) }))
                        {
                            DialogEditor.dialog = dialogs[i];
                            if (DialogEditor.editor == null)
                            {
                                DialogEditor.ShowEditor();
                            }
                            else
                            {
                                DialogEditor.editor.LoadDialog();
                            }
                        }
                        if (GUILayout.Button("Delete", new GUILayoutOption[] { GUILayout.Width(60) }))
                        {
                            DestroyImmediate(dialogs[i].gameObject);
                            dialogs.RemoveAt(i);
                            break;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                if (GUILayout.Button("Add Dialog"))
                {
                    GameObject goFolder = null;
                    if (parent.Find("Dialogs"))
                    {
                        goFolder = parent.Find("Dialogs").gameObject;
                    }
                    if (goFolder == null)
                    {
                        goFolder = new GameObject("Dialogs");
                        goFolder.transform.parent = parent;
                    }
                    GameObject newDialog = new GameObject(string.Format("Dialog_{0}", parent.name));
                    newDialog.transform.parent = goFolder.transform;
                    dialogs.Add(newDialog.AddComponent<Dialog.Dialog>());
                }
            }
        }

        public static void DrawQuestObjectiveList(List<QuestObjective> questObjectives, Transform parent, ref bool show)
        {
            if (GUILayout.Button(string.Format("Quest Objectives ({0})", questObjectives.Count)))
            {
                show = !show;
            }
            if (show)
            {
                EditorGUILayout.BeginVertical("box");
                for (int i = 0; i < questObjectives.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (questObjectives[i] != null)
                    {
                        questObjectives[i].objective = EditorGUILayout.TextField(questObjectives[i].objective);
                        GUILayout.Label("Main", new GUILayoutOption[] { GUILayout.Width(40) });
                        questObjectives[i].main = EditorGUILayout.Toggle(GUIContent.none, questObjectives[i].main, new GUILayoutOption[] { GUILayout.Width(40) });
                        questObjectives[i].state = (MainState)EditorGUILayout.EnumPopup(questObjectives[i].state, new GUILayoutOption[] { GUILayout.Width(100) });
                        if (GUILayout.Button("Delete", new GUILayoutOption[] { GUILayout.Width(100) }))
                        {
                            DestroyImmediate(questObjectives[i].gameObject);
                            questObjectives.RemoveAt(i);
                            break;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                if (GUILayout.Button("Add New Objective"))
                {
                    GameObject goFolder = null;
                    if (parent.Find("Objectives"))
                    {
                        goFolder = parent.Find("Objectives").gameObject;
                    }
                    if (goFolder == null)
                    {
                        goFolder = new GameObject("Objectives");
                        goFolder.transform.parent = parent;
                    }
                    GameObject newObjective = new GameObject(string.Format("Objective_{0}", parent.name));
                    newObjective.transform.parent = goFolder.transform;
                    questObjectives.Add(newObjective.AddComponent<QuestObjective>());
                }
                EditorGUILayout.EndVertical();
            }
        }

        public static void DrawQuestEventList(List<QuestEvent> questEvents, Transform parent, ref bool show)
        {
            if (GUILayout.Button(string.Format("Quest Events ({0})", questEvents.Count)))
            {
                show = !show;
            }
            if (show)
            {
                EditorGUILayout.BeginVertical("box");
                for (int i = 0; i < questEvents.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (questEvents[i] != null)
                    {
                        questEvents[i].eventName = EditorGUILayout.TextField(questEvents[i].eventName);
                        if (GUILayout.Button("Edit", new GUILayoutOption[] { GUILayout.Width(60) }))
                        {
                            Selection.activeGameObject = questEvents[i].gameObject;
                            break;
                        }
                        if (GUILayout.Button("Delete", new GUILayoutOption[] { GUILayout.Width(60) }))
                        {
                            DestroyImmediate(questEvents[i].gameObject);
                            questEvents.RemoveAt(i);
                            break;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                if (GUILayout.Button("Add New Event"))
                {
                    GameObject newEvent = new GameObject(string.Format("Event_{0}", parent.name));
                    newEvent.transform.parent = parent;
                    questEvents.Add(newEvent.AddComponent<QuestEvent>());
                }
                EditorGUILayout.EndVertical();
            }
        }

        public static void DrawQuestTaskList(List<QuestTask> questTasks, Transform parent)
        {
            EditorGUILayout.BeginVertical("box");
            for (int i = 0; i < questTasks.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (questTasks[i] != null)
                {
                    questTasks[i].taskName = EditorGUILayout.TextField(questTasks[i].taskName);
                    if (GUILayout.Button("Edit", new GUILayoutOption[] { GUILayout.Width(60) }))
                    {
                        Selection.activeGameObject = questTasks[i].gameObject;
                        break;
                    }
                    if (GUILayout.Button("Delete", new GUILayoutOption[] { GUILayout.Width(60) }))
                    {
                        DestroyImmediate(questTasks[i].gameObject);
                        questTasks.RemoveAt(i);
                        break;
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add New Task"))
            {
                GameObject newTask = new GameObject(string.Format("Task_{0}", parent.name));
                newTask.transform.parent = parent;
                questTasks.Add(newTask.AddComponent<QuestTask>());
            }
            EditorGUILayout.EndVertical();
        }

        public static void DrawQuestStateList(List<QuestState> list, Transform parent, ref bool show, string title)
        {
            if (GUILayout.Button(string.Format("{1} ({0})", list.Count, title)))
            {
                show = !show;
            }
            if (show)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    QuestState state = list[i];
                    SerializedObject soState = new SerializedObject(state);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.PropertyField(soState.FindProperty("stateName"));
                    if (state.stateName != state.name)
                    {
                        state.name = state.stateName;
                    }
                    EditorGUILayout.PropertyField(soState.FindProperty("type"));
                    if (state.type == QuestStateType.INT)
                    {
                        EditorGUILayout.PropertyField(soState.FindProperty("intValue"));
                    }
                    else if (state.type == QuestStateType.BOOL)
                    {
                        EditorGUILayout.PropertyField(soState.FindProperty("boolValue"));
                    }
                    else if (state.type == QuestStateType.SPECIAL)
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
                        list.RemoveAt(i);
                        DestroyImmediate(state.gameObject);
                        i--;
                        break;
                    }
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Box("", new GUILayoutOption[] { GUILayout.Height(1), GUILayout.ExpandWidth(true) });
                }
                if (GUILayout.Button(string.Format("Add {0}", title)))
                {
                    GameObject goFolder;
                    if (parent.Find("QuestStates") != null)
                    {
                        goFolder = parent.Find("QuestStates").gameObject;
                    }
                    else
                    {
                        goFolder = new GameObject("QuestStates");
                        goFolder.transform.parent = parent;
                    }
                    GameObject goState = new GameObject("QuestState");
                    goState.transform.parent = goFolder.transform;
                    QuestState state = goState.AddComponent<QuestState>();
                    state.stateName = state.name;
                    list.Add(state);
                }
            }
        }

        public static bool isPrefab(Object obj)
        {
            return PrefabUtility.GetPrefabType(obj) == PrefabType.Prefab;
        }

        public static Texture2D MakeTex(int width, int height, Color color)
        {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = color;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }
    }
}