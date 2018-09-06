using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Condition : MonoBehaviour
    {
        public ConditionType type;
        public Quest quest;
        public MainState mainState;
        public QuestState questState;
        public QuestEvent questEvent;
        public QuestTask task;
        public GameTime minTime;
        public GameTime maxTime;
        public Comparator comparator;
        public int intValue;
        public bool boolValue;
        public string stringValue;

        public bool isFulfilled()
        {
            QuestManager questManager = QuestManager.GetInstantiate();
            if(type == ConditionType.CUR_TIME_IN_HOUR_RANGE)
            {
                Timeline timeline = Timeline.GetInstantiate();
                GameTime gameTime = GameTime.Convert(timeline.GetTime()).GetDayHour();
                return gameTime.InDayRange(minTime, maxTime);
            }
            else if (type == ConditionType.TASK_MAINSTATE)
            {
                int questIndex = questManager.GetQuests().IndexOf(quest);
                int eventIndex = -1;
                int taskIndex = -1;
                if(questIndex != -1)
                {
                    eventIndex = questManager.GetQuests()[questIndex].GetEvents().IndexOf(questEvent);
                    taskIndex = questManager.GetQuests()[questIndex].GetEvents()[eventIndex].GetTask().IndexOf(task);
                }
                else
                {
                    questIndex = questManager.GetQuestInstances().IndexOf(quest);
                    eventIndex = questManager.GetQuestInstances()[questIndex].GetEvents().IndexOf(questEvent);
                    taskIndex = questManager.GetQuestInstances()[questIndex].GetEvents()[eventIndex].GetTask().IndexOf(task);
                }
                return questManager.GetQuestInstances()[questIndex].GetEvents()[eventIndex].GetTask()[taskIndex].mainState == mainState;
            }
            else if(type == ConditionType.QUEST_MAINSTATE)
            {
                int questIndex = questManager.GetQuests().IndexOf(quest);
                if (questIndex == -1)
                {
                    questIndex = questManager.GetQuestInstances().IndexOf(quest);
                }
                return questManager.GetQuestInstances()[questIndex].mainState == mainState;
            }
            else if(type == ConditionType.QUEST_STATE)
            {
                int questIndex = questManager.GetQuests().IndexOf(quest);
                int stateIndex = -1;
                if (questIndex != -1)
                {
                    stateIndex = questManager.GetQuests()[questIndex].GetQuestStates().IndexOf(questState);
                }
                else
                {
                    questIndex = questManager.GetQuestInstances().IndexOf(quest);
                    stateIndex = questManager.GetQuestInstances()[questIndex].GetQuestStates().IndexOf(questState);
                }
                if(questState.type == QuestStateType.BOOL)
                {
                    return questManager.GetQuestInstances()[questIndex].GetQuestStates()[stateIndex].boolValue == boolValue;
                }
                else if (questState.type == QuestStateType.INT)
                {
                    if(comparator == Comparator.EQUAL)
                    {
                        return questManager.GetQuestInstances()[questIndex].GetQuestStates()[stateIndex].intValue == intValue;
                    }
                    else if(comparator == Comparator.NOT_EQUAL)
                    {
                        return questManager.GetQuestInstances()[questIndex].GetQuestStates()[stateIndex].intValue != intValue;
                    }
                    else if (comparator == Comparator.LESS)
                    {
                        return questManager.GetQuestInstances()[questIndex].GetQuestStates()[stateIndex].intValue < intValue;
                    }
                    else if (comparator == Comparator.LESS_OR_EQUAL)
                    {
                        return questManager.GetQuestInstances()[questIndex].GetQuestStates()[stateIndex].intValue <= intValue;
                    }
                    else if (comparator == Comparator.MORE)
                    {
                        return questManager.GetQuestInstances()[questIndex].GetQuestStates()[stateIndex].intValue > intValue;
                    }
                    else if (comparator == Comparator.MORE_OR_EQUAL)
                    {
                        return questManager.GetQuestInstances()[questIndex].GetQuestStates()[stateIndex].intValue >= intValue;
                    }
                }
                else if (questState.type == QuestStateType.SPECIAL)
                {
                    return questManager.GetQuestInstances()[questIndex].GetQuestStates()[stateIndex].specialValue == stringValue;
                }
            }
            return false;
        }

        public void copyContentFrom(Condition other)
        {
            type = other.type;
            quest = other.quest;
            mainState = other.mainState;
            questState = other.questState;
            intValue = other.intValue;
            boolValue = other.boolValue;
            stringValue = other.stringValue;
        }
    }

    public enum Comparator
    {
        EQUAL,
        NOT_EQUAL,
        MORE,
        MORE_OR_EQUAL,
        LESS,
        LESS_OR_EQUAL
    }
}
