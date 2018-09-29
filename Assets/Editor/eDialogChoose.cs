using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.Dialog
{
    [CustomEditor (typeof(DialogChoose))]
    public class eDialogChoose : Editor
    {
        bool showConditions = false;
        bool showEffects = false;

        public override void OnInspectorGUI()
        {
            DialogChoose choose = (DialogChoose)target;
            base.OnInspectorGUI();
            eUtils.DrawConditionsSelector(choose.conditions, choose.transform, ref showConditions);
            eUtils.DrawEffectsSelector(choose.effects, choose.transform, ref showEffects);
        }
    }
}
