using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public abstract class LogicDataFunction : BaseFunction, iBoolData, iHaveNode
    {
        public bool checkNode;
        public bool result;
        public BaseFunction dataOutput;
        public abstract bool GetResult();

        public Rect windowRect;

        public abstract void SetDataInputLink(BaseFunction input);

        public abstract void RemoveDataInput(BaseFunction input);

        public void SetDataOutputLink(BaseFunction output)
        {
            if (dataOutput != null)
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

        public bool GetChackDataNode()
        {
            return checkNode;
        }

        public Rect GetWindowRect()
        {
            return windowRect;
        }

        public void SetWindowRect(Rect rect)
        {
            windowRect = rect;
        }
    }
}