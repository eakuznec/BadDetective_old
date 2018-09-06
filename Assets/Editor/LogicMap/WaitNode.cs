using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class WaitNode : LogicFunctionNode
    {
        private void Awake()
        {
            windowTitle = "Wait";
        }

        public override void DrawLinks()
        {
            WaitFunction waitFunction = logicFunction as WaitFunction;
            Vector2 startPos;
            Vector2 endPos;
            if (waitFunction.actionOutput != null)
            {
                startPos = new Vector2(waitFunction.GetWindowRect().xMax, waitFunction.GetWindowRect().y + 30);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction == waitFunction.actionOutput)
                    {
                        outputNode = node;
                    }
                }
                endPos = new Vector2(((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().xMin, ((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().y + 25);

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.blue;
                Color backColor = Color.black;
                if (waitFunction.realize)
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
        }

        public override void DrawWindow()
        {
            WaitFunction waitFunction = logicFunction as WaitFunction;
            SerializedObject soWait = new SerializedObject(waitFunction);

            EditorGUILayout.BeginHorizontal();
            GUIStyle noneActionStyle = new GUIStyle();
            noneActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.gray);
            GUIStyle actionStyle = new GUIStyle();
            actionStyle.normal.background = eUtils.MakeTex(10, 10, Color.blue);
            GUIStyle realizeActionStyle = new GUIStyle();
            realizeActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.magenta);
            if (waitFunction.actionInput == null)
            {
                if (!waitFunction.startFunction)
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
                if (waitFunction.actionInput is DataSplitter)
                {
                    DataSplitter input = (DataSplitter)waitFunction.actionInput;
                    if (input.trueOutput == waitFunction && !input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.trueOutput == waitFunction && input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == waitFunction && !input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == waitFunction && input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if (waitFunction.actionInput is LogicSplitter)
                {
                    LogicSplitter input = (LogicSplitter)waitFunction.actionInput;
                    int index = input.actionOutputs.IndexOf(waitFunction);
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
                else if (waitFunction.actionInput is WaitFunction)
                {
                    WaitFunction input = (WaitFunction)waitFunction.actionInput;
                    if (input.actionOutput == waitFunction && !input.realize)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.actionOutput == waitFunction && input.realize)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if (waitFunction.actionInput is ChallengeFunction)
                {
                    ChallengeFunction input = (ChallengeFunction)waitFunction.actionInput;
                    if (input.trueOutput == waitFunction && !input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.trueOutput == waitFunction && input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == waitFunction && !input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == waitFunction && input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if (waitFunction.actionInput is ChooseMethodFunction)
                {
                    ChooseMethodFunction input = (ChooseMethodFunction)waitFunction.actionInput;
                    if (input.brutalOutput == waitFunction && !input.realizeBrutal)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.brutalOutput == waitFunction && input.realizeBrutal)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.carefulOutput == waitFunction && !input.realizeCareful)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.carefulOutput == waitFunction && input.realizeCareful)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.diplomatOutput == waitFunction && !input.realizeDiplomat)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.diplomatOutput == waitFunction && input.realizeDiplomat)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.scienceOutput == waitFunction && !input.realizeScience)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.scienceOutput == waitFunction && input.realizeScience)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(soWait.FindProperty("waitType"), GUIContent.none);
            float min = EditorGUILayout.FloatField("Minutes", waitFunction.waitTime.minutes);
            int hour = EditorGUILayout.IntField("Hours", waitFunction.waitTime.hours);
            int day = 0;
            int week = 0;
            int month = 0;
            if (waitFunction.waitType == WaitType.RELATION || waitFunction.waitType == WaitType.ABSOLUTE)
            {
                day = EditorGUILayout.IntField("Days", waitFunction.waitTime.days);
                week = EditorGUILayout.IntField("Weeks", waitFunction.waitTime.weeks);
                month = EditorGUILayout.IntField("Months", waitFunction.waitTime.months);
            }
            GameTime gameTime = new GameTime 
{
                minutes = min,
                hours = hour,
                days = day,
                weeks = week,
                months = month
            };
            waitFunction.waitTime = GameTime.Convert(gameTime);
            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            if (waitFunction.actionOutput == null)
            {
                if (!waitFunction.startFunction)
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
                if (waitFunction.actionOutput is DataSplitter)
                {
                    DataSplitter output = (DataSplitter)waitFunction.actionOutput;
                    if (output.trueOutput == waitFunction && !output.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (output.trueOutput == waitFunction && output.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (output.falseOutput == waitFunction && !output.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (output.falseOutput == waitFunction && output.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if (waitFunction.actionOutput is LogicSplitter)
                {
                    LogicSplitter output = (LogicSplitter)waitFunction.actionOutput;
                    int index = output.actionOutputs.IndexOf(waitFunction);
                    if (output.actionInput != waitFunction)
                    {
                        GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else
                    {
                        if (!waitFunction.realize)
                        {
                            GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                        }
                        else
                        {
                            GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                        }
                    }
                }
                else if (waitFunction.actionOutput is LogicEffect)
                {
                    LogicEffect output = (LogicEffect)waitFunction.actionOutput;
                    if (!waitFunction.realize)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            soWait.ApplyModifiedProperties();
        }

        public void SetActionOutputLink()
        {
            WaitFunction waitFunction = logicFunction as WaitFunction;
            waitFunction.SetOutputLink(null);
            selectActionLink = true;
            LogicMapEditor.editor.selectFunctionNode = this;

        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            WaitFunction function = (WaitFunction)logicFunction;
            if (function.actionInput != null)
            {
                function.actionInput.RemoveActionOutput(function);
            }
            if (function.actionOutput != null)
            {
                function.actionOutput.RemoveActionInput(function);
            }
            LogicMapEditor.logicMap.logicFunc.Remove(function);
            DestroyImmediate(function.gameObject);
        }
    }
}