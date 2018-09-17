using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(Agency))]
    public class eAgency : Editor
    {
        private bool showStates;

        public override void OnInspectorGUI()
        {
            Agency agency = (Agency)target;
            base.OnInspectorGUI();
            GUILayout.Space(10);
            eUtils.DrawQuestStateList(agency.globalStates, agency.transform, ref showStates, "Global States");
        }
    }
}