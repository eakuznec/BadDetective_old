using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Effect : MonoBehaviour
    {
        public EffectType type;
        iEffectsContainer effectsContainer;
        public MainState mainState;
        public Quest quest;
        public QuestEvent questEvent;
        public QuestTask task;
        public QuestObjective objective;
        public QuestState questState;
        public List<FileNote> fileNotes = new List<FileNote>();
        public Dialog.Dialog dialog;
        public LogicMap.LogicMapOwnerType logicMapOwner;
        public LogicMap.LogicMap logicMap;
        public Item item;
        public Money money;
        public int intValue;
        public bool boolValue;
        public string stringValue;
        public LogicMap.WaitType waitType;
        public GameTime waitTime;

        public void Realize(iEffectsContainer effectsContainer, Team team)
        {
            this.effectsContainer = effectsContainer;
            Game game = Game.GetInstantiate();
            Agency agency = Agency.GetInstantiate();
            QuestManager questManager = QuestManager.GetInstantiate();
            DetectiveManager detectiveManager = DetectiveManager.GetInstantiate();
            UI.InterfaceManager interfaceManager = UI.InterfaceManager.GetInstantiate();
            if(type == EffectType.INSTANTIATE_QUEST)
            {
                GameObject goFolder = null;
                if (agency.transform.Find("Quests"))
                {
                    goFolder = agency.transform.Find("Quests").gameObject;
                }
                else
                {
                    goFolder = new GameObject("Quests");
                    goFolder.transform.parent = agency.transform;
                }
                Quest questInstance = Instantiate(quest, goFolder.transform);
                agency.quests.Add(questInstance);
                questInstance.Realize();
            }
            else if (type == EffectType.CHANGE_QUEST)
            {
                GetQuest().ChangeMainState(mainState);
            }
            else if (type == EffectType.CHANGE_TASK)
            {
                if (effectsContainer is QuestTask)
                {
                    team.reportEvent.Add(questEvent);
                    team.reportChangeTask.Add(task);
                    team.reportTaskState.Add(mainState);
                    if(mainState == MainState.Started && task.mainState == MainState.NotStarted)
                    {
                        team.reportQuest.Add(GetQuest());
                        List<string> keys = new List<string>() { questEvent.eventName, task.taskName };
                        FileNoteContainer noteContainer = FileNoteContainer.Create(Dialog.DialogManager.GetInstantiate().newTaskNote, team.transform, keys);
                        team.reportNotes.Add(noteContainer);
                    }
                }
                else
                {
                    questEvent.ChangeTask(task, mainState);
                }
            }
            else if(type == EffectType.CHANGE_OBJECTIVE)
            {
                if (effectsContainer is QuestTask)
                {
                    team.reportChangeObjective.Add(objective);
                    team.reportObjectiveState.Add(mainState);
                }
                else
                {
                    objective.state = mainState;
                }
            }
            else if(type == EffectType.CHANGE_QUEST_STATE || type == EffectType.CHANGE_DIALOG_STATE || type == EffectType.CHANGE_GLOBAL_STATE)
            {
                if(questState.type == QuestStateType.BOOL)
                {
                    questState.boolValue = boolValue;
                }
                else if(questState.type == QuestStateType.INT)
                {
                    questState.intValue = intValue;
                }
                else if(questState.type == QuestStateType.SPECIAL)
                {
                    questState.specialValue = stringValue;
                }
            }
            else if(type == EffectType.ADD_FILE_NOTE)
            {
                foreach(FileNote fileNote in fileNotes)
                {
                    if (fileNote != null)
                    {
                        if (effectsContainer is QuestTask)
                        {
                            team.reportQuest.Add(GetQuest());
                            team.reportNotes.Add(FileNoteContainer.Create(fileNote, team.transform));
                        }
                        else
                        {
                            GetQuest().notes.Add(FileNoteContainer.Create(fileNote, GetQuest().transform));
                        }
                    }
                }
            }
            else if(type == EffectType.ADD_ITEM)
            {
                if (team != null)
                {
                    Item newItem = Instantiate(item);
                    team.AddItem(newItem);
                    team.reportQuest.Add(GetQuest());
                    FileNoteContainer noteContainer = FileNoteContainer.Create(Dialog.DialogManager.GetInstantiate().addItemNote, team.transform, newItem.itemName);
                    team.reportNotes.Add(noteContainer);
                }
                else
                {
                    Item newItem = Instantiate(item, agency.transform);
                    agency.items.Add(newItem);
                }
            }
            else if (type == EffectType.CHANGE_MONEY)
            {
                agency.ChangeMoney(money);
            }
            else if(type == EffectType.REALIZE_TASK)
            {
                Team owner = effectsContainer.GetTeam();
                if (owner != null)
                {
                    if (!owner.targetTasks.Contains(task))
                    {
                        owner.targetTasks.Insert(0, task);
                    }
                }
            }
            else if(type == EffectType.REALIZE_LOGIC_MAP)
            {
                if (logicMapOwner == LogicMap.LogicMapOwnerType.QUEST)
                {
                    logicMap.RealizeLogicMap(GetQuest(), team);
                }
                else if (logicMapOwner == LogicMap.LogicMapOwnerType.QUEST_TASK)
                {
                    logicMap.RealizeLogicMap(task, team);
                }
            }
            else if (type == EffectType.START_DIALOG)
            {
                Dialog.DialogManager.GetInstantiate().StartDialog(dialog, team, GetQuest());
            }
            else if(type == EffectType.FINALIZE_TASK)
            {
                task.FinalizeTask();
            }
            else if(type == EffectType.TEAM_GOTO_OFFICE)
            {
                if (team != null)
                {
                    team.GoTo(agency.GetOffice(), team.GetPriorityWay(), true);
                }
            }
            else if(type == EffectType.TEAM_GOTO_HOMES)
            {
                if (team != null)
                {
                    for(int i=0;i< team.detectives.Count;i++)
                    {
                        team.detectives[i].ReturnToHome();
                        i--;
                    }
                }
            }
            else if(type == EffectType.TEAM_GOTO_EVENT)
            {
                if (team != null)
                {
                    game.ChangeGameState(GameState.WAIT_ACTIVITY_CHOICE);
                    detectiveManager.teamOnWait = team;
                    interfaceManager.detectiveRow.ResetRow();
                }
            }
            else if(type == EffectType.TELEPORT_TO_EVENT)
            {
                if(team.curTarget is QuestEvent)
                {
                    ((QuestEvent)team.startPlace).RemoveTeam(team);
                    team.startPlace = questEvent;
                    questEvent.AddTeam(team);
                }

            }
            else if(type == EffectType.TIMELINE_ACTION_CHANGE_QUEST_STATE)
            {
                Timeline timeline = Timeline.GetInstantiate();
                GameObject goAction = new GameObject(string.Format("TimelineAction_ChangeQuestState"));
                goAction.transform.parent = timeline.transform;
                TimelineAction action = goAction.AddComponent<TimelineAction>();
                action.actionType = TimelineActionType.CHANGE_QUEST_STATE;
                action.questState = questState;
                action.flag = boolValue;
                action.value = intValue;
                action.specialValue = stringValue;
                if (waitType == LogicMap.WaitType.ABSOLUTE)
                {
                    action.timer = GameTime.ConvertToFloat(waitTime);
                }
                else if (waitType == LogicMap.WaitType.RELATION)
                {
                    action.timer = timeline.GetTime() + GameTime.ConvertToFloat(waitTime);
                }
                timeline.RegistrateAction(action);
            }
            else if (type == EffectType.CHECK_QUEST)
            {

            }
        }

        public void copyContentFrom(Effect other)
        {
            type = other.type;
            quest = other.quest;
            mainState = other.mainState;
        }

        public Quest GetQuest()
        {
            if(effectsContainer != null)
            {
                return effectsContainer.GetQuest();
            }
            else if(transform.GetComponentInParent<Quest>() != null)
            {
                return transform.GetComponentInParent<Quest>();
            }
            else if (transform.GetComponentInParent<Dialog.Dialog>() != null)
            {
                return transform.GetComponentInParent<Dialog.Dialog>().questOwner;
            }
            else
            {
                return null;
            }
        }

        public Dialog.Dialog GetDialog()
        {
            if (effectsContainer != null)
            {
                return effectsContainer.GetDialog();
            }
            else
            {
                return transform.GetComponentInParent<Dialog.Dialog>();
            }
        }

    }
}
