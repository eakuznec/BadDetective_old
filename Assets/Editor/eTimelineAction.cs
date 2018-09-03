using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor (typeof(TimelineAction))]
    public class eTimelineAction : Editor
    {
        public override void OnInspectorGUI()
        {
            TimelineAction action = (TimelineAction)target;
            base.OnInspectorGUI();
            eUtils.DrawTimeInspector(action.timer);
            eUtils.DrawTimeSelector(ref action.timer);
            EditorUtility.SetDirty(action);
        }
    }
}
