using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public class LogicEffect : LogicFunction
    {
        public Effect effect;
        public bool checkNode;

        public override void RemoveActionInput(LogicFunction logicFunction)
        {
            for (int i = 0; i < actionInputs.Count; i++)
            {
                if (actionInputs[i] == logicFunction)
                {
                    actionInputs.RemoveAt(i);
                    break;
                }
            }
        }

        public override void RemoveActionOutput(LogicFunction logicFunction)
        {
        }

        public void Realize(iLogicMapContainer owner, bool isTest = false)
        {
            checkNode = true;
            if (!isTest)
            {
                //effect.owner = owner;
                effect.Realize(owner);
                //if (owner is TriggerZone)
                //{
                //    TriggerZone triggerZone = owner as TriggerZone;
                //    triggerZone.Reactivate();
                //}
            }
            else
            {
                Debug.Log(string.Format("Realize effect {0}", effect.type));
            }
        }
    }
}