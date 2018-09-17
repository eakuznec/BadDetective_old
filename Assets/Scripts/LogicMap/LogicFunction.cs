using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public abstract class LogicFunction : BaseFunction, iHaveNode
    {
        public List<LogicFunction> actionInputs = new List<LogicFunction>();
        public bool startFunction;

        public abstract void RemoveActionInput(LogicFunction logicFunction);
        public abstract void RemoveActionOutput(LogicFunction logicFunction);

        public Rect windowRect;

        public void SetActionInputLink(LogicFunction input)
        {
            if (!actionInputs.Contains(input))
            {
                actionInputs.Add(input);
            }
        }

        public Rect GetWindowRect()
        {
            return windowRect;
        }

        public void SetWindowRect(Rect rect)
        {
            windowRect = rect;
        }
    }
}
