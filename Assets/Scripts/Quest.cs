using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadDetective.LogicMap;
using BadDetective.Dialog;

namespace BadDetective
{
    public class Quest : MonoBehaviour, iLogicMapContainer
    {
        public string questName;
        [TextArea]
        public string shortDescription;
        public Character client;
        public Money reward;
        public bool withDeadline;
        public bool relativeDeadline;
        public float deadline;
        public bool canBeRegistrated;
        public bool registrated;
        public List<FileNoteContainer> notes = new List<FileNoteContainer>();
        public QuestType type;
        public MainState mainState;
        public LogicMap.LogicMap startLogicMap;
        public LogicMap.LogicMap endLogicMap;
        public List<LogicMap.LogicMap> logicMaps = new List<LogicMap.LogicMap>();
        public List<Dialog.Dialog> dialogs = new List<Dialog.Dialog>();
        public List<QuestState> questStates = new List<QuestState>();
        public List<QuestEvent> questEvents = new List<QuestEvent>();
        public List<QuestObjective> questObjectives = new List<QuestObjective>();

        public void ChangeMainState(MainState newMainState)
        {
            mainState = newMainState;
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

        public List<LogicMap.LogicMap> GetLogicMaps()
        {
            return logicMaps;
        }

        public List<string> GetLogicMapNames()
        {
            List<string> retVal = new List<string>();
            foreach(LogicMap.LogicMap logiMap in logicMaps)
            {
                retVal.Add(logiMap.logicMapName);
            }
            return retVal;
        }

        public List<QuestObjective> GetQuestObjectives()
        {
            return questObjectives;
        }

        public List<string> GetQuestObjectiveNames()
        {
            List<string> retVal = new List<string>();
            foreach (QuestObjective questObjective in questObjectives)
            {
                retVal.Add(questObjective.objective);
            }
            return retVal;
        }

        public List<QuestState> GetQuestStates()
        {
            return questStates;
        }

        public List<string> GetQuestStateName()
        {
            List<string> retVal = new List<string>();
            foreach(QuestState state in questStates)
            {
                if (state != null)
                {
                    retVal.Add(state.stateName);
                }
                else
                {
                    retVal.Add("");
                }
            }
            return retVal;
        }

        public Character GetCharacterOwner()
        {
            return null;
        }

        public Team GetTeam()
        {
            return null;
        }

        public Quest GetQuest()
        {
            return this;
        }

        public void Realize()
        {
            startLogicMap.RealizeLogicMap(this, null);
        }

        public Dialog.Dialog GetDialog()
        {
            return null;
        }
    }
}
