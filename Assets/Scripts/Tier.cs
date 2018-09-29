using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Tier : MonoBehaviour
    {
        public string tierName;
        [SerializeField]
        private List<PointOnMap> allPoints = new List<PointOnMap>();

        private float curPosition;
        private float timePos;
        private bool changePos;

        private void Update()
        {
            if (changePos)
            {
                MapManager mapManager = MapManager.GetInstantiate();
                timePos += Time.deltaTime;
                Quaternion quaternion = GetComponent<Transform>().rotation;
                float t = timePos / mapManager.timePosTier;
                if (t > 1)
                {
                    t = 1;
                }
                GetComponent<Transform>().SetPositionAndRotation(new Vector3(0, Mathf.Lerp(transform.position.y, curPosition, t), 0), quaternion);
                if (t == 1)
                {
                    changePos = false;
                }
            }
        }

        public List<PointOnMap> GetPoints()
        {
            return allPoints;
        }

        public List<string> GetPointNames()
        {
            List<string> retVal = new List<string>();
            foreach (PointOnMap point in allPoints)
            {
                if (point.pointName == "")
                {
                    retVal.Add(point.name);
                }
                else
                {
                    retVal.Add(point.pointName);
                }
            }
            return retVal;
        }

        public void SetPosition(float position)
        {
            MapManager mapManager = MapManager.GetInstantiate();
            if(Mathf.Abs(position - curPosition) > mapManager.deltaPosTier)
            {
                curPosition = position;
                changePos = true;
                timePos = 0;
            }
        }
    }
}
