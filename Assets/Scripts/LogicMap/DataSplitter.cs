using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public class DataSplitter : LogicFunction, iBoolData
    {
        public bool checkNode;
        public bool result;
        public BaseFunction dataInput;

        public bool trueFlag;
        public LogicFunction trueOutput;
        public bool realizeTrue;
        public LogicFunction falseOutput;
        public bool realizeFalse;

        public bool GetChackDataNode()
        {
            return checkNode;
        }

        public bool GetResult()
        {
            if (!checkNode)
            {
                if(dataInput is LogicCondition)
                {
                    result = ((LogicCondition)dataInput).GetResult();
                }
                else if (dataInput is LogicDataFunction)
                {
                    result = ((LogicDataFunction)dataInput).GetResult();
                }
                else if (dataInput is DataVariable)
                {
                    result = ((DataVariable)dataInput).GetResult();
                }
                checkNode = true;
            }
            return result;
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

        public void RemoveDataInput(BaseFunction input)
        {
            if(dataInput == input)
            {
                dataInput = null;
            }
        }

        public void RemoveDataOutput(BaseFunction output)
        {
        }

        public void SetCheckDataNode(bool check)
        {
            checkNode = check;
        }

        public void SetDataInputLink(BaseFunction input)
        {
            if(dataInput != null)
            {
                if (dataInput is LogicCondition)
                {
                    ((LogicCondition)dataInput).RemoveDataOutput(this);
                }
                else if (dataInput is LogicDataFunction)
                {
                    ((LogicDataFunction)dataInput).RemoveDataOutput(this);
                }
                else if (dataInput is DataVariable)
                {
                    ((DataVariable)dataInput).RemoveDataOutput(this);
                }
            }
            dataInput = input;
        }

        public void SetDataOutputLink(BaseFunction output)
        {
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
    }
}
