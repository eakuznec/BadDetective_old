using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class QuestEvent : MonoBehaviour, iActivityPlace
    {
        public string eventName;
        [TextArea]
        public string eventDescription;
        public float stressPerHour;
        [HideInInspector]
        public Tier tier;
        public PointOnMap point;
        public List<QuestTask> tasks = new List<QuestTask>();
        public List<Detective> detectivesOnEvent = new List<Detective>();
        public List<Detective> potencialDetectivesOnEvent = new List<Detective>();
        public List<Detective> plannedDetectivesOnEvent = new List<Detective>();

        private Team teamOnEvent;

        private void Awake()
        {
            MapManager mapManager = MapManager.GetInstantiate();
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
        }

        public string GetPlaceName()
        {
            return eventName;
        }

        public PointOnMap GetPoint()
        {
            return point;
        }

        public float GetStressPerHour()
        {
            return stressPerHour;
        }

        public void ChangeTask(QuestTask curTask, MainState state)
        {
            curTask.ChangeMainState(state);
            if(state == MainState.Started)
            {
                if (!point.pointContainer.Contains(this))
                {
                    point.pointContainer.Add(this);
                }
            }
            else if (state == MainState.Completed || state == MainState.Failed)
            {
                bool flag = false;
                foreach (QuestTask task in tasks)
                {
                    if (task.mainState == MainState.Started)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    point.pointContainer.Remove(this);
                }
            }
        }

        public List<QuestTask> GetUncompletedTasks()
        {
            List<QuestTask> retVal = new List<QuestTask>();
            foreach(QuestTask task in GetTask())
            {
                if(task.mainState == MainState.Started)
                {
                    retVal.Add(task);
                }
            }
            return retVal;
        }

        public List<QuestTask> GetTask()
        {
            return tasks;
        }

        public string[] GetTaskNames()
        {
            List<string> retVal = new List<string>();
            foreach(QuestTask task in tasks)
            {
                retVal.Add(task.taskName);
            }
            return retVal.ToArray();
        }

        public int GetAllDetectivesCount()
        {
            return detectivesOnEvent.Count + plannedDetectivesOnEvent.Count + potencialDetectivesOnEvent.Count;
        }

        public void AddTeam(Team newTeam)
        {
            if(teamOnEvent == null)
            {
                teamOnEvent = newTeam;
            }
            else if (teamOnEvent != newTeam)
            {
                foreach (Detective detective in newTeam.detectives)
                {
                    if (!teamOnEvent.detectives.Contains(detective))
                    {
                        teamOnEvent.detectives.Add(detective);
                    }
                }
                foreach (QuestTask task in newTeam.targetTasks)
                {
                    if (!teamOnEvent.targetTasks.Contains(task) && (task.mainState != MainState.Completed || task.mainState != MainState.Failed))
                    {
                        teamOnEvent.targetTasks.Add(task);
                    }
                }
                newTeam.destroy = true;
            }
            foreach(Detective detective in teamOnEvent.detectives)
            {
                if (potencialDetectivesOnEvent.Contains(detective))
                {
                    potencialDetectivesOnEvent.Remove(detective);
                    detectivesOnEvent.Add(detective);
                }
            }
        }

        public void RemoveTeam(Team team)
        {
            if(teamOnEvent == team)
            {
                foreach(Detective detective in teamOnEvent.detectives)
                {
                    detectivesOnEvent.Remove(detective);
                }
                teamOnEvent = null;
            }
        }
    }
}
