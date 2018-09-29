using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public class DataVariable : BaseFunction, iBoolData, iHaveNode
    {
        public bool checkNode;
        public bool result;
        public BoolVariable variable;
        public bool checkInput;
        public BaseFunction dataInput;
        public BaseFunction dataOutput;

        public Rect windowRect;

        public bool GetChackDataNode()
        {
            return checkNode;
        }

        public bool GetResult()
        {
            if (!checkNode)
            {
                if (variable != null)
                {
                    if (dataInput == null)
                    {
                        result = variable.value;
                    }
                    else if (checkInput)
                    {
                        result = variable.value;
                    }
                    else
                    {
                        if (dataInput is LogicCondition)
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
                        checkInput = true;
                    }
                }
                else
                {
                    throw new System.NotImplementedException();
                }
                checkNode = true;
            }
            return result;
        }

        public Rect GetWindowRect()
        {
            return windowRect;
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
            if (dataOutput == output)
            {
                dataOutput = null;
            }
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
            if (dataOutput != null)
            {
                if (dataOutput is LogicDataFunction)
                {
                    ((LogicDataFunction)dataOutput).RemoveDataOutput(this);
                }
                else if (dataOutput is DataVariable)
                {
                    ((DataVariable)dataOutput).RemoveDataOutput(this);
                }
                else if (dataOutput is DataSplitter)
                {
                    ((DataSplitter)dataOutput).RemoveDataOutput(this);
                }
            }
            dataOutput = output;
        }

        public void SetWindowRect(Rect rect)
        {
            windowRect = rect;
        }
    }
}