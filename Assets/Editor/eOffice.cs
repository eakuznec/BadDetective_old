using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor (typeof (Office))]
    public class eOffice : Editor
    {
        public override void OnInspectorGUI()
        {
            Office office = (Office)target;
            base.OnInspectorGUI();
            eUtils.DrawPointOnMapSelector(ref office.tier, ref office.point);
            EditorUtility.SetDirty(office);
        }
    }
}
