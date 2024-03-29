﻿using System.Collections;
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

                Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
            }
            for(int i=0; i<function.dialogOutputs.Count; i++)
            {
                if (function.dialogOutputs[i] != null)
                {
                    startPos = new Vector2(function.GetWindowRect().xMax, function.windowRect.yMax - 20 * (function.dialogOutputs.Count - i) + 5);

                    BaseLogicNode outputNode = null;
                    foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                    {
                        if (node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction == function.dialogOutputs[i])
                        {
                            outputNode = node;
                        }
                    }
                    endPos = new Vector2(((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().xMin, ((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().y + 25);

                    Vector2 startTan = startPos + Vector2.right * 50;
                    Vector2 endTan = endPos + Vector2.left * 50;
                    Color activeColor = Color.blue;
                    Color backColor = Color.black;
                    if (function.realizeDialogOutput[i])
                    {
                        activeColor = Color.magenta;
                    }
                    backColor = new Color(backColor.r, backColor.g, backColor.b, 0.1f);
                    int width = 2;

                    Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
                }
            }

            if (!function.dialogOutputFlag)
            {
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
            else
            {
                if (selectActionLink)
                {
                    Event e = Event.current;
                        startPos = new Vector2(function.GetWindowRect().xMax, function.windowRect.yMax - 20 * (function.dialogOutputs.Count - function.dialogOutputNum) + 10);

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
        }

        public override void DrawWindow()
        {
            ChooseMethodFunction function = logicFunction as ChooseMethodFunction;
            SerializedObject soFunction = new SerializedObject(function);
            EditorGUILayout.BeginHorizontal();
            GUIStyle noneActionStyle = new GUIStyle();
            noneActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.gray);
            GUIStyle actionStyle = new GUIStyle();
            actionStyle.normal.background = eUtils.MakeTex(10, 10, Color.blue);
            GUIStyle realizeActionStyle = new GUIStyle();
            realizeActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.magenta);
            if (function.actionInputs.Count == 0)
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
                bool realizeFlag = false;
                foreach (LogicFunction actionInput in function.actionInputs)
                {
                    if (actionInput is DataSplitter)
                    {
                        DataSplitter input = (DataSplitter)actionInput;
                        if (input.trueOutput == function && input.realizeTrue)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.falseOutput == function && input.realizeFalse)
                        {
                            realizeFlag = true;
                            break;
                        }
                    }
                    else if (actionInput is LogicSplitter)
                    {
                        LogicSplitter input = (LogicSplitter)actionInput;
                        int index = input.actionOutputs.IndexOf(function);
                        if (input.realizeOutputs[index])
                        {
                            realizeFlag = true;
                            break;
                        }
                    }
                    else if (actionInput is WaitFunction)
                    {
                        WaitFunction input = (WaitFunction)actionInput;
                        if (input.actionOutput == function && input.realize)
                        {
                            realizeFlag = true;
                            break;
                        }
                    }
                    else if (actionInput is ChallengeFunction)
                    {
                        ChallengeFunction input = (ChallengeFunction)actionInput;
                        if (input.trueOutput == function && input.realizeTrue)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.falseOutput == function && input.realizeFalse)
                        {
                            realizeFlag = true;
                            break;
                        }
                    }
                    else if (actionInput is ChooseMethodFunction)
                    {
                        ChooseMethodFunction input = (ChooseMethodFunction)actionInput;
                        if (input.brutalOutput == function && input.realizeBrutal)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.carefulOutput == function && input.realizeCareful)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.diplomatOutput == function && input.realizeDiplomat)
                        {
                            realizeFlag = true;
                            break;
                        }
                        else if (input.scienceOutput == function && input.realizeScience)
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
            GUILayout.Box("", new GUILayoutOption[] { GUILayout.Height(1), GUILayout.ExpandWidth(true) });
            EditorGUILayout.PropertyField(soFunction.FindProperty("dialog"), GUIContent.none, new GUILayoutOption[] { GUILayout.Width(120) });
            EditorGUILayout.BeginVertical();
            EditorGUILayout.EndVertical();
            if (function.dialog != null)
            {
                int dif = function.dialogOutputs.Count - function.dialog.GetEnds().Count;
                if (dif > 0)
                {
                    for(int i=0; i < dif; i++)
                    {
                        LogicFunction output = function.dialogOutputs[function.dialogOutputs.Count - 1];
                        if (output != null)
                        {
                            output.SetActionInputLink(null);
                        }
                        function.dialogOutputs.RemoveAt(function.dialogOutputs.Count - 1);
                        function.realizeDialogOutput.RemoveAt(function.dialogOutputs.Count - 1);
                    }
                }
                else if (dif < 0)
                {
                    for(int i=0; i < -dif; i++)
                    {
                        function.dialogOutputs.Add(null);
                        function.realizeDialogOutput.Add(false);
                    }
                }
                dif = function.dialogOutputs.Count - function.dialogFileNotes.Count;
                if (dif > 0)
                {
                    for (int i = 0; i < dif; i++)
                    {
                        function.dialogFileNotes.Add(null);
                    }
                }
                else if (dif < 0)
                {
                    for (int i = 0; i < -dif; i++)
                    {
                        function.dialogFileNotes.RemoveAt(function.dialogFileNotes.Count - 1);
                    }
                }

                for (int i=0; i<function.dialog.GetEnds().Count; i++)
                {
                    EditorGUILayout.BeginHorizontal(new GUILayoutOption[] { GUILayout.Height(20)});
                    EditorGUILayout.LabelField(function.dialog.GetEnds()[i].chooseText, new GUILayoutOption[] { GUILayout.Width(100) });
                    GUILayout.FlexibleSpace();
                    LogicFunction output = function.dialogOutputs[i];
                    EditorGUILayout.BeginVertical();
                    GUILayout.FlexibleSpace();
                    if (output == null)
                    {
                        GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (function.realizeDialogOutput[i])
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                for (int i = 0; i < function.dialogOutputs.Count; i++)
                {
                    LogicFunction output = function.dialogOutputs[function.dialogOutputs.Count - 1];
                    if (output != null)
                    {
                        output.SetActionInputLink(null);
                    }
                    function.dialogOutputs.RemoveAt(function.dialogOutputs.Count - 1);
                    function.realizeDialogOutput.RemoveAt(function.dialogOutputs.Count - 1);
                }
            }
            soFunction.ApplyModifiedProperties();
            function.windowRect.height = 115 + 25;
            if (function.dialog != null)
            {
                function.windowRect.height+= 20 * function.dialog.GetEnds().Count;
            }
        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            ChooseMethodFunction function = (ChooseMethodFunction)logicFunction;
            foreach (LogicFunction actionInput in function.actionInputs)
            {
                actionInput.RemoveActionOutput(function);
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
            foreach (LogicFunction dialogOutput in function.dialogOutputs)
            {
                dialogOutput.RemoveActionInput(function);
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
            EditorGUILayout.LabelField(Method.Accuracy.ToString(), new GUILayoutOption[] { GUILayout.Width(100) });
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(Method.Diplomacy.ToString(), new GUILayoutOption[] { GUILayout.Width(100) });
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(Method.Science.ToString(), new GUILayoutOption[] { GUILayout.Width(100) });
            EditorGUILayout.EndVertical();
        }
    }
}