using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public class LogicSplitter : LogicFunction
    {
        public List<LogicFunction> actionOutputs = new List<LogicFunction>();
        public List<bool> realizeOutputs = new List<bool>();

        public void SetOutputLink(LogicFunction output)
        {
            for(int i=0; i<actionOutputs.Count; i++)
            {
                if (actionOutputs[i] == null)
                {
                    if(output != null)
                    {
                        actionOutputs[i] = output;
                    }
                    else
                    {
                        actionOutputs.RemoveAt(i);
                        realizeOutputs.RemoveAt(i);
                    }
                    break;
                }
            }
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
            int index = actionOutputs.IndexOf(logicFunction);
            if(index != -1)
            {
                actionOutputs.RemoveAt(index);
                realizeOutputs.RemoveAt(index);
            }
        }
    }
}