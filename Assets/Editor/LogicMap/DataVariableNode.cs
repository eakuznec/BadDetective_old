using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class DataVariableNode : BaseLogicNode, iBoolDataNode
    {
        public BaseFunction dataVariable;
        public bool selectDataLink;

        private void Awake()
        {
            windowTitle = "Variable";
        }

        public override void DrawWindow()
        {
            DataVariable variable = (DataVariable)dataVariable;
            GUIStyle noneDataStyle = new GUIStyle();
            noneDataStyle.normal.background = eUtils.MakeTex(10, 10, Color.black);
            GUIStyle dataStyle = new GUIStyle();
            dataStyle.normal.background = eUtils.MakeTex(10, 10, Color.white);
            GUIStyle trueDataStyle = new GUIStyle();
            trueDataStyle.normal.background = eUtils.MakeTex(10, 10, Color.green);
            GUIStyle falseDataStyle = new GUIStyle();
            falseDataStyle.normal.background = eUtils.MakeTex(10, 10, Color.red);

            GUILayout.BeginHorizontal();
            if(variable.dataInput == null)
            {
                GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if(variable.dataInput is LogicCondition)
            {
                LogicCondition dataInput = variable.dataInput as LogicCondition;
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
            else if (variable.dataInput is DataVariable)
            {
                DataVariable dataInput = variable.dataInput as DataVariable;
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
            else if (variable.dataInput is LogicDataFunction)
            {
                LogicDataFunction dataInput = variable.dataInput as LogicDataFunction;
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

            GUILayout.FlexibleSpace();
            if (variable.dataOutput == null)
            {
                GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (!variable.checkNode)
            {
                GUILayout.Box(GUIContent.none, dataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (variable.checkNode && variable.result)
            {
                GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (variable.checkNode && !variable.result)
            {
                GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if(variable.variable == null)
            {
                string name = EditorGUILayout.DelayedTextField("");
                if(name != "")
                {
                    LogicMapEditor.logicMap.CheckBoolVariable(variable, name, false);
                }
            }
            else
            {
                string name = EditorGUILayout.DelayedTextField(variable.variable.variableName);
                bool flag = EditorGUILayout.Toggle(variable.variable.value);
                if(name != variable.variable.variableName || flag != variable.variable.value)
                {
                    LogicMapEditor.logicMap.CheckBoolVariable(variable, name, flag);
                }
            }
            GUILayout.EndHorizontal();
        }

        public override void DrawLinks()
        {
            DataVariable dataFunction = dataVariable as DataVariable;
            Vector2 startPos;
            Vector2 endPos = Vector2.zero;
            if (dataFunction.dataOutput != null)
            {
                startPos = new Vector2(dataFunction.GetWindowRect().xMax, dataFunction.GetWindowRect().y + 25);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicDataFunctionNode && ((LogicDataFunctionNode)node).dataFunction == dataFunction.dataOutput)
                    {
                        outputNode = node;
                    }
                    else if (node is DataSplitterNode && ((DataSplitterNode)node).logicFunction == dataFunction.dataOutput)
                    {
                        outputNode = node;
                    }
                    else if (node is DataVariableNode && ((DataVariableNode)node).dataVariable == dataFunction.dataOutput)
                    {
                        outputNode = node;
                    }
                }
                if (outputNode is LogicNotNode)
                {
                    endPos = new Vector2(((LogicNotNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicNotNode)outputNode).dataFunction.GetWindowRect().y + 25);
                }
                else if (outputNode is DataSplitterNode)
                {
                    endPos = new Vector2(((DataSplitterNode)outputNode).logicFunction.GetWindowRect().xMin, ((DataSplitterNode)outputNode).logicFunction.GetWindowRect().y + 40);
                }
                else if (outputNode is LogicAndNode)
                {
                    if (((LogicAND)((LogicAndNode)outputNode).dataFunction).dataInputOne == dataVariable)
                    {
                        endPos = new Vector2(((LogicAndNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicAndNode)outputNode).dataFunction.GetWindowRect().y + 25);
                    }
                    else if (((LogicAND)((LogicAndNode)outputNode).dataFunction).dataInputTwo == dataVariable)
                    {
                        endPos = new Vector2(((LogicAndNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicAndNode)outputNode).dataFunction.GetWindowRect().y + 45);
                    }
                }
                else if (outputNode is LogicOrNode)
                {
                    if (((LogicOR)((LogicOrNode)outputNode).dataFunction).dataInputOne == dataVariable)
                    {
                        endPos = new Vector2(((LogicOrNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicOrNode)outputNode).dataFunction.GetWindowRect().y + 25);
                    }
                    else if (((LogicOR)((LogicOrNode)outputNode).dataFunction).dataInputTwo == dataVariable)
                    {
                        endPos = new Vector2(((LogicOrNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicOrNode)outputNode).dataFunction.GetWindowRect().y + 45);
                    }
                }
                else if (outputNode is EffectNode)
                {
                    endPos = new Vector2(((EffectNode)outputNode).logicFunction.GetWindowRect().xMin, ((EffectNode)outputNode).logicFunction.GetWindowRect().y + 25);
                }
                else if (outputNode is DataVariableNode)
                {
                    endPos = new Vector2(((DataVariable)((DataVariableNode)outputNode).dataVariable).GetWindowRect().xMin, ((DataVariable)((DataVariableNode)outputNode).dataVariable).GetWindowRect().y + 25);
                }

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.black;
                Color backColor = Color.black;
                if (dataFunction.checkNode && dataFunction.result)
                {
                    activeColor = Color.green;
                }
                else if (dataFunction.checkNode && !dataFunction.result)
                {
                    activeColor = Color.red;
                }
                backColor = new Color(backColor.r, backColor.g, backColor.b, 0.1f);
                
                int width = 2;

                for (int i = 0; i < 3; i++)
                {
                    Handles.DrawBezier(startPos, endPos, startTan, endTan, backColor, null, (i + 1) * 5);
                }
                Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
            }
            else if (selectDataLink)
            {
                Event e = Event.current;
                startPos = new Vector2(dataFunction.GetWindowRect().xMax, dataFunction.GetWindowRect().y + 25);
                endPos = e.mousePosition;
                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.yellow;
                Color backColor = Color.black;
                backColor = new Color(backColor.r, backColor.g, backColor.b, 0.1f);
                int width = 2;

                for (int i = 0; i < 3; i++)
                {
                    Handles.DrawBezier(startPos, endPos, startTan, endTan, backColor, null, (i + 1) * 5);
                }
                Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
            }
        }

        public BaseFunction GetDataFunction()
        {
            return dataVariable;
        }

        public bool GetSelectDataLink()
        {
            return selectDataLink;
        }

        public void SetSelectDataLink(bool link)
        {
            selectDataLink = link;
        }

        public void SetDataOutputLink()
        {
            if (((DataVariable)dataVariable).dataOutput != null)
            {
                if (((DataVariable)dataVariable).dataOutput is LogicDataFunction)
                {
                    ((LogicDataFunction)((DataVariable)dataVariable).dataOutput).RemoveDataInput(dataVariable);
                }
                else if (((DataVariable)dataVariable).dataOutput is DataVariable)
                {
                    ((DataVariable)((DataVariable)dataVariable).dataOutput).RemoveDataInput(dataVariable);
                }
                else if (((DataVariable)dataVariable).dataOutput is DataSplitter)
                {
                    ((DataSplitter)((DataVariable)dataVariable).dataOutput).RemoveDataInput(dataVariable);
                }
            }
            ((DataVariable)dataVariable).SetDataOutputLink(null);
            selectDataLink = true;
            LogicMapEditor.editor.selectDataNode = this;
        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            DataVariable function = (DataVariable)dataVariable;
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
            LogicMapEditor.logicMap.dataVariables.Remove(function);
            DestroyImmediate(function.gameObject);
        }
    }
}