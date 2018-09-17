using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public class ChooseMethodFunction : LogicFunction
    {
        public int method;
        public LogicFunction brutalOutput;
        public bool realizeBrutal;
        public LogicFunction carefulOutput;
        public bool realizeCareful;
        public LogicFunction diplomatOutput;
        public bool realizeDiplomat;
        public LogicFunction scienceOutput;
        public bool realizeScience;
        
        public Dialog.Dialog dialog;
        public List<LogicFunction> dialogOutputs = new List<LogicFunction>();
        public List<bool> realizeDialogOutput = new List<bool>();
        public int dialogOutputNum;
        public bool dialogOutputFlag;

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
            if (brutalOutput == logicFunction && carefulOutput != logicFunction && diplomatOutput != logicFunction && scienceOutput != logicFunction)
            {
                brutalOutput = null;
            }
            else if (brutalOutput != logicFunction && carefulOutput == logicFunction && diplomatOutput != logicFunction && scienceOutput != logicFunction)
            {
                carefulOutput = null;
            }
            else if (brutalOutput != logicFunction && carefulOutput != logicFunction && diplomatOutput == logicFunction && scienceOutput != logicFunction)
            {
                diplomatOutput = null;
            }
            else if (brutalOutput != logicFunction && carefulOutput != logicFunction && diplomatOutput != logicFunction && scienceOutput == logicFunction)
            {
                scienceOutput = null;
            }
            else
            {
                if (brutalOutput == logicFunction && method != 0)
                {
                    brutalOutput = null;
                }
                if (carefulOutput == logicFunction && method != 1)
                {
                    carefulOutput = null;
                }
                if (diplomatOutput == logicFunction && method != 2)
                {
                    diplomatOutput = null;
                }
                if (scienceOutput == logicFunction && method != 3)
                {
                    scienceOutput = null;
                }
            }
            for(int i=0; i< dialogOutputs.Count;i++)
            {
                if(dialogOutputs[i] == logicFunction)
                {
                    dialogOutputs[i] = null;
                }
            }
        }

        public void SetOutputLink(LogicFunction output)
        {
            if (!dialogOutputFlag)
            {
                if (method == 0)
                {
                    if (brutalOutput != null)
                    {
                        brutalOutput.RemoveActionInput(this);
                    }
                    brutalOutput = output;
                }
                else if (method == 1)
                {
                    if (carefulOutput != null)
                    {
                        carefulOutput.RemoveActionInput(this);
                    }
                    carefulOutput = output;
                }
                else if (method == 2)
                {
                    if (diplomatOutput != null)
                    {
                        diplomatOutput.RemoveActionInput(this);
                    }
                    diplomatOutput = output;
                }
                else if (method == 3)
                {
                    if (scienceOutput != null)
                    {
                        scienceOutput.RemoveActionInput(this);
                    }
                    scienceOutput = output;
                }
            }
            else
            {
                if (dialogOutputs[dialogOutputNum] != null)
                {
                    dialogOutputs[dialogOutputNum].RemoveActionInput(this);
                }
                dialogOutputs[dialogOutputNum] = output;
            }
        }

        public Method Realize(iLogicMapContainer owner)
        {
            Team team = null;
            if (owner is QuestTask)
            {
                team = ((QuestTask)owner).GetTeam();
            }
            return team.GetPriorityMethod(brutalOutput != null, carefulOutput!=null, diplomatOutput!=null, scienceOutput!=null);
        }
    }
}