using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    [CustomEditor(typeof(PointOnMap))]
    public class ePointOnMap : Editor
    {
        Color pointLight = new Color(1, 0f, 0, 1);

        private void OnSceneGUI()
        {
            PointOnMap point = target as PointOnMap;

            for (int i = 0; i < 5; i++)
            {
                Handles.color = new Color(pointLight.r, pointLight.g, pointLight.b, 0.06f * (i + 1));
                Handles.SphereHandleCap(0, point.transform.position, point.transform.rotation, 0.3f - 0.03f * i, EventType.Repaint);
            }
            //Handles.color = Color.red;
            //Handles.SphereHandleCap(0, point.transform.position, point.transform.rotation, 0.1f, EventType.Repaint);
        }
    }
}
