using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class ChallengeNode : LogicFunctionNode
    {
        private void Awake()
        {
            windowTitle = "Challenge";
        }

        public override void DrawLinks()
        {
            ChallengeFunction function = logicFunction as ChallengeFunction;
            Vector2 startPos;
            Vector2 endPos;
            if (function.trueOutput != null)
            {
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 30);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction == function.trueOutput)
                    {
                        outputNode = node;
                    }
                }
                endPos = new Vector2(((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().xMin, ((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().y + 25);

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.blue;
                Color backColor = Color.black;
                if (function.realizeTrue)
                {
                    activeColor = Color.magenta;
                }
                backColor = new Color(backColor.r, backColor.g, backColor.b, 0.1f);
                int width = 2;

                Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
            }
            if (function.falseOutput != null)
            {
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 50);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction == function.falseOutput)
                    {
                        outputNode = node;
                    }
                }
                endPos = new Vector2(((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().xMin, ((LogicFunctionNode)outputNode).logicFunction.GetWindowRect().y + 25);

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.blue;
                Color backColor = Color.black;
                if (function.realizeFalse)
                {
                    activeColor = Color.magenta;
                }
                backColor = new Color(backColor.r, backColor.g, backColor.b, 0.1f);
                int width = 2;

                Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
            }
            if (selectActionLink && function.trueFlag)
            {
                Event e = Event.current;
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 30);
                endPos = e.mousePosition;
                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.yellow;
                Color backColor = Color.black;
                backColor = new Color(backColor.r, backColor.g, backColor.b, 0.1f);
                int width = 2;

                Handles.DrawBezier(startPos, endPos, startTan, endTan, activeColor, null, width);
            }
            else if (selectActionLink && !function.trueFlag)
            {
                Event e = Event.current;
                startPos = new Vector2(function.GetWindowRect().xMax, function.GetWindowRect().y + 45);
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

        public override void DrawWindow()
        {
            ChallengeFunction function = logicFunction as ChallengeFunction;
            SerializedObject soWait = new SerializedObject(function);

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
                foreach(LogicFunction actionInput in function.actionInputs)
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
            DrawChallengeSelector(function.challenge);
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();
            if (function.trueOutput == null)
            {
                GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else
            {
                if (function.realizeTrue)
                {
                    GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
                else
                {
                    GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                }
            }
            GUILayout.Space(15);
            if (function.falseOutput == null)
            {
                    GUILayout.Box(GUIContent.none, noneActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
            }
            else
            {
                if (function.realizeFalse)
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
            soWait.ApplyModifiedProperties();
        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            ChallengeFunction function = (ChallengeFunction)logicFunction;
            foreach(LogicFunction actionInput in function.actionInputs)
            {
                actionInput.RemoveActionOutput(function);
            }
            if (function.trueOutput != null)
            {
                function.trueOutput.RemoveActionInput(function);
            }
            if (function.falseOutput != null)
            {
                function.falseOutput.RemoveActionInput(function);
            }
            LogicMapEditor.logicMap.logicFunc.Remove(function);
            DestroyImmediate(function.gameObject);
        }

        public void SetActionOutputLink()
        {
            ChallengeFunction challengeFunction = logicFunction as ChallengeFunction;
            challengeFunction.SetOutputLink(null);
            selectActionLink = true;
            LogicMapEditor.editor.selectFunctionNode = this;
        }

        private void DrawChallengeSelector(TeamChallenge challenge)
        {
            SerializedObject soChallenge = new SerializedObject(challenge);
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(soChallenge.FindProperty("type"), GUIContent.none);
            if(challenge.type == ChallengeType.HAVE_TAG)
            {
                EditorGUILayout.PropertyField(soChallenge.FindProperty("executor"), GUIContent.none);
                EditorGUILayout.PropertyField(soChallenge.FindProperty("_tag"), GUIContent.none);
            }
            else if (challenge.type == ChallengeType.METHOD)
            {
                EditorGUILayout.PropertyField(soChallenge.FindProperty("executor"), GUIContent.none);
                EditorGUILayout.PropertyField(soChallenge.FindProperty("method"), GUIContent.none);
                EditorGUILayout.PropertyField(soChallenge.FindProperty("_tag"), GUIContent.none);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Level", new GUILayoutOption[] { GUILayout.Width(100) });
                EditorGUILayout.PropertyField(soChallenge.FindProperty("level"), GUIContent.none, new GUILayoutOption[] { GUILayout.Width(40) });
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Difficulty", new GUILayoutOption[] { GUILayout.Width(100) });
                EditorGUILayout.PropertyField(soChallenge.FindProperty("difficulty"), GUIContent.none, new GUILayoutOption[] { GUILayout.Width(40) });
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(challenge);
            soChallenge.ApplyModifiedProperties();
        }
    }
}
