using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public abstract class LogicDataFunctionNode : BaseLogicNode, iBoolDataNode
    {
        public LogicDataFunction dataFunction;
        public bool selectDataLink;
        
        public override void DrawLinks()
        {
            LogicDataFunction logicDataFunction = dataFunction as LogicDataFunction;
            Vector2 startPos;
            Vector2 endPos = Vector2.zero;
            if (logicDataFunction.dataOutput != null)
            {
                startPos = new Vector2(logicDataFunction.GetWindowRect().xMax, logicDataFunction.GetWindowRect().y + 25);
                BaseLogicNode outputNode = null;
                foreach (BaseLogicNode node in LogicMapEditor.editor.nodes)
                {
                    if (node is LogicDataFunctionNode && ((LogicDataFunctionNode)node).dataFunction == logicDataFunction.dataOutput)
                    {
                        outputNode = node;
                    }
                    else if (node is DataSplitterNode && ((DataSplitterNode)node).logicFunction == logicDataFunction.dataOutput)
                    {
                        outputNode = node;
                    }
                    else if (node is DataVariableNode && ((DataVariableNode)node).dataVariable == logicDataFunction.dataOutput)
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
                    if(((LogicAND)((LogicAndNode)outputNode).dataFunction).dataInputOne == logicDataFunction)
                    {
                        endPos = new Vector2(((LogicAndNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicAndNode)outputNode).dataFunction.GetWindowRect().y + 25);
                    }
                    if (((LogicAND)((LogicAndNode)outputNode).dataFunction).dataInputTwo == logicDataFunction)
                    {
                        endPos = new Vector2(((LogicAndNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicAndNode)outputNode).dataFunction.GetWindowRect().y + 45);
                    }
                }
                else if (outputNode is LogicOrNode)
                {
                    if (((LogicOR)((LogicOrNode)outputNode).dataFunction).dataInputOne == logicDataFunction)
                    {
                        endPos = new Vector2(((LogicOrNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicOrNode)outputNode).dataFunction.GetWindowRect().y + 25);
                    }
                    if (((LogicOR)((LogicOrNode)outputNode).dataFunction).dataInputTwo == logicDataFunction)
                    {
                        endPos = new Vector2(((LogicOrNode)outputNode).dataFunction.GetWindowRect().xMin, ((LogicOrNode)outputNode).dataFunction.GetWindowRect().y + 45);
                    }
                }
                else if (outputNode is EffectNode)
                {
                    endPos = new Vector2(((EffectNode)outputNode).logicFunction.GetWindowRect().xMin, ((EffectNode)outputNode).logicFunction.GetWindowRect().y + 25);
                }
                else if (outputNode is DataVariableNode)
                {
                    endPos = new Vector2(((DataVariable)((DataVariableNode)outputNode).dataVariable).GetWindowRect().xMin, ((DataVariable)((DataVariableNode)outputNode).dataVariable).GetWindowRect().y + 25);
                }

                Vector2 startTan = startPos + Vector2.right * 50;
                Vector2 endTan = endPos + Vector2.left * 50;
                Color activeColor = Color.black;
                Color backColor = Color.black;
                if(logicDataFunction.checkNode && logicDataFunction.result)
                {
                    activeColor = Color.green;
                }
                else if (logicDataFunction.checkNode && !logicDataFunction.result)
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
                startPos = new Vector2(logicDataFunction.GetWindowRect().xMax, logicDataFunction.GetWindowRect().y + 25);
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

        public abstract BaseFunction GetDataFunction();

        public void SetDataOutputLink()
        {
            if(dataFunction.dataOutput != null)
            {
                if (dataFunction.dataOutput is LogicDataFunction)
                {
                    ((LogicDataFunction)dataFunction.dataOutput).RemoveDataInput(dataFunction);
                }
                else if (dataFunction.dataOutput is DataVariable)
                {
                    ((DataVariable)dataFunction.dataOutput).RemoveDataInput(dataFunction);
                }
                else if (dataFunction.dataOutput is DataSplitter)
                {
                    ((DataSplitter)dataFunction.dataOutput).RemoveDataInput(dataFunction);
                }
            }
            dataFunction.SetDataOutputLink(null);
            selectDataLink = true;
            LogicMapEditor.editor.selectDataNode = this;
        }
      
        public bool GetSelectDataLink()
        {
            return selectDataLink;
        }

        public void SetSelectDataLink(bool link)
        {
            selectDataLink = link;
        }
    }
}
