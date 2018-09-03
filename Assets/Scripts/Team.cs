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

        public int GetLeaderMethodValue(Method method, Tag tag = Tag.NULL)
        {
            return GetDetectiveInTeamMethodValue(detectives[0], method, tag);
        }

        public int GetTeamMethodValue(Method method, Tag tag = Tag.NULL)
        {
            int retVal = 0;
            //
            return retVal;
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

        public int GetHighestMethodValue(Method method, Tag tag = Tag.NULL)
        {
            int retVal = 0;
            foreach(Detective detective in detectives)
            {
                int value = GetDetectiveInTeamMethodValue(detective, method, tag);
                if (retVal < value)
                {
                    retVal = value;
                }
            }
            return retVal;
        }

        public int GetLowesMethodValue(Method method, Tag tag = Tag.NULL)
        {
            int retVal = 999;
            foreach (Detective detective in detectives)
            {
                int value = GetDetectiveInTeamMethodValue(detective, method, tag);
                if (retVal > value)
                {
                    retVal = value;
                }
            }
            return retVal;
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
            curWay = MapManager.GetInstantiate().pathfinder.GetWay(wayType, startPlace.GetPoint(), target.GetPoint());
            showWay = colorWay;
            ChangeActivity(DetectiveActivity.IN_WAY, curWay.pointsAndPaths[0]);
        }

        public void DrawWay(float deltaTime)
        {
            timeInWay += deltaTime;
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
                    if(task!=null && task.mainState == MainState.Started)
                    {
                        curTask = task;
                        curTask.Realize(this);
                        break;
                    }
                }
            }
        }

        public WayType GetPriorityWay()
        {
            return detectives[0].priorityWay;
        }
    }
}
