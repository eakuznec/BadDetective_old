using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public class EnterNode : ScriptableObject
    {
        public string windowTitle = "Enter";
        //Link
        public bool selectLink;
        private BaseLogicNode startLogicNode;

        public EnterNode(Vector2 startPos)
        {
            LogicMapEditor.logicMap.enterWindowRect = new Rect(startPos, new Vector2(100, 40));
        }

        public virtual void DrawLink()
        {
            if (LogicMapEditor.logicMap != null)
            {
                if (LogicMapEditor.logicMap.startFunction != null)
                {
                    foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                    {
                        if (node is LogicFunctionNode)
                        {
                            if (LogicMapEditor.logicMap.startFunction == ((LogicFunctionNode)node).logicFunction)
                            {
                                startLogicNode = node;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    startLogicNode = null;
                }
                Rect windowRect = LogicMapEditor.logicMap.enterWindowRect;
                Vector2 startPos;
                Vector2 endPos;
                if (startLogicNode != null)
                {
                    startPos = new Vector2(windowRect.xMax, windowRect.y + windowRect.height / 2);
                    endPos = new Vector2(((LogicFunctionNode)startLogicNode).logicFunction.GetWindowRect().xMin, ((LogicFunctionNode)startLogicNode).logicFunction.GetWindowRect().y + 25);

                    Vector2 startTan = startPos + Vector2.right * 50;
                    Vector2 endTan = endPos + Vector2.left * 50;
                    Color activeColor = Color.blue;
                    Color backColor = Color.black;
                    if (LogicMapEditor.logicMap.startRealize)
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
                else if (selectLink)
                {
                    Event e = Event.current;
                    startPos = new Vector2(windowRect.xMax, windowRect.y + windowRect.height / 2);
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

        public void DrawWindow()
        {
            GUILayout.Label("Start Action");
        }
    }
}
