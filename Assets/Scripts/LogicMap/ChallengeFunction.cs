using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public class ChallengeFunction : LogicFunction
    {
        public TeamChallenge challenge;

        public bool trueFlag;
        public LogicFunction trueOutput;
        public bool realizeTrue;
        public LogicFunction falseOutput;
        public bool realizeFalse;

        public override void RemoveActionInput(LogicFunction logicFunction)
        {
            if (actionInput == logicFunction)
            {
                actionInput = null;
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

        public bool Realize(iLogicMapContainer owner)
        {
            Team team = null;
            if(owner is QuestTask)
            {
                team = ((QuestTask)owner).GetTeam();
            }
            return challenge.Challage(team);
        }
    }
}
