using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BadDetective.LogicMap
{
    public class WaitFunction : LogicFunction
    {
        public LogicFunction actionOutput;
        public bool realize;

        public bool checkNode;

        public WaitType waitType;
        public GameTime waitTime;

        public void SetOutputLink(LogicFunction output)
        {
            if (output != null)
            {
                output.RemoveActionInput(this);
            }

            actionOutput = output;
        }

        public override void RemoveActionInput(LogicFunction logicFunction)
        {
            for (int i = 0; i < actionInputs.Count; i++)
            {
                if (actionInputs[i] == logicFunction)
                {
                    actionInputs.RemoveAt(i);
                    break;
                }
            }
        }

        public override void RemoveActionOutput(LogicFunction logicFunction)
        {
            if (actionOutput == logicFunction)
            {
                actionOutput = null;
            }
        }

        public void CreateWaitAction(UnityAction action)
        {
            Timeline timeline = Timeline.GetInstantiate();
            GameObject goAction = new GameObject(string.Format("TimelineAction_Wait"));
            goAction.transform.parent = timeline.transform;
            TimelineAction waitAction = goAction.AddComponent<TimelineAction>();
            waitAction.actionType = TimelineActionType.WAIT;
            waitAction.action = delegate 
            {
                realize = true;
                action();
            };
            if(waitType == WaitType.ABSOLUTE)
            {
                waitAction.timer = GameTime.ConvertToFloat(waitTime);
            }
            else if(waitType == WaitType.RELATION)
            {
                waitAction.timer = timeline.GetTime() + GameTime.ConvertToFloat(waitTime);
            }
            else if(waitType == WaitType.ABSOLUTE_HOURS)
            {

                GameTime curTime = GameTime.Convert(timeline.GetTime());
                if (curTime.GetDayHour() <= waitTime)
                {
                    GameTime resultTime = new GameTime()
                    {
                        minutes = waitTime.minutes,
                        hours = waitTime.hours,
                        days = curTime.days,
                        weeks = curTime.weeks,
                        months = curTime.months
                    };
                    waitAction.timer = GameTime.ConvertToFloat(resultTime);
                }
                else if (curTime.GetDayHour() > waitTime)
                {
                    GameTime resultTime = new GameTime()
                    {
                        minutes = waitTime.minutes,
                        hours = waitTime.hours,
                        days = curTime.days + 1,
                        weeks = curTime.weeks,
                        months = curTime.months
                    };
                    waitAction.timer = GameTime.ConvertToFloat(resultTime);
                }
            }
            timeline.RegistrateAction(waitAction);
        }
    }

    public enum WaitType
    {
        RELATION,
        ABSOLUTE,
        ABSOLUTE_HOURS
    }
}
