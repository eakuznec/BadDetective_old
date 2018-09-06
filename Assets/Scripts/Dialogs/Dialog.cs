using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.Dialog
{
    public class Dialog : MonoBehaviour, iEffectsContainer
    {
        public string dialogName;
        public string pathToSave;
        public DialogPhrase startPhrase;
        public List<DialogPhrase> phrases = new List<DialogPhrase>();
        [HideInInspector]
        public Character owner;
        public Team teamOwner;

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
    }
}
