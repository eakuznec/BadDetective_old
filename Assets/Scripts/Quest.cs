﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadDetective.LogicMap;

namespace BadDetective
{
    public class Quest : MonoBehaviour, iLogicMapContainer
    {
        public string questName;
        [TextArea]
        public string shortDescription;
        public Character client;
        public Money reward;
        public bool haveStartTime;
        public bool relativeStartTime;
        public float startTime;
        public bool withDeadline;
        public bool relativeDeadline;
        public float deadline;
        public bool canBeRegistrated;
        public bool registrated;
        public List<FileNote> notes = new List<FileNote>();
        public QuestType type;
        public MainState mainState;
        public LogicMap.LogicMap startLogicMap;
        public LogicMap.LogicMap completeLogicMap;
        public LogicMap.LogicMap failLogicMap;
        public List<QuestState> questStates = new List<QuestState>();
        public List<QuestEvent> questEvents = new List<QuestEvent>();

        void Start()
        {
            if (haveStartTime && !relativeStartTime)
            {
                CreateQuestAction(MainState.Started);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ChangeMainState(MainState newMainState)
        {
            mainState = newMainState;
            if (mainState == MainState.Started && startLogicMap!=null)
            {
                startLogicMap.RealizeLogicMap(this);
            }
            else if(mainState == MainState.Completed && completeLogicMap!=null)
            {
                completeLogicMap.RealizeLogicMap(this);
            }
            else if(mainState == MainState.Failed && failLogicMap!=null)
            {
                failLogicMap.RealizeLogicMap(this);
            }
        }

        public void CreateQuestAction(MainState state)
        {
            Timeline timeline = Timeline.GetInstantiate();
            GameObject goAction = new GameObject(string.Format("TimelineAction_Quest_{0}", questName));
            goAction.transform.parent = timeline.transform;
            TimelineAction questAction = goAction.AddComponent<TimelineAction>();
            questAction.actionType = TimelineActionType.CHANGE_QUEST;
            questAction.quest = this;
            questAction.mainState = state;
            if (state == MainState.Started)
            {
                if(haveStartTime && !relativeStartTime)
                {
                    questAction.timer = startTime;
                }
            }
            timeline.RegistrateAction(questAction);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public List<QuestEvent> GetEvents()
        {
            return questEvents;
        }

        public string[] GetEventNames()
        {
            List<string> retVal = new List<string>();
            foreach(QuestEvent questEvent in questEvents)
            {
                retVal.Add(questEvent.eventName);
            }
            return retVal.ToArray();
        }
    }
}
