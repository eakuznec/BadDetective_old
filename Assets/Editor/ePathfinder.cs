using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor (typeof(Pathfinder))]
    public class ePathfinder : Editor
    {
        PointOnMap startPoint;
        PointOnMap endPoint;
        WayType type;
        Way testWay;

        public override void OnInspectorGUI()
        {
            Pathfinder pathfinder = target as Pathfinder;
            EditorGUILayout.LabelField("Test");
            type = (WayType)EditorGUILayout.EnumPopup(type);
            startPoint = EditorGUILayout.ObjectField(startPoint, typeof(PointOnMap)) as PointOnMap;
            endPoint = EditorGUILayout.ObjectField(endPoint, typeof(PointOnMap)) as PointOnMap;
            if(GUILayout.Button("Check Way"))
            {
                if(startPoint != null && endPoint != null)
                {
                    testWay = pathfinder.GetWay(type, startPoint, endPoint, pathfinder.transform);
                    
                    if(testWay != null)
                    {
                        foreach (iWayPlace pnp in testWay.pointsAndPaths)
                        {
                            if(pnp is Path)
                            {
                                Debug.Log(string.Format("{0}", ((Path)pnp).name));
                            }
                            else if(pnp is PointOnMap)
                            {
                                Debug.Log(string.Format("{0}: {1}", ((PointOnMap)pnp).name, ((PointOnMap)pnp).shortWay.totalTime));
                            }
                        }
                    }
                }
            }
        }
    }
}