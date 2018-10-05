using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadDetective.UI;

namespace BadDetective
{
    [RequireComponent (typeof(SphereCollider))]
    public class PointOnMap : MonoBehaviour, iWayPlace
    {
        public string pointName;
        public float timeToWay;
        public float stressPerHour;
       
        public List<Path> avaliablePaths = new List<Path>();
        public Way shortWay = new Way();

        private bool mouseover;
        public List<iActivityPlace> pointContainer = new List<iActivityPlace>();
        [SerializeField]
        private SpriteRenderer label;
        private float labelTime;
        private int curLabelIndex;
        private bool showRollover;
        // Use this for initialization

        private void Awake()
        {
            if (shortWay == null)
            {
                GameObject go = new GameObject("ShortWay");
                go.transform.parent = transform;
                shortWay = go.AddComponent<Way>();
            }
            if (label == null)
            {
                if (transform.Find("Label") != null && transform.Find("Label").GetComponent<SpriteRenderer>())
                {
                    label = transform.Find("Label").GetComponent<SpriteRenderer>();
                }
            }
            GetComponent<SphereCollider>().radius = MapManager.GetInstantiate().pointRadius;
        }

        public List<iActivityPlace> GetActiveActivity()
        {
            List<iActivityPlace> retVal = new List<iActivityPlace>();
            foreach(iActivityPlace place in pointContainer)
            {
                if(place is Office)
                {
                    retVal.Add(place);
                }
                else if (place is DetectiveHome && ((DetectiveHome)place).characterInHome)
                {
                    retVal.Add(place);
                }
                else if (place is QuestEvent)
                {
                    foreach(QuestTask task in ((QuestEvent)place).tasks)
                    {
                        if(task.mainState == MainState.Started)
                        {
                            retVal.Add(place);
                            break;
                        }
                    }
                }
            }
            return retVal;
        }

        private void  OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.06f);
        }

        public void ResetShortWay()
        {
            shortWay.totalTime = 0;
            shortWay.pointsAndPaths.Clear();
            shortWay.check = false;
        }

        public List<Path> GetAvaliablePaths(WayType type)
        {
            List<Path> retVal = new List<Path>();
            foreach(Path path in avaliablePaths)
            {
                if(path.GetWayType() == WayType.MAINSTREET)
                {
                    retVal.Add(path);
                }
                else if (path.GetWayType() == WayType.TRANSPORT && type == WayType.TRANSPORT)
                {
                    retVal.Add(path);
                }
                else if (path.GetWayType() == WayType.BACKSTREET && type == WayType.BACKSTREET)
                {
                    retVal.Add(path);
                }
            }
            return retVal;
        }

        private void LateUpdate()
        {
            if (GetActiveActivity().Count > 0)
            {
                Game game = Game.GetInstantiate();
                InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
                MapManager mapManager = MapManager.GetInstantiate();
                label.gameObject.SetActive(true);
                label.transform.LookAt(Camera.main.transform);
                labelTime += Time.deltaTime;
                if (labelTime > mapManager.pointLabelTime)
                {
                    curLabelIndex++;
                    labelTime = 0;
                }
                if (curLabelIndex >= GetActiveActivity().Count)
                {
                    curLabelIndex = 0;
                }
                if(GetActiveActivity()[curLabelIndex] is Office)
                {
                    if (mouseover)
                    {
                        label.sprite = mapManager.overOfficeSprite;
                    }
                    else
                    {
                        label.sprite = mapManager.officeSprite;
                    }
                }
                else if(GetActiveActivity()[curLabelIndex] is DetectiveHome)
                {
                    if (mouseover)
                    {
                        label.sprite = mapManager.overHomeSprite;
                    }
                    else
                    {
                        label.sprite = mapManager.homeSprite;
                    }
                }
                else if (GetActiveActivity()[curLabelIndex] is QuestEvent)
                {
                    if (mouseover)
                    {
                        label.sprite = mapManager.eventOverSprite;
                    }
                    else
                    {
                        label.sprite = mapManager.eventSprite;
                    }
                }
                if(game.GetGameState()== GameState.IN_GAME || game.GetGameState() == GameState.WAIT_ACTIVITY_CHOICE)
                {
                    if (mouseover != showRollover)
                    {
                        showRollover = mouseover;
                        if (showRollover)
                        {
                            interfaceManager.activitiesRollover.Show(GetActiveActivity(), Camera.main.WorldToScreenPoint(transform.position));
                        }
                        else
                        {
                            interfaceManager.activitiesRollover.Hide();
                        }
                    }
                }
                else
                {
                    if(mouseover && showRollover)
                    {
                        showRollover = false;
                        interfaceManager.activitiesRollover.Hide();
                    }
                }
            }
            else
            {
                label.gameObject.SetActive(false);
            }
        }

        private void OnMouseEnter()
        {
            mouseover = true;
        }

        private void OnMouseExit()
        {
            mouseover = false;
        }

        private void OnMouseUp()
        {
            InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
            if (pointContainer.Count > 0)
            {
                interfaceManager.activitiesPanel.Open(pointContainer);
            }
        }

        public string GetPlaceName()
        {
            return pointName;
        }

        public float GetStressPerHour()
        {
            return stressPerHour;
        }

        public PointOnMap GetPoint()
        {
            return this;
        }

        public float GetTimeToWay()
        {
            return timeToWay;
        }
    }
}
