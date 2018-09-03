using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class LogicNotNode : LogicDataFunctionNode
    {
        private void Awake()
        {
            windowTitle = "NOT";
        }

        public override void DrawWindow()
        {
            LogicNOT logicNot = dataFunction as LogicNOT;
            logicNot.windowRect.height = 40;

            GUIStyle noneDataStyle = new GUIStyle();
            noneDataStyle.normal.background = eUtils.MakeTex(10, 10, Color.black);
            GUIStyle dataStyle = new GUIStyle();
            dataStyle.normal.background = eUtils.MakeTex(10, 10, Color.white);
            GUIStyle trueDataStyle = new GUIStyle();
            trueDataStyle.normal.background = eUtils.MakeTex(10, 10, Color.green);
            GUIStyle falseDataStyle = new GUIStyle();
            falseDataStyle.normal.background = eUtils.MakeTex(10, 10, Color.red);


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            if(logicNot.dataInput == null)
            {
                GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else
            {
                if (logicNot.dataInput is LogicCondition)
                {
                    LogicCondition dataInput = logicNot.dataInput as LogicCondition;
                    if (!dataInput.GetChackDataNode())
                    {
                        GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (dataInput.GetResult())
                    {
                        GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (!dataInput.GetResult())
                    {
                        GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if (logicNot.dataInput is DataVariable)
                {
                    DataVariable dataInput = logicNot.dataInput as DataVariable;
                    if (!dataInput.GetChackDataNode())
                    {
                        GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (dataInput.GetResult())
                    {
                        GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (!dataInput.GetResult())
                    {
                        GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if (logicNot.dataInput is LogicDataFunction)
                {
                    LogicDataFunction dataInput = logicNot.dataInput as LogicDataFunction;
                    if (!dataInput.GetChackDataNode())
                    {
                        GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (dataInput.GetResult())
                    {
                        GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (!dataInput.GetResult())
                    {
                        GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
            }

            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();
            if (logicNot.dataOutput == null)
            {
                GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (!logicNot.checkNode)
            {
                GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (logicNot.result)
            {
                GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (!logicNot.result)
            {
                GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

        }

        public override BaseFunction GetDataFunction()
        {
            return (LogicNOT)dataFunction;
        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            LogicNOT function = (LogicNOT)dataFunction;
            if (function.dataInput != null)
            {
                if (function.dataInput is LogicCondition)
                {
                    ((LogicCondition)function.dataInput).RemoveDataOutput(function);
                }
                else if (function.dataInput is DataVariable)
                {
                    ((DataVariable)function.dataInput).RemoveDataOutput(function);
                }
                else if (function.dataInput is LogicDataFunction)
                {
                    ((LogicDataFunction)function.dataInput).RemoveDataOutput(function);
                }
            }
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
            LogicMapEditor.logicMap.dataFunc.Remove(function);
            DestroyImmediate(function.gameObject);
        }
    }
}