using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class DetectiveHome : MonoBehaviour, iActivityPlace
    {
        [HideInInspector]
        public List<Tier> potencialTiers = new List<Tier>();
        [HideInInspector]
        public List<PointOnMap> potentialPoints = new List<PointOnMap>();
        public PointOnMap point;
        public float stressPerHour = -1.2f;
        [HideInInspector]
        public Detective owner;
        public bool characterInHome;

        private void Start()
        {
            MapManager mapManager = MapManager.GetInstantiate();
            point = potentialPoints[Random.Range(0, potentialPoints.Count)];
            for (int i = 0; i < mapManager.ring.GetTiers().Count; i++)
            {
                for (int j = 0; j < mapManager.ring.GetTiers()[i].GetPoints().Count; j++)
                {
                    if (point == mapManager.ring.GetTiers()[i].GetPoints()[j])
                    {
                        point = mapManager.instanceRing.GetTiers()[i].GetPoints()[j];
                        break;
                    }
                }
            }
            mapManager.SetTier(mapManager.GetTierIndex(point));
            point.pointContainer.Add(this);
        }

        public string GetPlaceName()
        {
            return string.Format("Home on {0}", point.pointName);
        }

        public float GetStressPerHour()
        {
            return stressPerHour;
        }

        public PointOnMap GetPoint()
        {
            return point;
        }
    }
}
