using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Path : MonoBehaviour, iWayPlace
    {
        public string pathName;
        [SerializeField]
        private WayType type;
        public PointOnMap[] points = new PointOnMap[2];
        public float timeToWay;
        public float stressPerHour;

        public WayType GetWayType()
        {
            return type;
        }

        private void OnDrawGizmos()
        {
            if(type == WayType.MAINSTREET)
            {
                Gizmos.color = Color.blue;
            }
            else if (type == WayType.TRANSPORT)
            {
                Gizmos.color = new Color(0, 0.5f, 1);
            }
            else if (type == WayType.BACKSTREET)
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawLine(points[0].transform.position, points[1].transform.position);
        }

        public string GetPlaceName()
        {
            return pathName;
        }

        public float GetStressPerHour()
        {
            return stressPerHour;
        }

        public PointOnMap GetPoint()
        {
            return points[Random.Range(0, 1)];
        }

        public float GetTimeToWay()
        {
            return timeToWay;
        }

        public Vector3 GetPointOnPath(PointOnMap end, float part)
        {
            Vector3 startPoint;
            if (points[0] == end)
            {
                startPoint = points[1].transform.position;
            }
            else
            {
                startPoint = points[0].transform.position;
            }
            Vector3 endPoint = end.transform.position;
            float x = (endPoint.x - startPoint.x) * part;
            float y = (endPoint.y - startPoint.y) * part;
            float z = (endPoint.z - startPoint.z) * part;
            return startPoint + new Vector3(x, y, z);
        }
    }

    public enum WayType
    {
        MAINSTREET,
        TRANSPORT,
        BACKSTREET
    }
}