using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    [ExecuteInEditMode]
    public class LogicMap : MonoBehaviour
    {
        public string logicMapName;
        public iLogicMapContainer curOwner;
        public List<iLogicMapContainer> owners = new List<iLogicMapContainer>();
        public bool startRealize;
        public LogicFunction startFunction;
        public List<LogicFunction> logicFunc = new List<LogicFunction>();
        public List<LogicDataFunction> dataFunc = new List<LogicDataFunction>();
        public List<DataVariable> dataVariables = new List<DataVariable>();
        public List<BoolVariable> boolVariables = new List<BoolVariable>();
        public List<LogicCondition> conditions = new List<LogicCondition>();
        public List<LogicEffect> effects = new List<LogicEffect>();

        public Rect enterWindowRect = new Rect(50, 50, 100, 40);

        public void CheckBoolVariable(DataVariable dataVariable, string name, bool value)
        {
            BoolVariable boolVariable = null;
            if(name != "")
            {
                foreach (BoolVariable variable in boolVariables)
                {
                    if (variable.variableName == name)
                    {
                        boolVariable = variable;
                        boolVariable.value = value;
                    }
                }
                if(boolVariable == null)
                {
                    boolVariable = CreateBoolVariable(name, value);
                }
            }
            dataVariable.variable = boolVariable;
            CheckVariables();
        }

        private void CheckVariables()
        {
            for(int i = 0; i < boolVariables.Count; i++)
            {
                BoolVariable variable = boolVariables[i];
                bool flag = false;
                foreach (DataVariable dataVariable in dataVariables)
                {
                    if (dataVariable.variable == variable)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    boolVariables.RemoveAt(i);
                    i--;
                    DestroyImmediate(variable.gameObject);
                }
            }
        }

        private BoolVariable CreateBoolVariable(string name, bool value)
        {
            GameObject goBoolVariables;
            if (transform.Find("BoolVariables") == null)
            {
                goBoolVariables = new GameObject("BoolVariables");
                goBoolVariables.transform.parent = transform;
            }
            else
            {
                goBoolVariables = transform.Find("BoolVariables").gameObject;
            }
            GameObject goBoolVariable = new GameObject("BoolVariable");
            BoolVariable boolVariable = goBoolVariable.AddComponent<BoolVariable>();
            boolVariable.variableName = name;
            boolVariable.value = value;
            goBoolVariable.transform.parent = goBoolVariables.transform;
            boolVariables.Add(boolVariable);
            return boolVariable;
        }

        public void RealizeLogicMap(iLogicMapContainer owner, bool isTest = false)
        {
            curOwner = owner;
            ClearCheck();
            startRealize = true;
            RealizeFunction(startFunction, isTest);
        }

        private void RealizeFunction(LogicFunction function, bool isTest = false)
        {
            if(function is LogicSplitter)
            {
                List<LogicFunction> outputs = ((LogicSplitter)function).actionOutputs;
                for(int i=0; i< outputs.Count; i++)
                {
                    ((LogicSplitter)function).realizeOutputs[i] = true;
                    RealizeFunction(outputs[i], isTest);
                }
            }
            else if (function is DataSplitter)
            {
                DataSplitter dataSplitter = (DataSplitter)function;
                bool result = dataSplitter.GetResult();
                if (result)
                {
                    dataSplitter.realizeTrue = true;
                    RealizeFunction(dataSplitter.trueOutput, isTest);
                }
                else
                {
                    dataSplitter.realizeFalse = true;
                    RealizeFunction(dataSplitter.falseOutput, isTest);
                }
            }
            else if (function is LogicEffect)
            {
                ((LogicEffect)function).Realize(curOwner, isTest); 
            }
            else if (function is WaitFunction)
            {
                ((WaitFunction)function).CreateWaitAction(delegate{ RealizeFunction(((WaitFunction)function).actionOutput, isTest); });
            }
        }

        private void ClearCheck()
        {
            startRealize = false;
            foreach (LogicFunction function in logicFunc)
            {
                if(function is DataSplitter)
                {
                    DataSplitter dataSplitter = function as DataSplitter;
                    dataSplitter.realizeTrue = false;
                    dataSplitter.realizeFalse = false;
                    dataSplitter.checkNode = false;
                }
                if(function is LogicSplitter)
                {
                    LogicSplitter logicSplitter = function as LogicSplitter;
                    for(int i=0;i< logicSplitter.realizeOutputs.Count; i++)
                    {
                        logicSplitter.realizeOutputs[i] = false;
                    }
                }
            }
            foreach(LogicDataFunction function in dataFunc)
            {
                function.checkNode = false;
            }
            foreach (DataVariable function in dataVariables)
            {
                function.checkNode = false;
            }
            foreach(LogicCondition function in conditions)
            {
                function.checkNode = false;
            }
            foreach (LogicEffect function in effects)
            {
                function.checkNode = false;
            }
        }

        public bool haveFinish()
        {
            bool retVal = false;
            RealizeLogicMap(null, true);
            foreach(LogicEffect effect in effects)
            {
                if (effect.checkNode)
                {
                    retVal = true;
                }
            }
            ClearCheck();

            return retVal;
        }
    }
}