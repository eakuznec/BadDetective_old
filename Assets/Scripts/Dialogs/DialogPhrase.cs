﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.Dialog
{
    public class DialogPhrase : MonoBehaviour
    {
        public DialogSpeekerType speekerType;
        public Character speeker;
        public string phraseText;
        public List<DialogChoose> chooses = new List<DialogChoose>();
        [HideInInspector]
        public List<Effect> effects = new List<Effect>();
        [HideInInspector]
        public Rect nodePosition;
    }
}
