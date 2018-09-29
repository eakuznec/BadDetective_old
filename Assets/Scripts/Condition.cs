using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Condition : MonoBehaviour
    {
        iConditionContainer conditionContainer;
        public ConditionType type;
        public MainState mainState;
        public QuestEvent questEvent;
        public QuestState questState;
        public QuestTask task;
        public GameTime minTime;
        public GameTime maxTime;
        public Comparator comparator;
        public int intValue;
        public bool boolValue;
        public string stringValue;

        public bool isFulfilled(iConditionContainer conditionContainer)
        {
            this.conditionContainer = conditionContainer;
            QuestManager questManager = QuestManager.GetInstantiate();
            if(type == ConditionType.CUR_TIME_IN_HOUR_RANGE)
            {
                Timeline timeline = Timeline.GetInstantiate();
                GameTime gameTime = GameTime.Convert(timeline.GetTime()).GetDayHour();
                return gameTime.InDayRange(minTime, maxTime);
            }
            else if (type == ConditionType.TASK_MAINSTATE)
            {
                return task.mainState == mainState;
            }
            else if(type == ConditionType.QUEST_MAINSTATE)
            {
                Quest quest = this.conditionContainer.GetQuest();
                return quest.mainState == mainState;
            }
            else if(type == ConditionType.QUEST_STATE || type == ConditionType.DIALOG_STATE || type == ConditionType.GLOBAL_STATE)
            {
                if(questState.type == QuestStateType.BOOL)
                {
                    return questState.boolValue == boolValue;
                }
                else if (questState.type == QuestStateType.INT)
                {
                    if(comparator == Comparator.EQUAL)
                    {
                        return questState.intValue == intValue;
                    }
                    else if(comparator == Comparator.NOT_EQUAL)
                    {
                        return questState.intValue != intValue;
                    }
                    else if (comparator == Comparator.LESS)
                    {
                        return questState.intValue < intValue;
                    }
                    else if (comparator == Comparator.LESS_OR_EQUAL)
                    {
                        return questState.intValue <= intValue;
                    }
                    else if (comparator == Comparator.MORE)
                    {
                        return questState.intValue > intValue;
                    }
                    else if (comparator == Comparator.MORE_OR_EQUAL)
                    {
                        return questState.intValue >= intValue;
                    }
                }
                else if (questState.type == QuestStateType.SPECIAL)
                {
                    return questState.specialValue == stringValue;
                }
            }
            return false;
        }

        public void copyContentFrom(Condition other)
        {
            type = other.type;
            mainState = other.mainState;
            questState = other.questState;
            intValue = other.intValue;
            boolValue = other.boolValue;
            stringValue = other.stringValue;
        }

        public Quest GetQuest()
        {
            if(conditionContainer != null)
            {
                return conditionContainer.GetQuest();
            }
            else
            {
                return GetComponentInParent<Quest>();
            }
        }

        public Dialog.Dialog GetDialog()
        {
            if(conditionContainer != null)
            {
                return conditionContainer.GetDialog();
            }
            else
            {
                return GetComponentInParent<Dialog.Dialog>();
            }
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
