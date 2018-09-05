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
            GUILayout.Label("Crown");
            newMoney.crown = EditorGUILayout.DelayedIntField(money.crown);
            GUILayout.Label("Libra");
            newMoney.libra = EditorGUILayout.DelayedIntField(money.libra);
            GUILayout.Label("Penny");
            newMoney.penny = EditorGUILayout.DelayedIntField(money.penny);
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

        public static void DrawConditionSelector(Condition condition)
        {
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
            EditorGUILayout.EndVertical();
            soCondition.ApplyModifiedProperties();
            EditorUtility.SetDirty(condition.gameObject);
        }

        public static void DrawEffectSelector(Effect effect)
        {
            SerializedObject soEffect = new SerializedObject(effect);
            QuestManager questManager = QuestManager.GetInstantiate();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(soEffect.FindProperty("type"), GUIContent.none);
            if (effect.type == EffectType.CHANGE_QUEST)
            {
                int index = EditorGUILayout.Popup(questManager.GetQuests().IndexOf(effect.quest), questManager.GetQuestNames());
                if (index != -1)
                {
                    effect.quest = questManager.GetQuests()[index];
                }
                if (effect.quest != null)
                {
                    EditorGUILayout.PropertyField(soEffect.FindProperty("mainState"), GUIContent.none);
                }
            }
            else if (effect.type == EffectType.CHANGE_TASK)
            {
                int index = EditorGUILayout.Popup(questManager.GetQuests().IndexOf(effect.quest), questManager.GetQuestNames());
                if (index != -1)
                {
                    effect.quest = questManager.GetQuests()[index];
                }
                if (effect.quest != null)
                {
                    int eventIndex = EditorGUILayout.Popup(effect.quest.GetEvents().IndexOf(effect.questEvent), effect.quest.GetEventNames());
                    if (eventIndex != -1)
                    {
                        effect.questEvent = effect.quest.GetEvents()[eventIndex];
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
            else if (effect.type == EffectType.ADD_FILE_NOTE)
            {
                int index = EditorGUILayout.Popup(questManager.GetQuests().IndexOf(effect.quest), questManager.GetQuestNames());
                if (index != -1)
                {
                    effect.quest = questManager.GetQuests()[index];
                }
                if (effect.quest != null)
                {
                    EditorGUILayout.PropertyField(soEffect.FindProperty("fileNote"), GUIContent.none);
                }
            }
            else if (effect.type == EffectType.START_DIALOG)
            {
                EditorGUILayout.PropertyField(soEffect.FindProperty("dialog"), GUIContent.none);
            }
            else if (effect.type == EffectType.REALIZE_LOGIC_MAP)
            {
                EditorGUILayout.PropertyField(soEffect.FindProperty("logicMapOwner"), GUIContent.none);
                if (effect.logicMapOwner == LogicMap.LogicMapOwnerType.QUEST)
                {
                    int index = EditorGUILayout.Popup(questManager.GetQuests().IndexOf(effect.quest), questManager.GetQuestNames());
                    if (index != -1)
                    {
                        effect.quest = questManager.GetQuests()[index];
                    }
                    if(effect.quest != null)
                    {
                        index = EditorGUILayout.Popup(effect.quest.GetLogicMaps().IndexOf(effect.logicMap), effect.quest.GetLogicMapNames().ToArray());
                        if (index != -1)
                        {
                            effect.logicMap = effect.quest.GetLogicMaps()[index];
                        }
                    }
                }
                else if (effect.logicMapOwner == LogicMap.LogicMapOwnerType.QUEST_TASK)
                {
                    int index = EditorGUILayout.Popup(questManager.GetQuests().IndexOf(effect.quest), questManager.GetQuestNames());
                    if (index != -1)
                    {
                        effect.quest = questManager.GetQuests()[index];
                    }
                    if (effect.quest != null)
                    {
                        int eventIndex = EditorGUILayout.Popup(effect.quest.GetEvents().IndexOf(effect.questEvent), effect.quest.GetEventNames());
                        if (eventIndex != -1)
                        {
                            effect.questEvent = effect.quest.GetEvents()[eventIndex];
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
                                index = EditorGUILayout.Popup(effect.task.GetLogicMaps().IndexOf(effect.logicMap), effect.task.GetLogicMapNames().ToArray());
                                if (index != -1)
                                {
                                    effect.logicMap = effect.task.GetLogicMaps()[index];
                                }
                            }
                        }
                    }
                }
            }
            else if(effect.type == EffectType.CHECK_QUEST)
            {
                int index = EditorGUILayout.Popup(questManager.GetQuests().IndexOf(effect.quest), questManager.GetQuestNames());
                if (index != -1)
                {
                    effect.quest = questManager.GetQuests()[index];
                }
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
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(string.Format("logicMaps.Array.data[{0}]", i)), GUIContent.none);
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
                            LogicMap.LogicMapEditor.logicMap = logicMaps[i];
                            LogicMap.LogicMapEditor.ShowEditor();
                        }
                        if (GUILayout.Button("Delete", new GUILayoutOption[] { GUILayout.Width(100) }))
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