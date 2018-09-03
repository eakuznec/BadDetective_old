﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public class LogicCondition : BaseFunction, iBoolData, iHaveNode
    {
        public bool checkNode;
        public bool result;
        public Condition condition;

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
                checkNode = true;
            }
            result = condition.isFulfilled();
            return result;
        }

        public Rect GetWindowRect()
        {
            return windowRect;
        }

        public void RemoveDataInput(BaseFunction input)
        {
        }

        public void RemoveDataOutput(BaseFunction output)
        {
            if(dataOutput == output)
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
        }

        public void SetDataOutputLink(BaseFunction output)
        {
            if(dataOutput != null)
            {
                if (dataOutput is DataSplitter)
                {
                    ((DataSplitter)dataOutput).RemoveDataOutput(this);
                }
                else if (dataOutput is LogicDataFunction)
                {
                    ((LogicDataFunction)dataOutput).RemoveDataOutput(this);
                }
                else if (dataOutput is DataVariable)
                {
                    ((DataVariable)dataOutput).RemoveDataOutput(this);
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