using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BadDetective.Control;

namespace BadDetective.UI
{
    public class EventPanel : MonoBehaviour, iActivityPanel, iMouseoverUI
    {
        private QuestEvent _questEvent;
        private float height;
        [Header("UI")]
        public Text eventName;
        public Text eventDescription;
        public RectTransform tasksPanel;
        public RectTransform detectivePanel;
        [Header ("Samples")]
        public DetectiveEventPanelIcon detectiveIcon;
        public TaskPanel taskPanel;

        public QuestEvent questEvent
        {
            get
            {
                return _questEvent;
            }
            set
            {
                height = 0;
                this._questEvent = value;
                eventName.text = _questEvent.eventName;
                eventDescription.text = _questEvent.eventDescription;
                CheckTasks();
                CheckDetectives();
                height += 60 + 60 + 130;
                GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            }
        }

        public void CheckTasks()
        {
            List<QuestTask> uncompletedTasks = _questEvent.GetUncompletedTasks();
            int dif = tasksPanel.childCount - uncompletedTasks.Count;
            if (dif > 0)
            {
                for (int i = 0; i < dif; i++)
                {
                    Destroy(tasksPanel.GetChild(i).gameObject);
                }
            }
            else if (dif < 0)
            {
                for (int i = 0; i < -dif; i++)
                {
                    Instantiate(taskPanel, tasksPanel);
                }
            }
            for (int i = 0; i < tasksPanel.childCount; i++)
            {
                RectTransform panel = tasksPanel.GetChild(i).GetComponent<RectTransform>();
                float panelHeight = panel.rect.height;
                panel.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, panelHeight * i, panelHeight);
                height += panelHeight;
                panel.GetComponent<TaskPanel>().task = uncompletedTasks[i];
            }
        }

        public void CheckDetectives()
        {
            InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
            if (interfaceManager.activitiesPanel.prevState == GameState.WAIT_ACTIVITY_CHOICE)
            {
                detectivePanel.gameObject.SetActive(false);
            }
            else
            {
                detectivePanel.gameObject.SetActive(true);
                int dif = detectivePanel.childCount - _questEvent.GetAllDetectivesCount() - 1;
                if (dif > 0)
                {
                    for (int i = 0; i < dif; i++)
                    {
                        Destroy(detectivePanel.GetChild(i).gameObject);
                    }
                }
                else if (dif < 0)
                {
                    for (int i = 0; i < -dif; i++)
                    {
                        Instantiate(detectiveIcon, detectivePanel);
                    }
                }
                for (int i = 0; i < detectivePanel.childCount; i++)
                {
                    if (i < _questEvent.detectivesOnEvent.Count)
                    {
                        detectivePanel.GetChild(i).GetComponent<DetectiveEventPanelIcon>().SetDetective(_questEvent.detectivesOnEvent[i], true);
                    }
                    else if (i < _questEvent.detectivesOnEvent.Count + _questEvent.potencialDetectivesOnEvent.Count)
                    {
                        int index = i - _questEvent.detectivesOnEvent.Count;
                        detectivePanel.GetChild(i).GetComponent<DetectiveEventPanelIcon>().SetDetective(_questEvent.potencialDetectivesOnEvent[index], false);
                    }
                    else if (i < _questEvent.GetAllDetectivesCount())
                    {
                        int index = i - _questEvent.detectivesOnEvent.Count - _questEvent.potencialDetectivesOnEvent.Count;
                        detectivePanel.GetChild(i).GetComponent<DetectiveEventPanelIcon>().SetDetective(_questEvent.plannedDetectivesOnEvent[index], true);
                    }
                    else
                    {
                        detectivePanel.GetChild(i).GetComponent<DetectiveEventPanelIcon>().SetDetective(null, false);
                    }
                }
                float iconWidth = detectivePanel.GetChild(0).GetComponent<RectTransform>().rect.width;
                detectivePanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (_questEvent.GetAllDetectivesCount() + 1) * iconWidth + _questEvent.GetAllDetectivesCount() * 10);
                interfaceManager.activitiesPanel.CheckAccept();
            }
        }

        public float GetHeight()
        {
            return height;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ControlManager.GetInstantiate().mouseover = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (ControlManager.GetInstantiate().mouseover == this)
            {
                ControlManager.GetInstantiate().mouseover = null;
            }
        }

        public void Accept()
        {
            DetectiveManager detectiveManager = DetectiveManager.GetInstantiate();
            InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
            List<QuestTask> tasks = new List<QuestTask>();
            for(int i=0; i<tasksPanel.childCount; i++)
            {
                TaskPanel panel = tasksPanel.GetChild(i).GetComponent<TaskPanel>();
                if (panel.taskToggle.isOn)
                {
                    tasks.Add(panel.task);
                }
            }
            if(interfaceManager.activitiesPanel.prevState == GameState.WAIT_ACTIVITY_CHOICE)
            {
                Team team = detectiveManager.teamOnWait;
                team.targetTasks = tasks;
                if(team.startPlace == questEvent)
                {
                    interfaceManager.activitiesPanel.prevState = GameState.IN_GAME;
                    questEvent.AddTeam(team);
                    team.StartTask();
                }
                else
                {
                    team.GoTo(questEvent, team.GetPriorityWay(), true);
                    interfaceManager.activitiesPanel.prevState = GameState.IN_GAME;
                }
            }
            else
            {
                List<Detective> detectives = new List<Detective>();
                foreach (Detective detective in _questEvent.plannedDetectivesOnEvent)
                {
                    detectives.Add(detective);
                    _questEvent.potencialDetectivesOnEvent.Add(detective);
                }
                detectiveManager.TeamOnTarget(detectives, questEvent, tasks);
            }
            _questEvent.plannedDetectivesOnEvent.Clear();
        }

        public bool CheckAccept()
        {
            bool detectiveFlag = false;
            bool taskFlag = false;
            if (questEvent.plannedDetectivesOnEvent.Count > 0)
            {
                detectiveFlag = true;
            }
            for(int i=0; i< tasksPanel.childCount; i++)
            {
                TaskPanel panel = tasksPanel.GetChild(i).GetComponent<TaskPanel>();
                if (panel.taskToggle.isOn)
                {
                    taskFlag = true;
                    break;
                }
            }
            return detectiveFlag && taskFlag;
        }
    }
}
