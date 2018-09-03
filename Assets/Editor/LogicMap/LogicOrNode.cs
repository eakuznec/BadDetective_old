using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class LogicOrNode : LogicDataFunctionNode
    {
        private void Awake()
        {
            windowTitle = "OR";
        }

        public override void DrawWindow()
        {
            LogicOR logicOR = dataFunction as LogicOR;
            Rect rect = logicOR.GetWindowRect();
            logicOR.SetWindowRect(new Rect(rect.x, rect.y, rect.width, 60));
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
            if (logicOR.dataInputOne == null)
            {
                GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (logicOR.dataInputOne is LogicCondition)
            {
                LogicCondition dataInput = logicOR.dataInputOne as LogicCondition;
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
            else if (logicOR.dataInputOne is DataVariable)
            {
                DataVariable dataInput = logicOR.dataInputOne as DataVariable;
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
            else if (logicOR.dataInputOne is LogicDataFunction)
            {
                LogicDataFunction dataInput = logicOR.dataInputOne as LogicDataFunction;
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
            if (logicOR.dataInputTwo == null)
            {
                GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (logicOR.dataInputTwo is LogicCondition)
            {
                LogicCondition dataInput = logicOR.dataInputTwo as LogicCondition;
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
            else if (logicOR.dataInputTwo is DataVariable)
            {
                DataVariable dataInput = logicOR.dataInputTwo as DataVariable;
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
            else if (logicOR.dataInputTwo is LogicDataFunction)
            {
                LogicDataFunction dataInput = logicOR.dataInputTwo as LogicDataFunction;
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
            if (logicOR.dataOutput == null)
            {
                GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (!logicOR.checkNode)
            {
                GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (logicOR.checkNode && logicOR.result)
            {
                GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (logicOR.checkNode && !logicOR.result)
            {
                GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        public override BaseFunction GetDataFunction()
        {
            return (LogicOR)dataFunction;
        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            LogicOR function = (LogicOR)dataFunction;
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
            if (function.dataInputOne != null)
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
