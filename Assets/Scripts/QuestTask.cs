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
        public bool showTask;
        private Team curTeam;
        [HideInInspector]
        public LogicMap.LogicMap startLogicMap;
        [HideInInspector]
        public List<LogicMap.LogicMap> logicMaps = new List<LogicMap.LogicMap>();

        public void ChangeMainState(MainState newState)
        {
            mainState = newState;
        }

        public void FinalizeTask()
        {
            Agency agency = Agency.GetInstantiate();
            Dialog.DialogManager dialogManager = Dialog.DialogManager.GetInstantiate();
            curTeam.targetTasks.Remove(this);
            if (curTeam.targetTasks.Count == 0)
            {
                dialogManager.StartDialog(dialogManager.endEventDialog, curTeam, GetQuest());
            }
            else
            {
                curTeam.StartTask();
            }
            curTeam = null;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void Realize(Team team)
        {
            if (!team.targetTasks.Contains(this))
            {
                team.targetTasks.Add(this);
            }
            team.curTask = this;
            curTeam = team;
            startLogicMap.RealizeLogicMap(this);
        }

        public Team GetTeam()
        {
            return curTeam;
        }

        public Character GetCharacterOwner()
        {
            return curTeam.detectives[0];
        }

        public List<LogicMap.LogicMap> GetLogicMaps()
        {
            return logicMaps;
        }

        public List<string> GetLogicMapNames()
        {
            List<string> retVal = new List<string>();
            foreach (LogicMap.LogicMap logiMap in logicMaps)
            {
                if (logiMap != null)
                {
                    retVal.Add(logiMap.logicMapName);
                }
                else
                {
                    retVal.Add("-NULL-");
                }
            }
            return retVal;
        }

        public Quest GetQuest()
        {
            return transform.GetComponentInParent<Quest>();
        }

        public Dialog.Dialog GetDialog()
        {
            return null;
        }
    }
}
