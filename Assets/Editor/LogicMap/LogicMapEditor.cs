﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BadDetective.LogicMap
{
    public class LogicMapEditor : EditorWindow
    {
        public static LogicMapEditor editor;
        public static LogicMap logicMap;
        public GameObject prefab;
        private string pathToSave = "Assets/Global Values/LogicMaps/";

        private EnterNode enterNode;
        private int id;
        public List<BaseLogicNode> nodes = new List<BaseLogicNode>();

        //Select
        public LogicFunctionNode selectFunctionNode;
        public iBoolDataNode selectDataNode;

        //Control
        public Vector2 mousePos;
        private bool moveMod;
        private bool actionLinkMod;
        private bool dataLinkMod;

        [MenuItem("Window/Logic Map Editor")]
        public static void ShowEditor()
        {
            editor = EditorWindow.GetWindow<LogicMapEditor>(false, "Logic Map Editor");

            //editor.serializedObject = new SerializedObject(editor);
            //editor.property = editor.serializedObject.FindProperty("prefab");
        }

        private void Awake()
        {
            if (logicMap == null)
            {
                int logicMapsCount = FindObjectsOfType<LogicMap>().Length;
                GameObject newLogicMap = new GameObject(string.Format("LogicMap_{0}", logicMapsCount));
                logicMap = newLogicMap.AddComponent<LogicMap>();
                prefab = null;

                ClearAll();

                enterNode = new EnterNode(new Vector2(50, 50));
            }
            else
            {
                LoadLogicMap();
            }
        }

        private void OnDestroy()
        {
            //DestroyImmediate(logicMap.gameObject);
            ClearAll();
            logicMap = null;
        }

        private void OnGUI()
        {
            Event e = Event.current;
            mousePos = e.mousePosition;

            int select = -1;
            if (e.button == 0)
            {
                if (e.type == EventType.MouseUp)
                {
                    if (actionLinkMod)
                    {
                        actionLinkMod = false;
                        enterNode.selectLink = false;
                        foreach (BaseLogicNode node in nodes)
                        {
                                if (node is LogicSplitterNode)
                                {
                                    if (((LogicSplitterNode)node).logicFunction.GetWindowRect().Contains(mousePos))
                                    {
                                        if (selectFunctionNode == null)
                                        {
                                            SetStartFunction(((LogicSplitterNode)node).logicFunction);
                                        }
                                        else if (selectFunctionNode is LogicSplitterNode)
                                        {
                                            ((LogicSplitter)selectFunctionNode.logicFunction).SetOutputLink(((LogicSplitterNode)node).logicFunction);
                                            ((LogicSplitterNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                            if (((LogicSplitterNode)node).logicFunction == logicMap.startFunction)
                                            {
                                                SetStartFunction(null);
                                            }
                                        }
                                        else if (selectFunctionNode is DataSplitterNode)
                                        {
                                            ((DataSplitter)selectFunctionNode.logicFunction).SetOutputLink(((LogicSplitterNode)node).logicFunction);
                                            ((LogicSplitterNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                            if (((LogicSplitterNode)node).logicFunction == logicMap.startFunction)
                                            {
                                                SetStartFunction(null);
                                            }
                                        }
                                    else if (selectFunctionNode is WaitNode)
                                    {
                                        ((WaitFunction)selectFunctionNode.logicFunction).SetOutputLink(((LogicSplitterNode)node).logicFunction);
                                        ((LogicSplitterNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                        if (((LogicSplitterNode)node).logicFunction == logicMap.startFunction)
                                        {
                                            SetStartFunction(null);
                                        }
                                    }
                                    break;
                                    }
                                }
                                else if (node is DataSplitterNode)
                                {
                                    if (((DataSplitterNode)node).logicFunction.GetWindowRect().Contains(mousePos))
                                    {

                                        if (selectFunctionNode == null)
                                        {
                                            SetStartFunction(((DataSplitterNode)node).logicFunction);
                                        }
                                        else if (selectFunctionNode is LogicSplitterNode)
                                        {
                                            ((LogicSplitter)selectFunctionNode.logicFunction).SetOutputLink(((DataSplitterNode)node).logicFunction);
                                            ((DataSplitterNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                            if (((DataSplitterNode)node).logicFunction == logicMap.startFunction)
                                            {
                                                SetStartFunction(null);
                                            }
                                        }
                                        else if (selectFunctionNode is DataSplitterNode)
                                        {
                                            ((DataSplitter)selectFunctionNode.logicFunction).SetOutputLink(((DataSplitterNode)node).logicFunction);
                                            ((DataSplitterNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                            if (((DataSplitterNode)node).logicFunction == logicMap.startFunction)
                                            {
                                                SetStartFunction(null);
                                            }
                                        }
                                    else if (selectFunctionNode is WaitNode)
                                    {
                                        ((WaitFunction)selectFunctionNode.logicFunction).SetOutputLink(((DataSplitterNode)node).logicFunction);
                                        ((DataSplitterNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                        if (((DataSplitterNode)node).logicFunction == logicMap.startFunction)
                                        {
                                            SetStartFunction(null);
                                        }
                                    }
                                    break;
                                    }
                                }
                            else if (node is WaitNode)
                            {
                                if (((WaitNode)node).logicFunction.GetWindowRect().Contains(mousePos))
                                {

                                    if (selectFunctionNode == null)
                                    {
                                        SetStartFunction(((WaitNode)node).logicFunction);
                                    }
                                    else if (selectFunctionNode is LogicSplitterNode)
                                    {
                                        ((LogicSplitter)selectFunctionNode.logicFunction).SetOutputLink(((WaitNode)node).logicFunction);
                                        ((WaitNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                        if (((WaitNode)node).logicFunction == logicMap.startFunction)
                                        {
                                            SetStartFunction(null);
                                        }
                                    }
                                    else if (selectFunctionNode is DataSplitterNode)
                                    {
                                        ((DataSplitter)selectFunctionNode.logicFunction).SetOutputLink(((WaitNode)node).logicFunction);
                                        ((WaitNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                        if (((WaitNode)node).logicFunction == logicMap.startFunction)
                                        {
                                            SetStartFunction(null);
                                        }
                                    }
                                    else if (selectFunctionNode is WaitNode)
                                    {
                                        ((WaitFunction)selectFunctionNode.logicFunction).SetOutputLink(((WaitNode)node).logicFunction);
                                        ((WaitNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                        if (((WaitNode)node).logicFunction == logicMap.startFunction)
                                        {
                                            SetStartFunction(null);
                                        }
                                    }
                                    break;
                                }
                            }
                            else if (node is EffectNode)
                                {
                                    if (((EffectNode)node).logicFunction.GetWindowRect().Contains(mousePos))
                                    {

                                        if (selectFunctionNode == null)
                                        {
                                            SetStartFunction(((EffectNode)node).logicFunction);
                                        }
                                        else if (selectFunctionNode is LogicSplitterNode)
                                        {
                                            ((LogicSplitter)selectFunctionNode.logicFunction).SetOutputLink(((EffectNode)node).logicFunction);
                                            ((EffectNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                            if (((EffectNode)node).logicFunction == logicMap.startFunction)
                                            {
                                                SetStartFunction(null);
                                            }
                                        }
                                        else if (selectFunctionNode is DataSplitterNode)
                                        {
                                            ((DataSplitter)selectFunctionNode.logicFunction).SetOutputLink(((EffectNode)node).logicFunction);
                                            ((EffectNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                            if (((EffectNode)node).logicFunction == logicMap.startFunction)
                                            {
                                                SetStartFunction(null);
                                            }
                                        }
                                    else if (selectFunctionNode is WaitNode)
                                    {
                                        ((WaitFunction)selectFunctionNode.logicFunction).SetOutputLink(((EffectNode)node).logicFunction);
                                        ((EffectNode)node).logicFunction.SetActionInputLink(selectFunctionNode.logicFunction);
                                        if (((EffectNode)node).logicFunction == logicMap.startFunction)
                                        {
                                            SetStartFunction(null);
                                        }
                                    }
                                    break;
                                    }
                                }
                            
                        }
                        if(selectFunctionNode != null)
                        {
                            selectFunctionNode.selectActionLink = false;
                            if(selectFunctionNode is LogicSplitterNode && ((LogicSplitter)selectFunctionNode.logicFunction).actionOutputs.Contains(null))
                            {
                                ((LogicSplitter)selectFunctionNode.logicFunction).actionOutputs.RemoveAt(((LogicSplitter)selectFunctionNode.logicFunction).actionOutputs.IndexOf(null));
                            }
                            selectFunctionNode = null;
                        }
                    }
                    else if (dataLinkMod)
                    {
                        dataLinkMod = false;
                        foreach(BaseLogicNode node in nodes)
                        {
                            if(node is LogicAndNode)
                            {
                                Rect rect = ((LogicDataFunctionNode)node).dataFunction.GetWindowRect();
                                if(new Rect(rect.x, rect.y+20, rect.width, 20).Contains(mousePos))
                                {
                                    ((LogicAND)((LogicAndNode)node).GetDataFunction()).firstInput = true;
                                    if (selectDataNode is LogicDataFunctionNode)
                                    {
                                        ((LogicDataFunction)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicAndNode)node).GetDataFunction());
                                        ((LogicAND)((LogicAndNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }
                                    else if (selectDataNode is ConditionNode)
                                    {
                                        ((LogicCondition)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicAndNode)node).GetDataFunction());
                                        ((LogicAND)((LogicAndNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }
                                    else if (selectDataNode is DataVariableNode)
                                    {
                                        ((DataVariable)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicAndNode)node).GetDataFunction());
                                        ((LogicAND)((LogicAndNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }

                                }
                                else if (new Rect(rect.x, rect.y + 40, rect.width, 20).Contains(mousePos))
                                {
                                    ((LogicAND)((LogicAndNode)node).GetDataFunction()).firstInput = false;
                                    if (selectDataNode is LogicDataFunctionNode)
                                    {
                                        ((LogicDataFunction)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicAndNode)node).GetDataFunction());
                                        ((LogicAND)((LogicAndNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }
                                    else if (selectDataNode is ConditionNode)
                                    {
                                        ((LogicCondition)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicAndNode)node).GetDataFunction());
                                        ((LogicAND)((LogicAndNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }
                                    else if (selectDataNode is DataVariableNode)
                                    {
                                        ((DataVariable)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicAndNode)node).GetDataFunction());
                                        ((LogicAND)((LogicAndNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }

                                }
                            }
                            else if (node is LogicOrNode)
                            {
                                Rect rect = ((LogicDataFunctionNode)node).dataFunction.GetWindowRect();
                                if (new Rect(rect.x, rect.y + 20, rect.width, 20).Contains(mousePos))
                                {
                                    ((LogicOR)((LogicOrNode)node).GetDataFunction()).firstInput = true;
                                    if (selectDataNode is LogicDataFunctionNode)
                                    {
                                        ((LogicDataFunction)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicOrNode)node).GetDataFunction());
                                        ((LogicOR)((LogicOrNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }
                                    else if (selectDataNode is ConditionNode)
                                    {
                                        ((LogicCondition)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicOrNode)node).GetDataFunction());
                                        ((LogicOR)((LogicOrNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }
                                    else if (selectDataNode is DataVariableNode)
                                    {
                                        ((DataVariable)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicOrNode)node).GetDataFunction());
                                        ((LogicOR)((LogicOrNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }

                                }
                                else if (new Rect(rect.x, rect.y + 40, rect.width, 20).Contains(mousePos))
                                {
                                    ((LogicOR)((LogicOrNode)node).GetDataFunction()).firstInput = false;
                                    if (selectDataNode is LogicDataFunctionNode)
                                    {
                                        ((LogicDataFunction)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicOrNode)node).GetDataFunction());
                                        ((LogicOR)((LogicOrNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }
                                    else if (selectDataNode is ConditionNode)
                                    {
                                        ((LogicCondition)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicOrNode)node).GetDataFunction());
                                        ((LogicOR)((LogicOrNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }
                                    else if (selectDataNode is DataVariableNode)
                                    {
                                        ((DataVariable)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicOrNode)node).GetDataFunction());
                                        ((LogicOR)((LogicOrNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                    }

                                }
                            }
                            else if (node is LogicDataFunctionNode)
                                {
                                    if (((LogicDataFunctionNode)node).dataFunction.GetWindowRect().Contains(mousePos))
                                    {
                                        if (selectDataNode is LogicDataFunctionNode)
                                        {
                                            ((LogicDataFunction)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicDataFunctionNode)node).GetDataFunction());
                                        ((LogicDataFunction)((LogicDataFunctionNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                        }
                                        else if (selectDataNode is ConditionNode)
                                        {
                                        ((LogicCondition)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicDataFunctionNode)node).GetDataFunction());
                                        ((LogicDataFunction)((LogicDataFunctionNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                        }
                                        else if (selectDataNode is DataVariableNode)
                                        {
                                        ((DataVariable)selectDataNode.GetDataFunction()).SetDataOutputLink(((LogicDataFunctionNode)node).GetDataFunction());
                                            ((LogicDataFunction)((LogicDataFunctionNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                        }
                                    }
                                }
                                else if (node is DataSplitterNode)
                                {
                                    if (((DataSplitterNode)node).logicFunction.GetWindowRect().Contains(mousePos))
                                    {
                                        if (selectDataNode is LogicDataFunctionNode)
                                        {
                                        ((LogicDataFunction)selectDataNode.GetDataFunction()).SetDataOutputLink(((DataSplitterNode)node).GetDataFunction());
                                            ((DataSplitter)((DataSplitterNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                        }
                                        else if (selectDataNode is ConditionNode)
                                        {
                                        ((LogicCondition)selectDataNode.GetDataFunction()).SetDataOutputLink(((DataSplitterNode)node).GetDataFunction());
                                        ((DataSplitter)((DataSplitterNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                        }
                                        else if (selectDataNode is DataVariableNode)
                                        {
                                        ((DataVariable)selectDataNode.GetDataFunction()).SetDataOutputLink(((DataSplitterNode)node).GetDataFunction());
                                        ((DataSplitter)((DataSplitterNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                        }
                                    }
                                }
                                else if (node is DataVariableNode)
                                {
                                    if (((DataVariable)((DataVariableNode)node).dataVariable).GetWindowRect().Contains(mousePos))
                                    {

                                        if (selectDataNode is LogicDataFunctionNode)
                                        {
                                        ((LogicDataFunction)selectDataNode.GetDataFunction()).SetDataOutputLink(((DataVariableNode)node).GetDataFunction());
                                            ((DataVariable)((DataVariableNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                        }
                                        else if (selectDataNode is ConditionNode)
                                        {
                                        ((LogicCondition)selectDataNode.GetDataFunction()).SetDataOutputLink(((DataVariableNode)node).GetDataFunction());
                                        ((DataVariable)((DataVariableNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                        }
                                        else if (selectDataNode is DataVariableNode)
                                        {
                                        ((DataVariable)selectDataNode.GetDataFunction()).SetDataOutputLink(((DataVariableNode)node).GetDataFunction());
                                        ((DataVariable)((DataVariableNode)node).GetDataFunction()).SetDataInputLink(selectDataNode.GetDataFunction());
                                        }
                                    }
                                }
                        }
                        if (selectDataNode != null)
                        {
                            selectDataNode.SetSelectDataLink(false);
                            selectDataNode = null;
                        }
                    }
                }
            }
            else if (e.button == 1)
            {
                if (e.type == EventType.MouseDown)
                {
                }
                else if (e.type == EventType.MouseDrag)
                {
                    if (!moveMod && e.delta!=Vector2.zero)
                    {
                        moveMod = true;
                    }
                    if (moveMod)
                    {
                        logicMap.enterWindowRect.position -= e.delta;
                        foreach (LogicCondition function in logicMap.conditions)
                        {
                            function.windowRect.position -= e.delta;
                        }
                        foreach (LogicDataFunction function in logicMap.dataFunc)
                        {
                            function.windowRect.position -= e.delta;
                        }
                        foreach (DataVariable function in logicMap.dataVariables)
                        {
                            function.windowRect.position -= e.delta;
                        }
                        foreach (LogicEffect function in logicMap.effects)
                        {
                            function.windowRect.position -= e.delta;
                        }
                        foreach (LogicFunction function in logicMap.logicFunc)
                        {
                            function.windowRect.position -= e.delta;
                        }
                    }
                }
                else if (e.type == EventType.MouseUp)
                {
                    if (actionLinkMod)
                    {
                        actionLinkMod = false;
                        enterNode.selectLink = false;
                        if (selectFunctionNode != null)
                        {
                            selectFunctionNode.selectActionLink = false;
                            if (selectFunctionNode is LogicSplitterNode && ((LogicSplitter)selectFunctionNode.logicFunction).actionOutputs.Contains(null))
                            {
                                ((LogicSplitter)selectFunctionNode.logicFunction).actionOutputs.RemoveAt(((LogicSplitter)selectFunctionNode.logicFunction).actionOutputs.IndexOf(null));
                            }
                            selectFunctionNode = null;
                        }
                    }
                    else if (dataLinkMod)
                    {
                        dataLinkMod = false;
                        if (selectDataNode != null)
                        {
                            selectDataNode.SetSelectDataLink(false);
                            selectDataNode = null;
                        }
                    }
                    else if (moveMod)
                    {
                        moveMod = false;
                    }
                    else
                    {
                        foreach (BaseLogicNode node in nodes)
                        {
                            if (node is LogicDataFunctionNode && ((LogicDataFunctionNode)node).dataFunction.GetWindowRect().Contains(mousePos))
                            {
                                select = node.id;
                                LogicDataNodeGenericMenu();
                                break;
                            }
                            else if (node is LogicSplitterNode && ((LogicSplitterNode)node).logicFunction.GetWindowRect().Contains(mousePos))
                            {
                                select = node.id;
                                LogicSplitterNodeGenericMenu();
                                break;
                            }
                            else if (node is DataSplitterNode && ((DataSplitterNode)node).logicFunction.GetWindowRect().Contains(mousePos))
                            {
                                select = node.id;
                                DataSplitterNodeGenericMenu();
                                break;
                            }
                            else if (node is ConditionNode && ((ConditionNode)node).logicCondition.GetWindowRect().Contains(mousePos))
                            {
                                select = node.id;
                                DataNodeGenericMenu();
                                break;
                            }
                            else if (node is DataVariableNode && ((DataVariable)((DataVariableNode)node).dataVariable).GetWindowRect().Contains(mousePos))
                            {
                                select = node.id;
                                DataNodeGenericMenu();
                                break;
                            }
                            else if (node is EffectNode && ((EffectNode)node).logicFunction.GetWindowRect().Contains(mousePos))
                            {
                                select = node.id;
                                EffectNodeGenericMenu();
                                break;
                            }
                            else if (node is WaitNode && ((WaitNode)node).logicFunction.GetWindowRect().Contains(mousePos))
                            {
                                select = node.id;
                                WaitNodeGenericMenu();
                                break;
                            }
                        }
                        if (select == -1 && logicMap.enterWindowRect.Contains(mousePos))
                        {
                            select = -2;
                        }
                        if (select == -1)
                        {
                            CommonGenericMenu();
                        }
                        else if (select == -2)
                        {
                            EnterGenericMenu();
                        }
                    }
                }
            }

            BeginWindows();
            enterNode.DrawLink();
            foreach (BaseLogicNode node in nodes)
            {
                node.DrawLinks();
            }
            Color baseColor = GUI.contentColor;
            if(logicMap.startFunction == null)
            {
                GUI.color = Color.red;
            }
            else
            {
                if (!logicMap.startRealize)
                {
                    GUI.color = Color.blue;
                }
                else
                {
                    GUI.color = Color.magenta;
                }
            }
            logicMap.enterWindowRect = GUILayout.Window(-1, logicMap.enterWindowRect, WindowFunction, enterNode.windowTitle);
            GUI.color = baseColor;
            foreach (BaseLogicNode node in nodes)
            {
                if(node is LogicFunctionNode)
                {
                    ((LogicFunctionNode)node).logicFunction.windowRect = GUILayout.Window(node.id, ((LogicFunctionNode)node).logicFunction.windowRect, WindowFunction, node.windowTitle);
                }
                else if (node is LogicDataFunctionNode)
                {
                    ((LogicDataFunctionNode)node).dataFunction.windowRect = GUILayout.Window(node.id, ((LogicDataFunctionNode)node).dataFunction.windowRect, WindowFunction, node.windowTitle);
                }
                else if (node is ConditionNode)
                {
                    ((ConditionNode)node).logicCondition.windowRect = GUILayout.Window(node.id, ((ConditionNode)node).logicCondition.windowRect, WindowFunction, node.windowTitle);
                }
                else if (node is DataVariableNode)
                {
                    ((DataVariable)((DataVariableNode)node).dataVariable).windowRect = GUILayout.Window(node.id, ((DataVariable)((DataVariableNode)node).dataVariable).windowRect, WindowFunction, node.windowTitle);
                }
            }
            EndWindows();
            if(actionLinkMod || dataLinkMod || moveMod)
            {
                Repaint();
            }
        }

        public void WindowFunction(int id)
        {
            if (id == -1)
            {
                enterNode.DrawWindow();
            }
            else
            {
                foreach (BaseLogicNode node in nodes)
                {
                    if (node.id == id)
                    {
                        node.DrawWindow();
                        break;
                    }
                }
            }
            GUI.DragWindow();
        }

        private void ContextCallback(object obj)
        {
            string clb = obj.ToString();
            if (clb == "conditionNode")
            {
                CreateConditionNode(mousePos);
            }
            else if (clb == "effectNode")
            {
                CreateEffectNode(mousePos);
            }
            else if (clb == "variableNode")
            {
                CreateVariableNode(mousePos);
            }
            else if(clb == "andNode")
            {
                CreateAndNode(mousePos);
            }
            else if (clb == "orNode")
            {
                CreateOrNode(mousePos);
            }
            else if (clb == "notNode")
            {
                CreateNotNode(mousePos);
            }
            else if (clb == "logicSplitterNode")
            {
                CreateLogicSplitterNode(mousePos);
            }
            else if (clb == "dataSplitterNode")
            {
                CreateDataSplitterNode(mousePos);
            }
            else if (clb == "waitNode")
            {
                CreateWaitNode(mousePos);
            }
            else if (clb == "setStartNode")
            {
                foreach(BaseLogicNode node in nodes)
                {
                    if(node is LogicFunctionNode && ((LogicFunctionNode)node).logicFunction.windowRect.Contains(mousePos))
                    {
                        SetStartFunction(((LogicFunctionNode)node).logicFunction);
                    } 
                }
            }
            else if(clb == "resetStartNode")
            {
                ResetStartFunction();
            }
            else if (clb == "actionOutput")
            {
                actionLinkMod = true;
                foreach (BaseLogicNode node in nodes)
                {
                    if (node is LogicSplitterNode && ((LogicSplitterNode)node).logicFunction.windowRect.Contains(mousePos))
                    {
                        ((LogicSplitterNode)node).SetActionOutputLink();
                    }
                    else if (node is WaitNode && ((WaitNode)node).logicFunction.windowRect.Contains(mousePos))
                    {
                        ((WaitNode)node).SetActionOutputLink();
                    }
                }
            }
            else if(clb == "resetActionOutput")
            {
                foreach (BaseLogicNode node in nodes)
                {
                    if (node is LogicSplitterNode && ((LogicSplitterNode)node).logicFunction.windowRect.Contains(mousePos))
                    {
                        LogicSplitterNode logicSplitterNode = ((LogicSplitterNode)node);
                        LogicSplitter logicSplitter = logicSplitterNode.logicFunction as LogicSplitter;
                        for(int i=0; i< logicSplitter.actionOutputs.Count; i++)
                        {
                            Rect rect = new Rect(logicSplitter.windowRect.xMax - 20, logicSplitter.windowRect.y + 20 + 20 * i, 20, 20);
                            if (rect.Contains(mousePos))
                            {
                                actionLinkMod = true;
                                logicSplitterNode.SetActionOutputLink(i);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            else if(clb == "dataOutput")
            {
                foreach (BaseLogicNode node in nodes)
                {
                    if (node is LogicDataFunctionNode && ((LogicDataFunctionNode)node).dataFunction.windowRect.Contains(mousePos))
                    {
                        LogicDataFunctionNode dataFunctionNode = node as LogicDataFunctionNode;
                        LogicDataFunction dataFunction = (LogicDataFunction)dataFunctionNode.dataFunction;
                        dataLinkMod = true;
                        dataFunctionNode.SetDataOutputLink();
                        break;
                    }
                    else if (node is ConditionNode && ((ConditionNode)node).logicCondition.windowRect.Contains(mousePos))
                    {
                        ConditionNode conditionNode = node as ConditionNode;
                        LogicCondition dataFunction = conditionNode.logicCondition;
                        dataLinkMod = true;
                        conditionNode.SetDataOutputLink();
                        break;
                    }
                    else if (node is DataVariableNode && ((DataVariable)((DataVariableNode)node).dataVariable).windowRect.Contains(mousePos))
                    {
                        DataVariableNode dataVariableNode = node as DataVariableNode;
                        DataVariable dataFunction = (DataVariable)dataVariableNode.GetDataFunction();
                        dataLinkMod = true;
                        dataVariableNode.SetDataOutputLink();
                        break;
                    }
                }
            }
            else if (clb == "trueOutput")
            {
                foreach (BaseLogicNode node in nodes)
                {
                    if (node is DataSplitterNode && ((DataSplitterNode)node).logicFunction.windowRect.Contains(mousePos))
                    {
                        DataSplitterNode dataSplitterNode = node as DataSplitterNode;
                        DataSplitter dataFunction = (DataSplitter)dataSplitterNode.logicFunction;
                        dataFunction.trueFlag = true;
                        actionLinkMod = true;
                        dataSplitterNode.SetActionOutputLink();
                        break;
                    }
                }
            }
            else if (clb == "falseOutput")
            {
                foreach (BaseLogicNode node in nodes)
                {
                        if (node is DataSplitterNode && ((DataSplitterNode)node).logicFunction.windowRect.Contains(mousePos))
                        {
                            DataSplitterNode dataSplitterNode = node as DataSplitterNode;
                            DataSplitter dataFunction = (DataSplitter)dataSplitterNode.logicFunction;
                            dataFunction.trueFlag = false;
                            actionLinkMod = true;
                            dataSplitterNode.SetActionOutputLink();
                            break;
                        }
                }
            }
            else if (clb == "deleteNode")
            {
                for(int i=0; i<nodes.Count; i++)
                {
                    if (nodes[i] is LogicFunctionNode && ((LogicFunctionNode)nodes[i]).logicFunction.windowRect.Contains(mousePos))
                    {
                        nodes[i].DeleteNode();
                        i--;
                    }
                    else if (nodes[i] is LogicDataFunctionNode && ((LogicDataFunctionNode)nodes[i]).dataFunction.windowRect.Contains(mousePos))
                    {
                        nodes[i].DeleteNode();
                        i--;
                    }
                    else if (nodes[i] is ConditionNode && ((ConditionNode)nodes[i]).logicCondition.windowRect.Contains(mousePos))
                    {
                        nodes[i].DeleteNode();
                        i--;
                    }
                    else if (nodes[i] is DataVariableNode && ((DataVariable)((DataVariableNode)nodes[i]).dataVariable).windowRect.Contains(mousePos))
                    {
                        nodes[i].DeleteNode();
                        i--;
                    }
                }
            }
            else if (clb == "test")
            {
                TestLogicMap();
            }
            else if (clb == "save")
            {
                //SavePrefab();
            }
        }

        private LogicSplitterNode CreateLogicSplitterNode(Vector2 mousePos, LogicSplitter logicSplitter = null)
        {
            GameObject goFolder;
            if (logicMap.transform.Find("LogicSplitter") == null)
            {
                goFolder = new GameObject("LogicSplitter");
                goFolder.transform.parent = logicMap.transform;
            }
            else
            {
                goFolder = logicMap.transform.Find("LogicSplitter").gameObject;
            }
            LogicSplitterNode logicSplitterNode = new LogicSplitterNode();
            if(logicSplitter == null)
            {
                GameObject go = new GameObject(string.Format("{0}_LogicSplitter", id));
                logicSplitterNode.logicFunction = go.AddComponent<LogicSplitter>();
                go.transform.parent = goFolder.transform;
                logicMap.logicFunc.Add(logicSplitterNode.logicFunction);
                logicSplitterNode.logicFunction.windowRect = new Rect(mousePos, new Vector2(80, 40));
            }
            else
            {
                logicSplitterNode.logicFunction = logicSplitter;
            }
            logicSplitterNode.id = id;
            id++;
            nodes.Add(logicSplitterNode);
            if (logicMap.startFunction == null)
            {
                SetStartFunction(logicSplitterNode.logicFunction);
            }
            return logicSplitterNode;
        }

        private WaitNode CreateWaitNode(Vector2 mousePos, WaitFunction waitFunction = null)
        {
            GameObject goFolder;
            if (logicMap.transform.Find("Waits") == null)
            {
                goFolder = new GameObject("Waits");
                goFolder.transform.parent = logicMap.transform;
            }
            else
            {
                goFolder = logicMap.transform.Find("Waits").gameObject;
            }
            WaitNode waitNode = new WaitNode();
            if (waitFunction == null)
            {
                GameObject go = new GameObject(string.Format("{0}_Wait", id));
                waitNode.logicFunction = go.AddComponent<WaitFunction>();
                go.transform.parent = goFolder.transform;
                logicMap.logicFunc.Add(waitNode.logicFunction);
                waitNode.logicFunction.windowRect = new Rect(mousePos, new Vector2(220, 140));
            }
            else
            {
                waitNode.logicFunction = waitFunction;
            }
            waitNode.id = id;
            id++;
            nodes.Add(waitNode);
            if (logicMap.startFunction == null)
            {
                SetStartFunction(waitNode.logicFunction);
            }
            return waitNode;
        }

        private DataSplitterNode CreateDataSplitterNode(Vector2 mousePos, DataSplitter dataSplitter = null)
        {
            GameObject goFolder;
            if (logicMap.transform.Find("DataSplitter") == null)
            {
                goFolder = new GameObject("DataSplitter");
                goFolder.transform.parent = logicMap.transform;
            }
            else
            {
                goFolder = logicMap.transform.Find("DataSplitter").gameObject;
            }
            DataSplitterNode dataSplitterNode = new DataSplitterNode();
            if(dataSplitter == null)
            {
                GameObject go = new GameObject(string.Format("{0}_DataSplitter", id));
                dataSplitterNode.logicFunction = go.AddComponent<DataSplitter>();
                go.transform.parent = goFolder.transform;
                logicMap.logicFunc.Add(dataSplitterNode.logicFunction);
                dataSplitterNode.logicFunction.windowRect = new Rect(mousePos, new Vector2(80, 40));
            }
            else
            {
                dataSplitterNode.logicFunction = dataSplitter;
            }
            dataSplitterNode.id = id;
            id++;
            nodes.Add(dataSplitterNode);
            if (logicMap.startFunction == null)
            {
                SetStartFunction(dataSplitterNode.logicFunction);
            }
            return dataSplitterNode;
        }

        private LogicAndNode CreateAndNode(Vector2 mousePos, LogicAND logicAND = null)
        {
            GameObject goFolder;
            if (logicMap.transform.Find("AND") == null)
            {
                goFolder = new GameObject("AND");
                goFolder.transform.parent = logicMap.transform;
            }
            else
            {
                goFolder = logicMap.transform.Find("AND").gameObject;
            }
            LogicAndNode andNode = new LogicAndNode();
            andNode.id = id;
            id++;
            if(logicAND == null)
            {
                GameObject go = new GameObject(string.Format("{0}_LogicAnd", id));
                andNode.dataFunction = go.AddComponent<LogicAND>();
                go.transform.parent = goFolder.transform;
                logicMap.dataFunc.Add(andNode.dataFunction);
                andNode.dataFunction.windowRect = new Rect(mousePos, new Vector2(40, 60));
            }
            else
            {
                andNode.dataFunction = logicAND;
            }
            nodes.Add(andNode);
            return andNode;
        }

        private LogicOrNode CreateOrNode(Vector2 mousePos, LogicOR logicOR = null)
        {
            GameObject goFolder;
            if (logicMap.transform.Find("OR") == null)
            {
                goFolder = new GameObject("OR");
                goFolder.transform.parent = logicMap.transform;
            }
            else
            {
                goFolder = logicMap.transform.Find("OR").gameObject;
            }
            LogicOrNode orNode = new LogicOrNode();
            if(logicOR == null)
            {
                GameObject go = new GameObject(string.Format("{0}_LogicOr", id));
                orNode.dataFunction = go.AddComponent<LogicOR>();
                go.transform.parent = goFolder.transform;
                logicMap.dataFunc.Add(orNode.dataFunction);
                orNode.dataFunction.windowRect = new Rect(mousePos, new Vector2(40, 60));
            }
            else
            {
                orNode.dataFunction = logicOR;
            }
            orNode.id = id;
            id++;
            nodes.Add(orNode);
            return orNode;
        }

        private LogicNotNode CreateNotNode(Vector2 mousePos, LogicNOT logicNOT = null)
        {
            GameObject goFolder;
            if (logicMap.transform.Find("NOT") == null)
            {
                goFolder = new GameObject("NOT");
                goFolder.transform.parent = logicMap.transform;
            }
            else
            {
                goFolder = logicMap.transform.Find("NOT").gameObject;
            }
            LogicNotNode notNode = new LogicNotNode();
            if(logicNOT == null)
            {
                GameObject go = new GameObject(string.Format("{0}_LogicNot", id));
                notNode.dataFunction = go.AddComponent<LogicNOT>();
                go.transform.parent = goFolder.transform;
                logicMap.dataFunc.Add(notNode.dataFunction);
                notNode.dataFunction.windowRect = new Rect(mousePos, new Vector2(40, 40));
            }
            else
            {
                notNode.dataFunction = logicNOT;
            }
            notNode.id = id;
            id++;
            nodes.Add(notNode);
            return notNode;
        }
        
        public ConditionNode CreateConditionNode(Vector2 mousePos, LogicCondition lCondition = null)
        {
            GameObject goConditions;
            if (logicMap.transform.Find("Conditions") == null)
            {
                goConditions = new GameObject("Conditions");
                goConditions.transform.parent = logicMap.transform;
            }
            else
            {
                goConditions = logicMap.transform.Find("Conditions").gameObject;
            }
            ConditionNode conditionNode = new ConditionNode();
            if(lCondition == null)
            {
                GameObject logicCondition = new GameObject(string.Format("{0}_Condition", id));
                conditionNode.logicCondition = logicCondition.AddComponent<LogicCondition>();
                GameObject condition = new GameObject("Condition");
                condition.transform.parent = logicCondition.transform;
                conditionNode.logicCondition.condition = condition.AddComponent<Condition>();
                logicCondition.transform.parent = goConditions.transform;
                logicMap.conditions.Add(conditionNode.logicCondition);
                conditionNode.logicCondition.windowRect = new Rect(mousePos, new Vector2(240, 160));
            }
            else
            {
                conditionNode.logicCondition = lCondition;
            }
            conditionNode.id = id;
            id++;
            nodes.Add(conditionNode);
            return conditionNode;
        }

        public EffectNode CreateEffectNode(Vector2 mousePos, LogicEffect lEffect = null)
        {
            GameObject goEffects;
            if (logicMap.transform.Find("Effects") == null)
            {
                goEffects = new GameObject("Effects");
                goEffects.transform.parent = logicMap.transform;
            }
            else
            {
                goEffects = logicMap.transform.Find("Effects").gameObject;
            }
            EffectNode effectNode = new EffectNode();
            if(lEffect == null)
            {
                GameObject logicEffect = new GameObject(string.Format("{0}_Effect", id));
                effectNode.logicFunction = logicEffect.AddComponent<LogicEffect>();
                GameObject effect = new GameObject("Effect");
                effect.transform.parent = logicEffect.transform;
                ((LogicEffect)effectNode.logicFunction).effect = effect.AddComponent<Effect>();
                logicEffect.transform.parent = goEffects.transform;
                logicMap.effects.Add((LogicEffect)effectNode.logicFunction);
                effectNode.logicFunction.windowRect = new Rect(mousePos, new Vector2(280, 140));
            }
            else
            {
                effectNode.logicFunction = lEffect;
            }
            effectNode.id = id;
            id++;
            nodes.Add(effectNode);
            if (logicMap.startFunction == null)
            {
                SetStartFunction(effectNode.logicFunction);
            }
            return effectNode;
        }

        private DataVariableNode CreateVariableNode(Vector2 mousePos, DataVariable variable = null)
        {
            GameObject goVariables;
            if (logicMap.transform.Find("DataVariable") == null)
            {
                goVariables = new GameObject("DataVariable");
                goVariables.transform.parent = logicMap.transform;
            }
            else
            {
                goVariables = logicMap.transform.Find("DataVariable").gameObject;
            }
            DataVariableNode variableNode = new DataVariableNode();
            if(variable == null)
            {
                GameObject dataVariable = new GameObject(string.Format("{0}_DataVariable", id));
                variableNode.dataVariable = dataVariable.AddComponent<DataVariable>();
                dataVariable.transform.parent = goVariables.transform;
                logicMap.dataVariables.Add((DataVariable)variableNode.dataVariable);
                ((DataVariable)variableNode.dataVariable).windowRect = new Rect(mousePos, new Vector2(100, 60));
            }
            else
            {
                variableNode.dataVariable = variable;
            }
            variableNode.id = id;
            id++;
            nodes.Add(variableNode);
            return variableNode;
        }

        private void CommonGenericMenu()
        {
            Event e = Event.current;
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Create AND Node"), false, ContextCallback, "andNode");
            menu.AddItem(new GUIContent("Create OR Node"), false, ContextCallback, "orNode");
            menu.AddItem(new GUIContent("Create NOT Node"), false, ContextCallback, "notNode");
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Create Logic Splitter Node"), false, ContextCallback, "logicSplitterNode");
            menu.AddItem(new GUIContent("Create Data Splitter Node"), false, ContextCallback, "dataSplitterNode");
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Create Variable Node"), false, ContextCallback, "variableNode");
            menu.AddItem(new GUIContent("Create Condition Node"), false, ContextCallback, "conditionNode");
            menu.AddItem(new GUIContent("Create Effect Node"), false, ContextCallback, "effectNode");
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Create Wait Node"), false, ContextCallback, "waitNode");
            menu.AddSeparator("");
            // menu.AddItem(new GUIContent("Save Logic Map"), false, ContextCallback, "save");
            menu.AddItem(new GUIContent("Test Logic Map"), false, ContextCallback, "test");
            menu.ShowAsContext();
            e.Use();
        }

        private void LogicDataNodeGenericMenu()
        {
            Event e = Event.current;
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Set Data Output"), false, ContextCallback, "dataOutput");
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, "deleteNode");
            menu.ShowAsContext();
            e.Use();
        }

        private void LogicSplitterNodeGenericMenu()
        {
            Event e = Event.current;
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add Action Output"), false, ContextCallback, "actionOutput");
            foreach (BaseLogicNode node in nodes)
            {
                if (node is LogicSplitterNode && ((LogicSplitterNode)node).logicFunction.windowRect.Contains(mousePos))
                {
                    LogicSplitterNode logicSplitterNode = node as LogicSplitterNode;
                    LogicSplitter logicSplitter = ((LogicSplitterNode)node).logicFunction as LogicSplitter;
                    if (logicSplitter.actionOutputs.Count > 0)
                    {
                        Rect rect = new Rect(logicSplitter.windowRect.xMax - 20, logicSplitter.windowRect.y + 20, 20, logicSplitter.windowRect.height);
                        if (rect.Contains(mousePos))
                        {
                            menu.AddItem(new GUIContent("Reset Action Output"), false, ContextCallback, "resetActionOutput");
                        }
                    }
                    break;
                }
            }
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, "deleteNode");
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Set Start Node"), false, ContextCallback, "setStartNode");
            menu.ShowAsContext();
            e.Use();
        }

        private void DataSplitterNodeGenericMenu()
        {
            Event e = Event.current;
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Set True Output"), false, ContextCallback, "trueOutput");
            menu.AddItem(new GUIContent("Set False Output"), false, ContextCallback, "falseOutput");
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, "deleteNode");
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Set Start Node"), false, ContextCallback, "setStartNode");
            menu.ShowAsContext();
            e.Use();
        }

        private void DataNodeGenericMenu()
        {
            Event e = Event.current;
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Set Data Output"), false, ContextCallback, "dataOutput");
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, "deleteNode");
            menu.ShowAsContext();
            e.Use();
        }

        private void EffectNodeGenericMenu()
        {
            Event e = Event.current;
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, "deleteNode");
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Set Start Node"), false, ContextCallback, "setStartNode");
            menu.ShowAsContext();
            e.Use();
        }

        private void WaitNodeGenericMenu()
        {
            Event e = Event.current;
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Set Output"), false, ContextCallback, "actionOutput");
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, "deleteNode");
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Set Start Node"), false, ContextCallback, "setStartNode");
            menu.ShowAsContext();
            e.Use();
        }

        private void EnterGenericMenu()
        {
            Event e = Event.current;
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Set Start Node"), false, ContextCallback, "resetStartNode");
            menu.ShowAsContext();
            e.Use();
        }

        private void SetStartFunction(LogicFunction logicFunction)
        {
            if(logicMap.startFunction != null)
            {
                logicMap.startFunction.startFunction = false;
            }
            logicMap.startFunction = logicFunction;
            if(logicFunction != null)
            {
                logicFunction.startFunction = true;
                logicFunction.SetActionInputLink(null);
            }
        }

        private void ResetStartFunction()
        {
            enterNode.selectLink = true;
            actionLinkMod = true;
            SetStartFunction(null);
        }

        private void TestLogicMap()
        {
            logicMap.RealizeLogicMap(null, true);
        }

        //private void SavePrefab()
        //{
        //    if (!Directory.Exists(pathToSave))
        //    {
        //        Directory.CreateDirectory(pathToSave);
        //    }
        //    string path = "";
        //    if (prefab == null)
        //    {
        //        path = EditorUtility.SaveFilePanelInProject("Save Logic Map", logicMap.name, "prefab", "Select path to save", pathToSave);
        //    }
        //    else
        //    {
        //        path = EditorUtility.SaveFilePanelInProject("Save Logic Map", prefab.name, "prefab", "Select path to save", pathToSave);
        //    }
        //    if(path != "")
        //    {
        //        prefab = PrefabUtility.CreatePrefab(path, logicMap.gameObject);
        //        PrefabUtility.ConnectGameObjectToPrefab(logicMap.gameObject, prefab);
        //    }
        //}

        private void LoadLogicMap()
        {
            ClearAll();
            enterNode = new EnterNode(logicMap.enterWindowRect.position);
            foreach(LogicCondition function in logicMap.conditions)
            {
                CreateConditionNode(function.windowRect.position, function);
            }
            foreach (LogicDataFunction function in logicMap.dataFunc)
            {
                if(function is LogicAND)
                {
                    CreateAndNode(function.windowRect.position, (LogicAND)function);
                }
                else if(function is LogicOR)
                {
                    CreateOrNode(function.windowRect.position, (LogicOR)function);
                }
                else if (function is LogicNOT)
                {
                    CreateNotNode(function.windowRect.position, (LogicNOT)function);
                }
            }
            foreach (DataVariable function in logicMap.dataVariables)
            {
                CreateVariableNode(function.windowRect.position, function);
            }
            foreach (LogicEffect function in logicMap.effects)
            {
                CreateEffectNode(function.windowRect.position, function);
            }
            foreach (LogicFunction function in logicMap.logicFunc)
            {
                if(function is LogicSplitter)
                {
                    CreateLogicSplitterNode(function.windowRect.position, (LogicSplitter)function);
                }
                else if (function is DataSplitter)
                {
                    CreateDataSplitterNode(function.windowRect.position, (DataSplitter)function);
                }
                else if (function is WaitFunction)
                {
                    CreateWaitNode(function.windowRect.position, (WaitFunction)function);
                }
            }
            if (PrefabUtility.GetPrefabParent(logicMap) != null)
            {
                prefab = PrefabUtility.GetPrefabParent(logicMap) as GameObject;
            }
        }

        public void ClearAll()
        {
            nodes.Clear();
            id = 0;
            moveMod = false;
            actionLinkMod = false;
            dataLinkMod = false;
        }
    }
}