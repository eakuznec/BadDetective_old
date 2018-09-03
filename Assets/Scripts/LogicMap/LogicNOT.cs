using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public class LogicNOT : LogicDataFunction
    {
        public BaseFunction dataInput;

        public override bool GetResult()
        {
            if (!checkNode)
            {
                if (dataInput is LogicCondition)
                {
                    result = !((LogicCondition)dataInput).GetResult();
                }
                else if (dataInput is LogicDataFunction)
                {
                    result = !((LogicDataFunction)dataInput).GetResult();
                }
                else if (dataInput is DataVariable)
                {
                    result = !((DataVariable)dataInput).GetResult();
                }

                checkNode = true;
            }
            return result;
        }

        public override void RemoveDataInput(BaseFunction input)
        {
            if(dataInput == input)
            {
                dataInput = null;
            }
        }

        public override void SetDataInputLink(BaseFunction input)
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
    }
}