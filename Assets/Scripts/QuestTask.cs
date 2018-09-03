using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class QuestTask : MonoBehaviour, LogicMap.iLogicMapContainer
    {
        public string taskName;
        [TextArea]
        public string taskDescription;
        public bool isMain;
        public MainState mainState;
        private Team curTeam;
        [HideInInspector]
        public LogicMap.LogicMap logicMap;

        public void ChangeMainState(MainState newState)
        {
            Agency agency = Agency.GetInstantiate();
            mainState = newState;
            if(mainState == MainState.Completed || mainState == MainState.Failed)
            {
                curTeam.targetTasks.Remove(this);
                if (curTeam.targetTasks.Count == 0)
                {
                    curTeam.GoTo(agency.GetOffice(), curTeam.GetPriorityWay(), true);
                }
                curTeam = null;
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void Realize(Team team)
        {
            curTeam = team;
            logicMap.RealizeLogicMap(this);
        }

        public Team GetTeam()
        {
            return curTeam;
        }
    }
}
