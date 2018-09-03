using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public abstract class BaseLogicNode : ScriptableObject
    {
        public string windowTitle = "";
        public int id;

        public abstract void DrawWindow();

        public abstract void DrawLinks();

        public virtual void DeleteNode()
        {
            List<BaseLogicNode> nodes = LogicMapEditor.editor.nodes;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] == this)
                {
                    LogicMapEditor.editor.nodes.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
