using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class LogicAndNode : LogicDataFunctionNode
    {
        private void Awake()
        {
            windowTitle = "AND";
        }

        public override void DrawWindow()
        {
            LogicAND logicAnd = dataFunction as LogicAND;
            Rect rect = logicAnd.GetWindowRect();
                logicAnd.SetWindowRect(new Rect(rect.x, rect.y, rect.width, 60));
            GUIStyle noneDataStyle = new GUIStyle();
            noneDataStyle.normal.background = eUtils.MakeTex(10, 10, Color.black);
            GUIStyle dataStyle = new GUIStyle();
            dataStyle.normal.background = eUtils.MakeTex(10, 10, Color.white);
            GUIStyle trueDataStyle = new GUIStyle();
            trueDataStyle.normal.background = eUtils.MakeTex(10, 10, Color.green);
            GUIStyle falseDataStyle = new GUIStyle();
            falseDataStyle.normal.background = eUtils.MakeTex(10, 10, Color.red);

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            if(logicAnd.dataInputOne == null)
            {
                GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (logicAnd.dataInputOne is LogicCondition)
            {
                LogicCondition dataInput = logicAnd.dataInputOne as LogicCondition;
                if (!dataInput.GetChackDataNode())
                {
                    GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && !dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }
            else if (logicAnd.dataInputOne is DataVariable)
            {
                DataVariable dataInput = logicAnd.dataInputOne as DataVariable;
                if (dataInput == null)
                {
                    GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (!dataInput.GetChackDataNode())
                {
                    GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && !dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }
            else if (logicAnd.dataInputOne is LogicDataFunction)
            {
                LogicDataFunction dataInput = logicAnd.dataInputOne as LogicDataFunction;
                if (dataInput == null)
                {
                    GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (!dataInput.GetChackDataNode())
                {
                    GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && !dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }
            GUILayout.FlexibleSpace();
            if (logicAnd.dataInputTwo == null)
            {
                GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (logicAnd.dataInputTwo is LogicCondition)
            {
                LogicCondition dataInput = logicAnd.dataInputTwo as LogicCondition;
                if (!dataInput.GetChackDataNode())
                {
                    GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && !dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }
            else if (logicAnd.dataInputTwo is DataVariable)
            {
                DataVariable dataInput = logicAnd.dataInputTwo as DataVariable;
                if (dataInput == null)
                {
                    GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (!dataInput.GetChackDataNode())
                {
                    GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && !dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }
            else if (logicAnd.dataInputTwo is LogicDataFunction)
            {
                LogicDataFunction dataInput = logicAnd.dataInputTwo as LogicDataFunction;
                if (dataInput == null)
                {
                    GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (!dataInput.GetChackDataNode())
                {
                    GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else if (dataInput.GetChackDataNode() && !dataInput.GetResult())
                {
                    GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }

            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            if (logicAnd.dataOutput == null)
            {
                GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (!logicAnd.checkNode)
            {
                GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (logicAnd.checkNode && logicAnd.result)
            {
                GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (logicAnd.checkNode && !logicAnd.result)
            {
                GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        public override BaseFunction GetDataFunction()
        {
            return (LogicAND)dataFunction;
        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            LogicAND function = (LogicAND)dataFunction;
            if (function.dataOutput != null)
            {
                if (function.dataOutput is DataSplitter)
                {
                    ((DataSplitter)function.dataOutput).RemoveDataInput(function);
                }
                else if (function.dataOutput is DataVariable)
                {
                    ((DataVariable)function.dataOutput).RemoveDataInput(function);
                }
                else if (function.dataOutput is LogicDataFunction)
                {
                    ((LogicDataFunction)function.dataOutput).RemoveDataInput(function);
                }
            }
            if(function.dataInputOne != null)
            {
                if (function.dataInputOne is LogicCondition)
                {
                    ((LogicCondition)function.dataInputOne).RemoveDataOutput(function);
                }
                else if (function.dataInputOne is DataVariable)
                {
                    ((DataVariable)function.dataInputOne).RemoveDataOutput(function);
                }
                else if (function.dataInputOne is LogicDataFunction)
                {
                    ((LogicDataFunction)function.dataInputOne).RemoveDataOutput(function);
                }
            }
            if (function.dataInputTwo != null)
            {
                if (function.dataInputTwo is LogicCondition)
                {
                    ((LogicCondition)function.dataInputTwo).RemoveDataOutput(function);
                }
                else if (function.dataInputTwo is DataVariable)
                {
                    ((DataVariable)function.dataInputTwo).RemoveDataOutput(function);
                }
                else if (function.dataInputTwo is LogicDataFunction)
                {
                    ((LogicDataFunction)function.dataInputTwo).RemoveDataOutput(function);
                }
            }
            LogicMapEditor.logicMap.dataFunc.Remove(function);
            DestroyImmediate(function.gameObject);
        }
    }
}