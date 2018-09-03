using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective.Dialog
{
    [CustomEditor(typeof(DialogLink))]
    public class eDialogLink : Editor
    {
        bool showConditions = false;

        public override void OnInspectorGUI()
        {
            DialogLink link = (DialogLink)target;
            base.OnInspectorGUI();
            eUtils.DrawConditionsSelector(link.conditions, link.transform, ref showConditions);
        }
    }
}