using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class DataSplitterNode : LogicFunctionNode, iBoolDataNode
    {
        private void Awake()
        {
            windowTitle = "DataSplitter";
        }

        public override void DrawWindow()
        {
            DataSplitter dataSplitter = logicFunction as DataSplitter;
            Rect rect = dataSplitter.GetWindowRect();
            dataSplitter.SetWindowRect(new Rect(rect.x, rect.y, rect.width, 60));

            GUIStyle noneActionStyle = new GUIStyle();
            noneActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.gray);
            GUIStyle actionStyle = new GUIStyle();
            actionStyle.normal.background = eUtils.MakeTex(10, 10, Color.blue);
            GUIStyle realizeActionStyle = new GUIStyle();
            realizeActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.magenta);
            GUIStyle checkActionStyle = new GUIStyle();
            checkActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.magenta);
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
            if (dataSplitter.actionInputs.Count == 0)
            {
                if (!dataSplitter.startFunction)
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
                bool realizeFlag = false;
                foreach (LogicFunction actionInput in dataSplitter.actionInputs)
                {
                    if (actionInput is DataSplitter)
                    {
                        DataSplitter input = (DataSplitter)actionInput;
                        if (input.trueOutput == dataSplitter && input.realizeTrue)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.falseOutput == dataSplitter && input.realizeFalse)
                        {
                            realizeFlag = true;
                            break;
                        }
                    }
                    else if (actionInput is LogicSplitter)
                    {
                        LogicSplitter input = (LogicSplitter)actionInput;
                        int index = input.actionOutputs.IndexOf(dataSplitter);
                        if (input.realizeOutputs[index])
                        {
                            realizeFlag = true;
                            break;
                        }
                    }
                    else if (actionInput is WaitFunction)
                    {
                        WaitFunction input = (WaitFunction)actionInput;
                        if (input.actionOutput == dataSplitter && input.realize)
                        {
                            realizeFlag = true;
                            break;
                        }
                    }
                    else if (actionInput is ChallengeFunction)
                    {
                        ChallengeFunction input = (ChallengeFunction)actionInput;
                        if (input.trueOutput == dataSplitter && input.realizeTrue)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.falseOutput == dataSplitter && input.realizeFalse)
                        {
                            realizeFlag = true;
                            break;
                        }
                    }
                    else if (actionInput is ChooseMethodFunction)
                    {
                        ChooseMethodFunction input = (ChooseMethodFunction)actionInput;
                        if (input.brutalOutput == dataSplitter && input.realizeBrutal)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.carefulOutput == dataSplitter && input.realizeCareful)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.diplomatOutput == dataSplitter && input.realizeDiplomat)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.scienceOutput == dataSplitter && input.realizeScience)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else
                        {
                            for (int i = 0; i < input.dialogOutputs.Count; i++)
                            {
                                if (input.dialogOutputs[i] == logicFunction && input.realizeDialogOutput[i])
                                {
                                    realizeFlag = true;
                                    break;
                                }
                            }
                        }
                    }
                    else if (actionInput is ChooseTemperFunction)
                    {
                        ChooseTemperFunction input = (ChooseTemperFunction)actionInput;
                        if (input.rudeOutput == logicFunction && input.realizeRude)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.prudentOutput == logicFunction && input.realizePrudent)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.cruelOutput == logicFunction && input.realizeCruel)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.principledOutput == logicFunction && input.realizePrincipled)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else
                        {
                            for (int i = 0; i < input.dialogOutputs.Count; i++)
                            {
                                if (input.dialogOutputs[i] == logicFunction && input.realizeDialogOutput[i])
                                {
                                    realizeFlag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (realizeFlag)
                {
                    GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else
                {
                    GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }
            GUILayout.FlexibleSpace();

            if(dataSplitter.dataInput == null)
            {
                GUILayout.Box(GUIContent.none, noneDataStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else
            {
                if (dataSplitter.dataInput is LogicCondition)
                {
                    LogicCondition dataInput = dataSplitter.dataInput as LogicCondition;
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
                else if (dataSplitter.dataInput is DataVariable)
                {
                    DataVariable dataInput = dataSplitter.dataInput as DataVariable;
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
                else if (dataSplitter.dataInput is LogicDataFunction)
                {
                    LogicDataFunction dataInput = dataSplitter.dataInput as LogicDataFunction;
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

            }

            GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("T");
                        GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (dataSplitter.trueOutput == null)
                        {
                            GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                        }
                        else if (!dataSplitter.realizeTrue)
                        {
                            GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                        }
            else
            {
                GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            GUILayout.FlexibleSpace();
                        GUILayout.EndVertical();
                    GUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("F");
                        GUILayout.BeginVertical();
                        GUILayout.FlexibleSpace();
                        if (dataSplitter.falseOutput == null)
                        {
                            GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                        }
                        else if (!dataSplitter.realizeFalse)
                        {
                            GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                        }
            else
            {
                GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            GUILayout.FlexibleSpace();
                        GUILayout.EndVertical();
                    GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        public override void DrawLinks()
        {
            DataSplitter dataSplitter = logicFunction as DataSplitter;
            Vector2 startPos;
            Vector2 endPos;
            if (dataSplitter.trueOutput != null)
            {
                startPos = new Vector2(dataSplitter.GetWindowRect().xMax, dataSplitter.GetWindowRect().y + 30);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction == dataSplitter.trueOutput)
                    {
                        outputNode = node;
                    }
                }
                endPos = new Vector2(((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().xMin, ((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().y + 25);

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.blue;
                Color backColor = Color.black;
                if (dataSplitter.realizeTrue)
                {
                    activeColor = Color.magenta;
                }
                backColor = new Color(backColor.r, backColor.g, backColor.b, 0.1f);
                int width = 2;

                for (int i = 0; i < 3; i++)
                {
                    Handles.DrawBezier(startPos, endPos, startTan, endTan, backColor, null, (i + 1) * 5);
                }
                Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
            }
            if (dataSplitter.falseOutput != null)
            {
                startPos = new Vector2(dataSplitter.GetWindowRect().xMax, dataSplitter.GetWindowRect().y + 45);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction == dataSplitter.falseOutput)
                    {
                        outputNode = node;
                    }
                }
                endPos = new Vector2(((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().xMin, ((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().y + 25);

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.blue;
                Color backColor = Color.black;
                if (dataSplitter.realizeFalse)
                {
                    activeColor = Color.magenta;
                }
                backColor = new Color(backColor.r, backColor.g, backColor.b, 0.1f);
                int width = 2;

                for (int i = 0; i < 3; i++)
                {
                    Handles.DrawBezier(startPos, endPos, startTan, endTan, backColor, null, (i + 1) * 5);
                }
                Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
            }
            if (selectActionLink  && dataSplitter.trueFlag)
            {
                Event e = Event.current;
                startPos = new Vector2(dataSplitter.GetWindowRect().xMax, dataSplitter.GetWindowRect().y + 30);
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
            else if (selectActionLink && !dataSplitter.trueFlag)
            {
                Event e = Event.current;
                startPos = new Vector2(dataSplitter.GetWindowRect().xMax, dataSplitter.GetWindowRect().y + 45);
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
            return (DataSplitter)logicFunction;
        }

        public bool GetSelectDataLink()
        {
            return selectActionLink;
        }

        public void SetSelectDataLink(bool link)
        {
            selectActionLink = link;
        }

        public void SetActionOutputLink()
        {
            DataSplitter dataSplitter = logicFunction as DataSplitter;
            dataSplitter.SetOutputLink(null);
            selectActionLink = true;
            LogicMapEditor.editor.selectFunctionNode = this;
        }

        public void SetDataOutputLink()
        {
        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            DataSplitter dataSplitter = (DataSplitter)logicFunction;
            if (dataSplitter.trueOutput != null)
            {
                dataSplitter.trueOutput.RemoveActionInput(dataSplitter);
            }
            if (dataSplitter.falseOutput != null)
            {
                dataSplitter.falseOutput.RemoveActionInput(dataSplitter);
            }
            foreach (LogicFunction actionInput in dataSplitter.actionInputs)
            {
                actionInput.RemoveActionOutput(dataSplitter);
            }
            if (dataSplitter.dataInput != null)
            {
                if (dataSplitter.dataInput is LogicCondition)
                {
                    ((LogicCondition)dataSplitter.dataInput).RemoveDataOutput(dataSplitter);
                }
                else if (dataSplitter.dataInput is DataVariable)
                {
                    ((DataVariable)dataSplitter.dataInput).RemoveDataOutput(dataSplitter);
                }
                else if (dataSplitter.dataInput is LogicDataFunction)
                {
                    ((LogicDataFunction)dataSplitter.dataInput).RemoveDataOutput(dataSplitter);
                }
            }
            LogicMapEditor.logicMap.logicFunc.Remove(dataSplitter);
            DestroyImmediate(dataSplitter.gameObject);
        }
    }
}