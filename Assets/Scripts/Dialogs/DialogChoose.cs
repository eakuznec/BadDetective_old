using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.Dialog
{
    public class DialogChoose : MonoBehaviour
    {
        public string chooseText;
        public bool next;
        [HideInInspector]
        public List<Condition> conditions = new List<Condition>();
        [HideInInspector]
        public List<Effect> effects = new List<Effect>();
        public List<DialogLink> links = new List<DialogLink>();
        [HideInInspector]
        public Rect nodePosition;
    }
}
