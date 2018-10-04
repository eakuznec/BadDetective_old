using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Office : MonoBehaviour, iActivityPlace
    {
        [TextArea]
        public string officeDescription;
        [HideInInspector]
        public Tier tier;
        [HideInInspector]
        public PointOnMap point;
        public Money cost;
        public Money lease;
        public float stressPerHour;
        [Header ("Detectives")]
        public List<Detective> detectivesInOffice = new List<Detective>();

        public string GetPlaceName()
        {
            return string.Format("Office on {0}", point.pointName);
        }

        public PointOnMap GetPoint()
        {
            return point;
        }

        public float GetStressPerHour()
        {
            return stressPerHour;
        }

        private void Start()
        {
            MapManager mapManager = MapManager.GetInstantiate();
            for(int i=0; i<mapManager.ring.GetTiers().Count; i++)
            {
                for(int j=0; j< mapManager.ring.GetTiers()[i].GetPoints().Count; j++)
                {
                    if(point == mapManager.ring.GetTiers()[i].GetPoints()[j])
                    {
                        point = mapManager.instanceRing.GetTiers()[i].GetPoints()[j];
                        break;
                    }
                }
            }
            mapManager.SetTier(mapManager.GetTierIndex(point));
            point.pointContainer.Add(this);
            //
            CreateLeaseAction();
        }

        public void CreateLeaseAction()
        {
            Timeline timeline = Timeline.GetInstantiate();
            GameObject goAction = new GameObject(string.Format("TimelineAction_OffiseLease"));
            goAction.transform.parent = timeline.transform;
            TimelineAction leaseAction = goAction.AddComponent<TimelineAction>();
            leaseAction.actionType = TimelineActionType.OFFICE_LEASE;
            leaseAction.office = this;
            leaseAction.timer = timeline.GetTime() + 24 * 7 * 30; //выплата через месяц.
            timeline.RegistrateAction(leaseAction);
        }
    }
}

