using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadDetective.UI;

namespace BadDetective
{
    public class Team : MonoBehaviour
    {
        public List<Detective> detectives = new List<Detective>();
        public iActivityPlace startPlace;
        public iActivityPlace curTarget;
        public List<QuestTask> targetTasks = new List<QuestTask>();
        public QuestTask curTask;
       
        [HideInInspector]
        public DetectiveActivity activity;
        [HideInInspector]
        public Way curWay;
        private LineRenderer lineRenderer;
        public bool showWay;
        private float timeInWay;
        public bool destroy = false;

        [Header("Report")]
        public List<Quest> reportQuest = new List<Quest>();
        public List<FileNoteContainer> reportNotes = new List<FileNoteContainer>();
        public List<QuestEvent> reportEvent = new List<QuestEvent>();
        public List<QuestTask> reportChangeTask = new List<QuestTask>();
        public List<MainState> reportTaskState = new List<MainState>();
        public List<QuestObjective> reportChangeObjective = new List<QuestObjective>();
        public List<MainState> reportObjectiveState = new List<MainState>();

        public Detective GetLeader()
        {
            Detective retVal = null;
            float max = 0;
            foreach (Detective detective in detectives)
            {
                if (retVal == null || detective.GetTotalConfidece() > max)
                {
                    retVal = detective;
                    max = detective.GetTotalConfidece();
                }
            }
            return retVal;
        }

        public bool IsLeaderHaveTag(Tag tag)
        {
            return GetLeader().HaveTag(tag);
        }

        public bool IsTeamHaveTag(List<Detective> successedDetectives, List<Detective> failedDetectives, Tag tag)
        {
            bool retVal = false;
            foreach(Detective detective in detectives)
            {
                if (detective.HaveTag(tag))
                {
                    successedDetectives.Add(detective);
                    retVal = true;
                }
                else
                {
                    failedDetectives.Add(detective);
                }
            }
            return retVal;
        }

        public bool IsLeaderChallenge(Method method, int level, int difficult, Tag tag = Tag.NULL)
        {
            int methodValue = GetDetectiveInTeamMethodValue(GetLeader(), method, tag);
            if (methodValue < level)
            {
                methodValue = methodValue / 2;
            }
            return methodValue >= difficult;
        }

        public bool IsTeamChallenge(List<Detective> successedDetectives, List<Detective> failedDetectives, Method method, int level, int difficult, Tag tag = Tag.NULL)
        {
            int teamValue = 0;
            foreach(Detective detective in detectives)
            {
                int methodValue = GetDetectiveInTeamMethodValue(detective, method, tag);
                if (methodValue < level)
                {
                    failedDetectives.Add(detective);
                    methodValue = methodValue / 2;
                }
                else
                {
                    successedDetectives.Add(detective);
                }
                teamValue += methodValue;
            }
            return teamValue >= difficult;
        }

        public Method GetPriorityMethod(bool brutal, bool careful, bool diplomat, bool science)
        {
            return GetLeader().GetPriorityMethod(brutal, careful, diplomat, science);
        }

        public Temper GetPriorityTemper(bool rude, bool prudent, bool merciful, bool cruel, bool mercantile, bool principled)
        {
            return GetLeader().GetPriorityTemper(rude, prudent, merciful, cruel, mercantile, principled);
        }

        public Detective GetTeacher(Detective forWhom, out int result, Method method, Tag tag = Tag.NULL)
        {
            Detective teacher = null;
            result = 0;
            foreach(Detective detective in detectives)
            {
                if (detective != forWhom && detective.HaveTag(Tag.teacher))
                {
                    int value = detective.GetMethodValue(method, tag);
                    if (teacher == null || (result < value))
                    {
                        teacher = detective;
                        result = value;
                    }
                }
            }
            return teacher;
        }

        private int GetDetectiveInTeamMethodValue(Detective detective, Method method, Tag tag = Tag.NULL)
        {
            int retVal = 0;
            int maxMethodValue = 0;
            Detective teacher = GetTeacher(detective, out maxMethodValue, method, tag);
            retVal = detective.GetMethodValue(method, tag);
            if (teacher != null && retVal < maxMethodValue)
            {
                retVal++;
            }
            return retVal;
        }

        public void ChangeActivity(DetectiveActivity newActivity, iActivityPlace newPlace)
        {
            if (activity != newActivity)
            {
                if (activity == DetectiveActivity.IN_WAY)
                {
                    lineRenderer.positionCount = 0;
                    timeInWay = 0;
                }
                else if (activity == DetectiveActivity.IN_EVENT)
                {
                    ((QuestEvent)startPlace).RemoveTeam(this);
                }
                activity = newActivity;
                foreach (Detective detective in detectives)
                {
                    detective.ChangeActivity(newActivity);
                }
                if (activity == DetectiveActivity.IN_WAY)
                {
                    foreach (Detective detective in detectives)
                    {
                        timeInWay = 0;
                        detective.activityPlace = newPlace;
                    }
                }
                else if(activity == DetectiveActivity.IN_EVENT)
                {
                    startPlace = newPlace;
                    ((QuestEvent)newPlace).AddTeam(this);
                    StartTask();
                }
                else if (activity == DetectiveActivity.IN_OFFICE)
                {
                    destroy = true;
                }
                InterfaceManager.GetInstantiate().detectiveRow.ResetRow();
            }
            else if (detectives[0].activityPlace != newPlace)
            {
                foreach (Detective detective in detectives)
                {
                    detective.activityPlace = newPlace;
                }
            }
        }

        public void GoTo(iActivityPlace target, WayType wayType, bool colorWay)
        {
            curTarget = target;
            curWay = MapManager.GetInstantiate().pathfinder.GetWay(wayType, startPlace.GetPoint(), target.GetPoint(), transform);
            showWay = colorWay;
            ChangeActivity(DetectiveActivity.IN_WAY, curWay.pointsAndPaths[0]);
        }

        public void DrawWay(float deltaTime, WayType wayType = WayType.MAINSTREET)
        {
            timeInWay += deltaTime * GetSpeedModifier();
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, startPlace.GetPoint().transform.position);
            float timeToPoint = 0;
            for (int i = 0; i < curWay.pointsAndPaths.Count; i++)
            {
                iWayPlace place = curWay.pointsAndPaths[i];
                timeToPoint += place.GetTimeToWay();
                if (place is PointOnMap)
                {
                    lineRenderer.positionCount++;
                    Vector3 wayPoint = new Vector3(place.GetPoint().transform.position.x, place.GetPoint().transform.position.y, place.GetPoint().transform.position.z + 0.01f * Agency.GetInstantiate().teams.IndexOf(this));
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, wayPoint);
                }
                else if (place is Path)
                {
                    if (timeToPoint > timeInWay)
                    {
                        lineRenderer.positionCount++;
                        float part = (timeInWay - timeToPoint + place.GetTimeToWay()) / place.GetTimeToWay();
                        PointOnMap end = (PointOnMap)curWay.pointsAndPaths[i + 1];
                        Vector3 partVector = ((Path)place).GetPointOnPath(end, part);
                        Vector3 wayPoint = new Vector3(partVector.x, partVector.y, partVector.z + 0.01f * Agency.GetInstantiate().teams.IndexOf(this));
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, wayPoint);
                    }
                }
                MapManager mapManager = MapManager.GetInstantiate();
                if (detectives[0].activityPlace is iWayPlace && (iWayPlace)detectives[0].activityPlace == place)
                {
                    if (timeToPoint <= timeInWay)
                    {
                        if (i == curWay.pointsAndPaths.Count - 1)
                        {
                            if (curTarget is Office)
                            {
                                ChangeActivity(DetectiveActivity.IN_OFFICE, curTarget);
                            }
                            else if (curTarget is DetectiveHome)
                            {
                                ChangeActivity(DetectiveActivity.IN_HOME, curTarget);
                            }
                            else if (curTarget is QuestEvent)
                            {
                                ChangeActivity(DetectiveActivity.IN_EVENT, curTarget);
                            }
                            Destroy(curWay.gameObject);
                        }
                        else
                        {
                            ChangeActivity(DetectiveActivity.IN_WAY, curWay.pointsAndPaths[i + 1]);
                        }
                    }
                    break;
                }
            }
        }

        public void CreateLineRenderer()
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startColor = new Color(detectives[0].wayColor.r, detectives[0].wayColor.g, detectives[0].wayColor.b, 0);
            lineRenderer.endColor = detectives[0].wayColor;
            lineRenderer.positionCount = 0;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.material = Game.GetInstantiate().materialForWays;
        }

        public void StartTask()
        {
            if(targetTasks.Count>0)
            {
                foreach(QuestTask task in targetTasks)
                {
                    if(task!=null && (task.mainState == MainState.Started || GetReportTaskMainState(task) == MainState.Started))
                    {
                        task.Realize(this);
                        break;
                    }
                }
            }
        }

        public WayType GetPriorityWay()
        {
            return detectives[0].priorityWay;
        }

        public void Report()
        {
            for(int i =0; i< reportNotes.Count; i++)
            {
                reportQuest[i].notes.Add(reportNotes[i]);
                reportNotes[i].transform.parent = reportQuest[i].transform;
            }
            reportQuest.Clear();
            reportNotes.Clear();
            for (int i = 0; i < reportChangeTask.Count; i++)
            {
                reportEvent[i].ChangeTask(reportChangeTask[i], reportTaskState[i]);
            }
            reportEvent.Clear();
            reportChangeTask.Clear();
            reportTaskState.Clear();
            for(int i=0; i< reportChangeObjective.Count; i++)
            {
                reportChangeObjective[i].state = reportObjectiveState[i];
            }
            reportChangeObjective.Clear();
            reportObjectiveState.Clear();
        }

        private MainState GetReportTaskMainState(QuestTask task)
        {
            for(int i=0; i< reportChangeTask.Count; i++)
            {
                if(reportChangeTask[i] == task)
                {
                    return reportTaskState[i];
                }
            }
            return MainState.NotStarted;
        }

        public void AddItem(Item newItem)
        {
            Detective owner = newItem.GetPriorityOwner(this);
            owner.AddItem(newItem);
        }

        public float GetSpeedModifier()
        {
            float retVal = -1;
            foreach(Detective detective in detectives)
            {
                if (retVal == -1 || detective.speedMod < retVal)
                {
                    retVal = detective.speedMod;
                }
            }
            return retVal;
        }
    }
}
