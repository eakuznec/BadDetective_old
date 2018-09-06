using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class LogicSplitterNode : LogicFunctionNode
    {
        private void Awake()
        {
            windowTitle = "LogicSplitter";
        }

        public override void DrawWindow()
        {
            LogicSplitter logicSplitter = logicFunction as LogicSplitter;
            if(logicSplitter.actionOutputs.Count == 0)
            {
                logicSplitter.windowRect.height = 40;
            }
            else
            {
                logicSplitter.windowRect.height = 20 + 20 * logicSplitter.actionOutputs.Count;
            }
            GUIStyle noneActionStyle = new GUIStyle();
            noneActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.gray);
            GUIStyle actionStyle = new GUIStyle();
            actionStyle.normal.background = eUtils.MakeTex(10, 10, Color.blue);
            GUIStyle realizeActionStyle = new GUIStyle();
            realizeActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.magenta);

            GUILayout.BeginHorizontal();

            if (logicSplitter.actionInput == null)
            {
                if (!logicSplitter.startFunction)
                {
                    GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else
                {
                    if (!LogicMapEditor.logicMap.startRealize)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
            }
            else
            {
                if (logicSplitter.actionInput is DataSplitter)
                {
                    DataSplitter input = (DataSplitter)logicSplitter.actionInput;
                    if (input.trueOutput == logicSplitter && !input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.trueOutput == logicSplitter && input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == logicSplitter && !input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == logicSplitter && input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if (logicSplitter.actionInput is LogicSplitter)
                {
                    LogicSplitter input = (LogicSplitter)logicSplitter.actionInput;
                    int index = input.actionOutputs.IndexOf(logicSplitter);
                    if (index == -1)
                    {
                        GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else
                    {
                        if (!input.realizeOutputs[index])
                        {
                            GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                        }
                        else
                        {
                            GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                        }
                    }
                }
                else if (logicSplitter.actionInput is WaitFunction)
                {
                    WaitFunction input = (WaitFunction)logicSplitter.actionInput;
                    if (input.actionOutput == logicSplitter && !input.realize)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.actionOutput == logicSplitter && input.realize)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if (logicSplitter.actionInput is ChooseMethodFunction)
                {
                    ChooseMethodFunction input = (ChooseMethodFunction)logicSplitter.actionInput;
                    if (input.brutalOutput == logicSplitter && !input.realizeBrutal)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.brutalOutput == logicSplitter && input.realizeBrutal)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.carefulOutput == logicSplitter && !input.realizeCareful)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.carefulOutput == logicSplitter && input.realizeCareful)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.diplomatOutput == logicSplitter && !input.realizeDiplomat)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.diplomatOutput == logicSplitter && input.realizeDiplomat)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.scienceOutput == logicSplitter && !input.realizeScience)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.scienceOutput == logicSplitter && input.realizeScience)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            for(int i=0; i< logicSplitter.actionOutputs.Count; i++)
            {
                if (logicSplitter.actionOutputs[i] == null)
                {
                    GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else
                {
                    if (!logicSplitter.realizeOutputs[i])
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                if (i < logicSplitter.actionOutputs.Count - 1)
                {
                    GUILayout.FlexibleSpace();
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        public override void DrawLinks()
        {
            LogicSplitter logicSplitter = logicFunction as LogicSplitter;
            for(int j=0; j< logicSplitter.actionOutputs.Count; j++)
            {
                LogicFunction output = logicSplitter.actionOutputs[j];
                Vector2 startPos;
                Vector2 endPos;
                Color activeColor = Color.blue;
                Color backColor = Color.black;
                startPos = new Vector2(logicSplitter.windowRect.xMax, logicSplitter.windowRect.y + 10 + (j+1)*20);
                if(output != null)
                {
                    BaseLogicNode outputNode = null;
                    foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                    {
                        if (node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction == output)
                        {
                            outputNode = node;
                        }
                    }
                    endPos = new Vector2(((LogicFunctionNode)outputNode).logicFunction.windowRect.xMin, ((LogicFunctionNode)outputNode).logicFunction.windowRect.y + 25);
                    if (logicSplitter.realizeOutputs[j])
                    {
                        activeColor = Color.magenta;
                    }
                }
                else
                {
                    endPos = LogicMapEditor.editor.mousePos;
                    activeColor = Color.yellow;
                }
                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                backColor = new Color(backColor.r, backColor.g, backColor.b, 0.1f);
                int width = 2;

                for (int i = 0; i < 3; i++)
                {
                    Handles.DrawBezier(startPos, endPos, startTan, endTan, backColor, null, (i + 1) * 5);
                }
                Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
            }
        }

        public void SetActionOutputLink()
        {
            selectActionLink = true;
            LogicMapEditor.editor.selectFunctionNode = this;
            ((LogicSplitter)logicFunction).actionOutputs.Add(null);
            ((LogicSplitter)logicFunction).realizeOutputs.Add(false);
        }

        public void SetActionOutputLink(int index)
        {
            LogicSplitter logicSplitter = logicFunction as LogicSplitter;
            if (index>=0 && logicSplitter.actionOutputs.Count > index)
            {
                selectActionLink = true;
                LogicMapEditor.editor.selectFunctionNode = this;
                logicSplitter.actionOutputs[index].actionInput = null;
                logicSplitter.actionOutputs[index] = null;
            }
        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            LogicSplitter function = (LogicSplitter)logicFunction;
            if (function.actionInput != null)
            {
                function.actionInput.RemoveActionOutput(function);
            }
            for(int i=0; i< function.actionOutputs.Count; i++)
            {
                function.actionOutputs[i].RemoveActionInput(function);
            }
            LogicMapEditor.logicMap.logicFunc.Remove(function);
            DestroyImmediate(function.gameObject);
        }
    }
}
