using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.LogicMap
{
    public abstract class LogicFunctionNode : BaseLogicNode
    {
        public LogicFunction logicFunction;

        public bool selectActionLink;
    }
}
