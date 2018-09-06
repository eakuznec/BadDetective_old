using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class ChooseMethodNode : LogicFunctionNode
    {
        private void Awake()
        {
            windowTitle = "Choose Method";
        }

        public override void DrawLinks()
        {
            ChooseMethodFunction function = logicFunction as ChooseMethodFunction;
            Vector2 startPos;
            Vector2 endPos;
            if (function.brutalOutput != null)
            {
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 25);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction == function.brutalOutput)
                    {
                        outputNode = node;
                    }
                }
                endPos = new Vector2(((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().xMin, ((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().y + 25);

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.blue;
                Color backColor = Color.black;
                if (function.realizeBrutal)
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
            if (function.carefulOutput != null)
            {
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 50);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction == function.carefulOutput)
                    {
                        outputNode = node;
                    }
                }
                endPos = new Vector2(((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().xMin, ((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().y + 25);

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.blue;
                Color backColor = Color.black;
                if (function.realizeCareful)
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
            if (function.diplomatOutput != null)
            {
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 75);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction == function.diplomatOutput)
                    {
                        outputNode = node;
                    }
                }
                endPos = new Vector2(((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().xMin, ((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().y + 25);

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.blue;
                Color backColor = Color.black;
                if (function.realizeDiplomat)
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
            if (function.scienceOutput != null)
            {
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 100);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction == function.scienceOutput)
                    {
                        outputNode = node;
                    }
                }
                endPos = new Vector2(((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().xMin, ((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().y + 25);

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.blue;
                Color backColor = Color.black;
                if (function.realizeScience)
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

            if (selectActionLink && function.method == 0)
            {
                Event e = Event.current;
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 25);
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
            else if (selectActionLink && function.method == 1)
            {
                Event e = Event.current;
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 50);
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
            else if (selectActionLink && function.method == 2)
            {
                Event e = Event.current;
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 75);
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
            else if (selectActionLink && function.method == 3)
            {
                Event e = Event.current;
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 100);
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

        public override void DrawWindow()
        {
            ChooseMethodFunction function = logicFunction as ChooseMethodFunction;
            EditorGUILayout.BeginHorizontal();
            GUIStyle noneActionStyle = new GUIStyle();
            noneActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.gray);
            GUIStyle actionStyle = new GUIStyle();
            actionStyle.normal.background = eUtils.MakeTex(10, 10, Color.blue);
            GUIStyle realizeActionStyle = new GUIStyle();
            realizeActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.magenta);
            if (function.actionInput == null)
            {
                if (!function.startFunction)
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
                if (function.actionInput is DataSplitter)
                {
                    DataSplitter input = (DataSplitter)function.actionInput;
                    if (input.trueOutput == function && !input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.trueOutput == function && input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == function && !input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == function && input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if (function.actionInput is LogicSplitter)
                {
                    LogicSplitter input = (LogicSplitter)function.actionInput;
                    int index = input.actionOutputs.IndexOf(function);
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
                else if (function.actionInput is WaitFunction)
                {
                    WaitFunction input = (WaitFunction)function.actionInput;
                    if (input.actionOutput == function && !input.realize)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.actionOutput == function && input.realize)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if (function.actionInput is ChallengeFunction)
                {
                    ChallengeFunction input = (ChallengeFunction)function.actionInput;
                    if (input.trueOutput == function && !input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.trueOutput == function && input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == function && !input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == function && input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if(function.actionInput is ChooseMethodFunction)
                {
                    ChooseMethodFunction input = (ChooseMethodFunction)function.actionInput;
                    if (input.brutalOutput == function && !input.realizeBrutal)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.brutalOutput == function && input.realizeBrutal)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.carefulOutput == function && !input.realizeCareful)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.carefulOutput == function && input.realizeCareful)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.diplomatOutput == function && !input.realizeDiplomat)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.diplomatOutput == function && input.realizeDiplomat)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.scienceOutput == function && !input.realizeScience)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.scienceOutput == function && input.realizeScience)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
            }
            GUILayout.FlexibleSpace();
            DrawMethodSelector();
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();
            if (function.brutalOutput == null)
            {
                GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else
            {
                if (function.realizeBrutal)
                {
                    GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else
                {
                    GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }
            GUILayout.Space(15);
            if (function.carefulOutput == null)
            {
                GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else
            {
                if (function.realizeCareful)
                {
                    GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else
                {
                    GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }
            GUILayout.Space(15);
            if (function.diplomatOutput == null)
            {
                GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else
            {
                if (function.realizeDiplomat)
                {
                    GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else
                {
                    GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }
            GUILayout.Space(15);
            if (function.scienceOutput == null)
            {
                GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else
            {
                if (function.realizeScience)
                {
                    GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else
                {
                    GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            ChooseMethodFunction function = (ChooseMethodFunction)logicFunction;
            if (function.actionInput != null)
            {
                function.actionInput.RemoveActionOutput(function);
            }
            if (function.brutalOutput != null)
            {
                function.brutalOutput.RemoveActionInput(function);
            }
            if (function.carefulOutput != null)
            {
                function.carefulOutput.RemoveActionInput(function);
            }
            if (function.diplomatOutput != null)
            {
                function.diplomatOutput.RemoveActionInput(function);
            }
            if (function.scienceOutput != null)
            {
                function.scienceOutput.RemoveActionInput(function);
            }
            LogicMapEditor.logicMap.logicFunc.Remove(function);
            DestroyImmediate(function.gameObject);
        }

        public void SetActionOutputLink()
        {
            ChooseMethodFunction methodFunction = logicFunction as ChooseMethodFunction;
            methodFunction.SetOutputLink(null);
            selectActionLink = true;
            LogicMapEditor.editor.selectFunctionNode = this;
        }

        private void DrawMethodSelector()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(Method.Brutal.ToString(), new GUILayoutOption[] { GUILayout.Width(100) });
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(Method.Careful.ToString(), new GUILayoutOption[] { GUILayout.Width(100) });
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(Method.Diplomatic.ToString(), new GUILayoutOption[] { GUILayout.Width(100) });
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(Method.Scientific.ToString(), new GUILayoutOption[] { GUILayout.Width(100) });
            EditorGUILayout.EndVertical();
        }
    }
}