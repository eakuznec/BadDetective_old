using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class EffectNode : LogicFunctionNode
    {
        private void Awake()
        {
            windowTitle = "Effect";
        }

        public override void DrawWindow()
        {
            LogicEffect logicEffect = logicFunction as LogicEffect;

            Effect effect = logicEffect.effect;
            SerializedObject soEffect = new SerializedObject(effect);
            EditorGUILayout.BeginHorizontal();
            GUIStyle noneActionStyle = new GUIStyle();
            noneActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.gray);
            GUIStyle actionStyle = new GUIStyle();
            actionStyle.normal.background = eUtils.MakeTex(10, 10, Color.blue);
            GUIStyle realizeActionStyle = new GUIStyle();
            realizeActionStyle.normal.background = eUtils.MakeTex(10, 10, Color.magenta);
            if (logicEffect.actionInput == null)
            {
                if (!logicEffect.startFunction)
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
                if (logicEffect.actionInput is DataSplitter)
                {
                    DataSplitter input = (DataSplitter)logicEffect.actionInput;
                    if (input.trueOutput == logicEffect && !input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.trueOutput == logicEffect && input.realizeTrue)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == logicEffect && !input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else if (input.falseOutput == logicEffect && input.realizeFalse)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
                else if (logicEffect.actionInput is LogicSplitter)
                {
                    LogicSplitter input = (LogicSplitter)logicEffect.actionInput;
                    int index = input.actionOutputs.IndexOf(logicEffect);
                    if(index == -1)
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
                else if (logicEffect.actionInput is WaitFunction)
                {
                    WaitFunction input = (WaitFunction)logicEffect.actionInput;
                    if (input.realize)
                    {
                        GUILayout.Box(GUIContent.none, realizeActionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                    else
                    {
                        GUILayout.Box(GUIContent.none, actionStyle, new GUILayoutOption[] { GUILayout.Width(10), GUILayout.Height(10) });
                    }
                }
            }

            GUILayout.FlexibleSpace();
            eUtils.DrawEffectSelector(effect);
            EditorGUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Dublicate"))
            {
                EffectNode newNode = LogicMapEditor.editor.CreateEffectNode(logicEffect.windowRect.position + new Vector2(20, 20));
                ((LogicEffect)newNode.logicFunction).effect.copyContentFrom(logicEffect.effect);
            }
        }

        public override void DrawLinks()
        {
        }

        public override void DeleteNode()
        {
            base.DeleteNode();
            LogicEffect function = (LogicEffect)logicFunction;
            if (function.actionInput != null)
            {
                function.actionInput.RemoveActionOutput(function);
            }
            LogicMapEditor.logicMap.effects.Remove(function);
            DestroyImmediate(function.gameObject);
        }
    }
}
