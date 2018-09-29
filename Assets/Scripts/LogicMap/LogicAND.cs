using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public class LogicAND : LogicDataFunction
    {
        public BaseFunction dataInputOne;
        public BaseFunction dataInputTwo;
        public bool firstInput = false;

        public override bool GetResult()
        {
            if (!checkNode)
            {
                if(dataInputOne != null)
                {
                    if (dataInputOne is LogicCondition)
                    {
                        result = ((LogicCondition)dataInputOne).GetResult();
                    }
                    else if (dataInputOne is DataVariable)
                    {
                        result = ((DataVariable)dataInputOne).GetResult();
                    }
                    else if (dataInputOne is LogicDataFunction)
                    {
                        result = ((LogicDataFunction)dataInputOne).GetResult();
                    }
                }

                if(dataInputTwo != null)
                {
                    if (dataInputTwo is LogicCondition)
                    {
                        result = (result && ((LogicCondition)dataInputTwo).GetResult());
                    }
                    else if (dataInputTwo is DataVariable)
                    {
                        result = (result && ((DataVariable)dataInputTwo).GetResult());
                    }
                    else if (dataInputTwo is LogicDataFunction)
                    {
                        result = (result && ((LogicDataFunction)dataInputTwo).GetResult());
                    }
                }
                checkNode = true;
            }
            return result;
        }

        public override void RemoveDataInput(BaseFunction input)
        {
            if (dataInputOne == input)
            {
                dataInputOne = null;
            }
            if(dataInputTwo == input)
            {
                dataInputTwo = null;
            }
        }

        public override void SetDataInputLink(BaseFunction input)
        {
            if (firstInput)
            {
                if(dataInputOne != null)
                {
                    if (dataInputOne is LogicCondition)
                    {
                        ((LogicCondition)dataInputOne).RemoveDataOutput(this);
                    }
                    else if (dataInputOne is LogicDataFunction)
                    {
                        ((LogicDataFunction)dataInputOne).RemoveDataOutput(this);
                    }
                    else if (dataInputOne is DataVariable)
                    {
                        ((DataVariable)dataInputOne).RemoveDataOutput(this);
                    }
                }
                dataInputOne = input;
            }
            else
            {
                if(dataInputTwo != null)
                {
                    if (dataInputTwo is LogicCondition)
                    {
                        ((LogicCondition)dataInputTwo).RemoveDataOutput(this);
                    }
                    else if (dataInputTwo is LogicDataFunction)
                    {
                        ((LogicDataFunction)dataInputTwo).RemoveDataOutput(this);
                    }
                    else if (dataInputTwo is DataVariable)
                    {
                        ((DataVariable)dataInputTwo).RemoveDataOutput(this);
                    }

                }
                dataInputTwo = input;
            }
        }
    }
}
