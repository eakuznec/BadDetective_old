using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public class ChooseTemperFunction : LogicFunction
    {
        public int temper;
        public LogicFunction rudeOutput;
        public bool realizeRude;
        public LogicFunction prudentOutput;
        public bool realizePrudent;
        public LogicFunction mercifulOutput;
        public bool realizeMerciful;
        public LogicFunction cruelOutput;
        public bool realizeCruel;
        public LogicFunction mercantileOutput;
        public bool realizeMercantile;
        public LogicFunction principledOutput;
        public bool realizePrincipled;

        public Dialog.Dialog dialog;
        public List<LogicFunction> dialogOutputs = new List<LogicFunction>();
        public List<bool> realizeDialogOutput = new List<bool>();
        public int dialogOutputNum;
        public bool dialogOutputFlag;

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
            if (rudeOutput == logicFunction && prudentOutput != logicFunction && mercifulOutput != logicFunction && cruelOutput != logicFunction && mercantileOutput != logicFunction && principledOutput !=logicFunction)
            {
                rudeOutput = null;
            }
            else if (rudeOutput != logicFunction && prudentOutput == logicFunction && mercifulOutput != logicFunction && cruelOutput != logicFunction && mercantileOutput != logicFunction && principledOutput != logicFunction)
            {
                prudentOutput = null;
            }
            else if (rudeOutput != logicFunction && prudentOutput != logicFunction && mercifulOutput == logicFunction && cruelOutput != logicFunction && mercantileOutput != logicFunction && principledOutput != logicFunction)
            {
                mercifulOutput = null;
            }
            else if (rudeOutput != logicFunction && prudentOutput != logicFunction && mercifulOutput != logicFunction && cruelOutput == logicFunction && mercantileOutput != logicFunction && principledOutput != logicFunction)
            {
                cruelOutput = null;
            }
            else if (rudeOutput != logicFunction && prudentOutput != logicFunction && mercifulOutput != logicFunction && cruelOutput != logicFunction && mercantileOutput == logicFunction && principledOutput != logicFunction)
            {
                mercantileOutput = null;
            }
            else if (rudeOutput != logicFunction && prudentOutput != logicFunction && mercifulOutput != logicFunction && cruelOutput != logicFunction && mercantileOutput != logicFunction && principledOutput == logicFunction)
            {
                principledOutput = null;
            }

            else
            {
                if (rudeOutput == logicFunction && temper != 0)
                {
                    rudeOutput = null;
                }
                if (prudentOutput == logicFunction && temper != 1)
                {
                    prudentOutput = null;
                }
                if (mercifulOutput == logicFunction && temper != 2)
                {
                    mercifulOutput = null;
                }
                if (cruelOutput == logicFunction && temper != 3)
                {
                    cruelOutput = null;
                }
                if (mercantileOutput == logicFunction && temper != 4)
                {
                    cruelOutput = null;
                }
                if (principledOutput == logicFunction && temper != 5)
                {
                    cruelOutput = null;
                }
            }
            for (int i = 0; i < dialogOutputs.Count; i++)
            {
                if (dialogOutputs[i] == logicFunction)
                {
                    dialogOutputs[i] = null;
                }
            }
        }

        public void SetOutputLink(LogicFunction output)
        {
            if (!dialogOutputFlag)
            {
                if (temper == 0)
                {
                    if (rudeOutput != null)
                    {
                        rudeOutput.RemoveActionInput(this);
                    }
                    rudeOutput = output;
                }
                else if (temper == 1)
                {
                    if (prudentOutput != null)
                    {
                        prudentOutput.RemoveActionInput(this);
                    }
                    prudentOutput = output;
                }
                else if (temper == 2)
                {
                    if (mercifulOutput != null)
                    {
                        mercifulOutput.RemoveActionInput(this);
                    }
                    mercifulOutput = output;
                }
                else if (temper == 3)
                {
                    if (cruelOutput != null)
                    {
                        cruelOutput.RemoveActionInput(this);
                    }
                    cruelOutput = output;
                }
                else if (temper == 4)
                {
                    if (mercantileOutput != null)
                    {
                        mercantileOutput.RemoveActionInput(this);
                    }
                    mercantileOutput = output;
                }
                else if (temper == 5)
                {
                    if (principledOutput != null)
                    {
                        principledOutput.RemoveActionInput(this);
                    }
                    principledOutput = output;
                }

            }
            else
            {
                if (dialogOutputs[dialogOutputNum] != null)
                {
                    dialogOutputs[dialogOutputNum].RemoveActionInput(this);
                }
                dialogOutputs[dialogOutputNum] = output;
            }
        }

        public Temper Realize(iLogicMapContainer owner)
        {
            Team team = null;
            if (owner is QuestTask)
            {
                team = ((QuestTask)owner).GetTeam();
            }
            return team.GetPriorityTemper(rudeOutput != null, prudentOutput != null, mercifulOutput != null, cruelOutput != null, mercantileOutput != null, principledOutput != null);
        }
    }
}