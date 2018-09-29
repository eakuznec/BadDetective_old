using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class ConditionNode : BaseLogicNode, iBoolDataNode
    {
        private bool selectDataLink;

        public LogicCondition logicCondition;

        private void Awake()
        {
            windowTitle = "Condition";
        }

        public override void DrawWindow()
        {
            Condition condition = logicCondition.condition;
            SerializedObject soCondition = new SerializedObject(condition);
            EditorGUILayout.BeginHorizontal();
            eUtils.DrawConditionSelector(condition);

            GUILayout.FlexibleSpace();

            GUIStyle noneActionStyle = new GUIStyle();
            noneActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.black);
            GUIStyle actionStyle = new GUIStyle();
            actionStyle.normal.background = eUtils.MakeTex(10, 10, Color.white);
            GUIStyle trueDataStyle = new GUIStyle();
            trueDataStyle.normal.background = eUtils.MakeTex(10, 10, Color.green);
            GUIStyle falseDataStyle = new GUIStyle();
            falseDataStyle.normal.background = eUtils.MakeTex(10, 10, Color.red);

            if (logicCondition.dataOutput == null)
            {
                GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (!logicCondition.checkNode)
            {
                GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (logicCondition.result)
            {
                GUILayout.Box(GUIContent.none, trueDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else if (!logicCondition.result)
            {
                GUILayout.Box(GUIContent.none, falseDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Dublicate"))
            {
                ConditionNode newNode = LogicMapEditor.editor.CreateConditionNode(logicCondition.windowRect.position + new Vector2(20, 20));
                newNode.logicCondition.condition.copyContentFrom(condition);
            }
        }

        public override void DrawLinks()
        {
            Vector2 startPos;
            Vector2 endPos = Vector2.zero;
            if (logicCondition.dataOutput != null)
            {
                startPos = new Vector2(logicCondition.GetWindowRect().xMax, logicCondition.GetWindowRect().y + 25);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicDataFunctionNode && ((LogicDataFunctionNode)node).dataFunction == logicCondition.dataOutput)
                    {
                        outputNode = node;
                    }
                    else if (node is DataSplitterNode && ((DataSplitterNode)node).logicFunction == logicCondition.dataOutput)
                    {
                        outputNode = node;
                    }
                    else if (node is DataVariableNode && ((DataVariableNode)node).dataVariable == logicCondition.dataOutput)
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
                    if(((LogicAND)((LogicAndNode)outputNode).dataFunction).dataInputOne == logicCondition)
                    {
                        endPos = new Vector2(((LogicAndNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicAndNode)outputNode).dataFunction.GetWindowRect().y + 25);
                    }
                    else if (((LogicAND)((LogicAndNode)outputNode).dataFunction).dataInputTwo == logicCondition)
                    {
                        endPos = new Vector2(((LogicAndNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicAndNode)outputNode).dataFunction.GetWindowRect().y + 45);
                    }
                }
                else if (outputNode is LogicOrNode)
                {
                    if (((LogicOR)((LogicOrNode)outputNode).dataFunction).dataInputOne == logicCondition)
                    {
                        endPos = new Vector2(((LogicOrNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicOrNode)outputNode).dataFunction.GetWindowRect().y + 25);
                    }
                    else if (((LogicOR)((LogicOrNode)outputNode).dataFunction).dataInputTwo == logicCondition)
                    {
                        endPos = new Vector2(((LogicOrNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicOrNode)outputNode).dataFunction.GetWindowRect().y + 45);
                    }
                }
                else if (outputNode is DataVariableNode)
                {
                    endPos = new Vector2(((DataVariable)((DataVariableNode)outputNode).dataVariable).GetWindowRect().xMin, ((DataVariable)((DataVariableNode)outputNode).dataVariable).GetWindowRect().y + 25);
                }

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.black;
                Color backColor = Color.black;
                if (logicCondition.checkNode && logicCondition.result)
                {
                    activeColor = Color.green;
                }
                else if (logicCondition.checkNode && !logicCondition.result)
                {
                    activeColor = Color.red;
                }
                backColor = new Color(backColor.r, backColor.g, backColor.b, 0.1f);
                int width = 2;

                Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
            }
            else if (selectDataLink)
            {
                Event e = Event.current;
                startPos = new Vector2(logicCondition.GetWindowRect().xMax, logicCondition.GetWindowRect().y + 25);
                endPos = e.mousePosition;
                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.yellow;
                Color backColor = Color.black;
                backColor = new Color(backColor.r, backColor.g, backColor.b, 0.1f);
                int width = 2;

                Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
            }
        }

        public BaseFunction GetDataFunction()
        {
            return logicCondition;
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
            if (logicCondition.dataOutput != null)
            {
                if (logicCondition.dataOutput is LogicDataFunction)
                {
                    ((LogicDataFunction)logicCondition.dataOutput).RemoveDataInput(logicCondition);
                }
                else if (logicCondition.dataOutput is DataVariable)
                {
                    ((DataVariable)logicCondition.dataOutput).RemoveDataInput(logicCondition);
                }
                else if (logicCondition.dataOutput is DataSplitter)
                {
                    ((DataSplitter)logicCondition.dataOutput).RemoveDataInput(logicCondition);
                }
            }
            logicCondition.SetDataOutputLink(null);
            selectDataLink = true;
            LogicMapEditor.editor.selectDataNode = this;
        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            if(logicCondition.dataOutput!= null)
            {
                if(logicCondition.dataOutput is DataSplitter)
                {
                    ((DataSplitter)logicCondition.dataOutput).RemoveDataInput(logicCondition);
                }
                else if (logicCondition.dataOutput is DataVariable)
                {
                    ((DataVariable)logicCondition.dataOutput).RemoveDataInput(logicCondition);
                }
                else if (logicCondition.dataOutput is LogicDataFunction)
                {
                    ((LogicDataFunction)logicCondition.dataOutput).RemoveDataInput(logicCondition);
                }

            }
            LogicMapEditor.logicMap.conditions.Remove(logicCondition);
            DestroyImmediate(logicCondition.gameObject);
        }
    }
}