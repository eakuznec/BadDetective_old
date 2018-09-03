using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.Dialog
{
    [CustomEditor (typeof(DialogPhrase))]
    public class eDialogPhrase : Editor
    {
        bool showEffects = false;

        public override void OnInspectorGUI()
        {
            DialogPhrase phrase = (DialogPhrase)target;
            base.OnInspectorGUI();
            eUtils.DrawEffectsSelector(phrase.effects, phrase.transform, ref showEffects);
        }
    }
}
