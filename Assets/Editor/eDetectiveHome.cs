using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(DetectiveHome))]
    public class eDetectiveHome : Editor
    {
        public override void OnInspectorGUI()
        {
            DetectiveHome home = (DetectiveHome)target;
            base.OnInspectorGUI();
            int count = EditorGUILayout.IntField(home.potentialPoints.Count);
            int dif = count - home.potentialPoints.Count;
            if (dif > 0)
            {
                for(int i = 0; i < dif; i++)
                {
                    home.potentialPoints.Add(null);
                    home.potencialTiers.Add(null);
                }
            }
            else if (dif < 0)
            {
                for(int i = 0; i < -dif; i++)
                {
                    home.potentialPoints.RemoveAt(home.potentialPoints.Count - i - 1);
                    home.potencialTiers.RemoveAt(home.potentialPoints.Count - i - 1);
                }
            }
            for(int i=0;i<home.potentialPoints.Count;i++)
            {
                Tier tier = home.potencialTiers[i];
                PointOnMap point = home.potentialPoints[i];
                eUtils.DrawPointOnMapSelector(ref tier, ref point);
                home.potentialPoints[i] = point;
                home.potencialTiers[i] = tier;
                GUILayout.Box("", new GUILayoutOption[] { GUILayout.Height(1), GUILayout.ExpandWidth(true) });
            }
            EditorUtility.SetDirty(home);
        }
    }

}
