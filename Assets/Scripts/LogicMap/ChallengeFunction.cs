using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BadDetective.LogicMap
{
    public class ChallengeFunction : LogicFunction
    {
        public TeamChallenge challenge;

        public bool trueFlag;
        public LogicFunction trueOutput;
        public FileNote trueFileNote;
        public bool realizeTrue;
        public LogicFunction falseOutput;
        public FileNote falseFileNote;
        public bool realizeFalse;
        public WaitType waitType = WaitType.RELATION;
        public GameTime waitTime;

        public override void RemoveActionInput(LogicFunction logicFunction)
        {
            for(int i=0; i<actionInputs.Count; i++)
            {
                if(actionInputs[i] == logicFunction)
                {
                    actionInputs.RemoveAt(i);
                    break;
                }
            }
        }

        public override void RemoveActionOutput(LogicFunction logicFunction)
        {
            if (trueOutput == logicFunction && falseOutput != logicFunction)
            {
                trueOutput = null;
            }
            else if (falseOutput == logicFunction && trueOutput != logicFunction)
            {
                falseOutput = null;
            }
            else if (trueOutput == logicFunction && falseOutput == logicFunction)
            {
                if (trueFlag)
                {
                    falseOutput = null;
                }
                else
                {
                    trueOutput = null;
                }
            }
        }

        public void SetOutputLink(LogicFunction output)
        {
            if (trueFlag)
            {
                if (trueOutput != null)
                {
                    trueOutput.RemoveActionInput(this);
                }
                trueOutput = output;
            }
            else
            {
                if (falseOutput != null)
                {
                    falseOutput.RemoveActionInput(this);
                }
                falseOutput = output;
            }
        }

        private bool Realize(iLogicMapContainer owner)
        {
            Team team = null;
            if(owner is QuestTask)
            {
                team = ((QuestTask)owner).GetTeam();
            }
            return challenge.Challage(team);
        }

        public void CreateWaitAction(iLogicMapContainer owner, UnityAction trueAction, UnityAction falseAction)
        {
            Timeline timeline = Timeline.GetInstantiate();
            GameObject goAction = new GameObject(string.Format("TimelineAction_WaitChallenge"));
            goAction.transform.parent = timeline.transform;
            TimelineAction waitAction = goAction.AddComponent<TimelineAction>();
            waitAction.actionType = TimelineActionType.WAIT;
            waitAction.action = delegate
            {
                bool result = Realize(owner);
                if (result)
                {
                    trueAction();
                }
                else
                {
                    falseAction();
                }
            };
            if (waitType == WaitType.ABSOLUTE)
            {
                waitAction.timer = GameTime.ConvertToFloat(waitTime);
            }
            else if (waitType == WaitType.RELATION)
            {
                waitAction.timer = timeline.GetTime() + GameTime.ConvertToFloat(waitTime);
            }
            else if (waitType == WaitType.ABSOLUTE_HOURS)
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
            Debug.Log(string.Format("ждать челендж {0}", waitTime.ToString()), this);
        }
    }
}
