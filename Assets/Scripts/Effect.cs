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
        public FileNote fileNote;
        public Dialog.Dialog dialog;
        public LogicMap.LogicMapOwnerType logicMapOwner;
        public LogicMap.LogicMap logicMap;
        public Item item;
        public int intValue;
        public bool boolValue;
        public string stringValue;

        public void Realize(iEffectsContainer effectsContainer)
        {
            this.effectsContainer = effectsContainer;
            Agency agency = Agency.GetInstantiate();
            QuestManager questManager = QuestManager.GetInstantiate();
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
                    effectsContainer.GetTeam().reportEvent.Add(questEvent);
                    effectsContainer.GetTeam().reportChangeTask.Add(task);
                    effectsContainer.GetTeam().reportTaskState.Add(mainState);
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
                    effectsContainer.GetTeam().reportChangeObjective.Add(objective);
                    effectsContainer.GetTeam().reportObjectiveState.Add(mainState);
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
                if (effectsContainer is QuestTask)
                {
                    Team team = effectsContainer.GetTeam();
                    team.reportQuest.Add(GetQuest());
                    team.reportNotes.Add(FileNoteContainer.Create(fileNote, team.transform));
                }
                else
                {
                    GetQuest().notes.Add(FileNoteContainer.Create(fileNote, GetQuest().transform));
                }
            }
            else if(type == EffectType.ADD_ITEM)
            {
                Team owner = effectsContainer.GetTeam();
                if (owner != null)
                {
                    Item newItem = Instantiate(item);
                    owner.AddItem(newItem);
                }
                else
                {
                    Item newItem = Instantiate(item, agency.transform);
                    agency.items.Add(newItem);
                }
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
                    logicMap.RealizeLogicMap(GetQuest());
                }
                else if (logicMapOwner == LogicMap.LogicMapOwnerType.QUEST_TASK)
                {
                    logicMap.RealizeLogicMap(task);
                }
            }
            else if (type == EffectType.START_DIALOG)
            {
                Team owner = effectsContainer.GetTeam();
                Dialog.DialogManager.GetInstantiate().StartDialog(dialog, owner, GetQuest());
            }
            else if(type == EffectType.FINALIZE_TASK)
            {
                task.FinalizeTask();
            }
            else if(type == EffectType.TEAM_GOTO_OFFICE)
            {
                Team owner = effectsContainer.GetTeam();
                if (owner != null)
                {
                    owner.GoTo(agency.GetOffice(), owner.GetPriorityWay(), true);
                }
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
