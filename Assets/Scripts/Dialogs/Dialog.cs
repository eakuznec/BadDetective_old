using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.Dialog
{
    public class Dialog : MonoBehaviour, iEffectsContainer, iConditionContainer
    {
        public string dialogName;
        public string pathToSave;
        public DialogPhrase startPhrase;
        public List<DialogPhrase> phrases = new List<DialogPhrase>();
        [HideInInspector]
        public List<QuestState> dialogStates = new List<QuestState>();
        [HideInInspector] public Character owner;
        [HideInInspector] public Team teamOwner;
        [HideInInspector] public Quest questOwner;
        [HideInInspector] public DialogChoose end;

        public Character GetCharacterOwner()
        {
            if (teamOwner == null)
            {
                return owner;
            }
            else
            {
                return teamOwner.detectives[0];
            }
        }

        public Team GetTeam()
        {
            return teamOwner;
        }

        public Quest GetQuest()
        {
            return questOwner;
        }

        public Dialog GetDialog()
        {
            return this;
        }

        public List<DialogChoose> GetEnds()
        {
            List<DialogChoose> retVal = new List<DialogChoose>();
            foreach(DialogPhrase phrase in phrases)
            {
                foreach(DialogChoose choose in phrase.chooses)
                {
                    if(choose.type == ChooseType.END)
                    {
                        retVal.Add(choose);
                    }
                }
            }
            return retVal;
        }

        public List<QuestState> GetDialogStates()
        {
            return dialogStates;
        }

        public List<string> GetDialogStateNames()
        {
            List<string> retVal = new List<string>();
            foreach(QuestState state in dialogStates)
            {
                if(state != null)
                {
                    retVal.Add(state.stateName);
                }
                else
                {
                    retVal.Add("-NULL-");
                }
            }
            return retVal;
        }
    }
}
