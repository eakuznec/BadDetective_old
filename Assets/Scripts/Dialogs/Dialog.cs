using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.Dialog
{
    public class Dialog : MonoBehaviour, LogicMap.iLogicMapContainer
    {
        public string dialogName;
        public string pathToSave;
        public DialogPhrase startPhrase;
        public List<DialogPhrase> phrases = new List<DialogPhrase>();
        [HideInInspector]
        public Character owner;

        public Transform GetTransform()
        {
            return transform;
        }
    }
}
