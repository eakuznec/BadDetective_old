using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Condition : MonoBehaviour
    {
        public ConditionType type;
        public Quest quest;
        public MainState questMainState;
        public QuestState questState;
        public GameTime minTime;
        public GameTime maxTime;
        public int intValue;
        public bool boolValue;
        public string stringValue;

        public bool isFulfilled()
        {
            if(type == ConditionType.CUR_TIME_IN_HOUR_RANGE)
            {
                Timeline timeline = Timeline.GetInstantiate();
                GameTime gameTime = GameTime.Convert(timeline.GetTime()).GetDayHour();
                return gameTime.InDayRange(minTime, maxTime);
            }
            return false;
        }

        public void copyContentFrom(Condition other)
        {
            type = other.type;
            quest = other.quest;
            questMainState = other.questMainState;
            questState = other.questState;
            intValue = other.intValue;
            boolValue = other.boolValue;
            stringValue = other.stringValue;
        }
    }
}
