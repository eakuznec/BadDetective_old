using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Effect : MonoBehaviour
    {
        public EffectType type;
        iEffectsContainer effectsContainer;
        public Quest quest;
        public MainState mainState;
        public QuestEvent questEvent;
        public QuestTask task;
        public QuestObjective objective;
        public FileNote fileNote;
        public Dialog.Dialog dialog;
        public LogicMap.LogicMapOwnerType logicMapOwner;
        public LogicMap.LogicMap logicMap;
        public Item item;
        public int intValue;
        public bool boolValue;

        public void Realize(iEffectsContainer effectsContainer)
        {
            this.effectsContainer = effectsContainer;
            QuestManager questManager = QuestManager.GetInstantiate();
            if (type == EffectType.CHANGE_QUEST)
            {
                questManager.GetQuestInstance(quest).ChangeMainState(mainState);
            }
            else if (type == EffectType.CHANGE_TASK)
            {
                int questIndex = questManager.GetQuests().IndexOf(quest);
                int eventIndex = -1;
                int taskIndex = -1;
                if (questIndex != -1)
                {
                    eventIndex = questManager.GetQuests()[questIndex].GetEvents().IndexOf(questEvent);
                    taskIndex = questManager.GetQuests()[questIndex].GetEvents()[eventIndex].GetTask().IndexOf(task);
                }
                else
                {
                    questIndex = questManager.GetQuestInstances().IndexOf(quest);
                    eventIndex = questManager.GetQuestInstances()[questIndex].GetEvents().IndexOf(questEvent);
                    taskIndex = questManager.GetQuestInstances()[questIndex].GetEvents()[eventIndex].GetTask().IndexOf(task);
                }
                QuestEvent qEvent = questManager.GetQuestInstances()[questIndex].GetEvents()[eventIndex];
                qEvent.ChangeTask(qEvent.GetTask()[taskIndex], mainState);
            }
            else if(type == EffectType.CHANGE_OBJECTIVE)
            {
                int questIndex = questManager.GetQuests().IndexOf(quest);
                int objectiveIndex = -1;
                if(questIndex != -1)
                {
                    objectiveIndex = questManager.GetQuests()[questIndex].questObjectives.IndexOf(objective);
                }
                else
                {
                    questIndex = questManager.GetQuestInstances().IndexOf(quest);
                    objectiveIndex = questManager.GetQuestInstances()[questIndex].questObjectives.IndexOf(objective);
                }
                QuestObjective qObjective = questManager.GetQuestInstances()[questIndex].questObjectives[objectiveIndex];
                qObjective.state = mainState;
            }
            else if(type == EffectType.ADD_FILE_NOTE)
            {
                int questIndex = questManager.GetQuestInstances().IndexOf(quest);
                questManager.GetQuestInstances()[questIndex].notes.Add(fileNote);
            }
            else if(type == EffectType.ADD_ITEM)
            {
                Team owner = effectsContainer.GetTeam();
                if (owner != null)
                {
                    Item newItem = Instantiate(item, owner.transform);
                    owner.items.Add(newItem);
                }
                else
                {
                    Agency agency = Agency.GetInstantiate();
                    Item newItem = Instantiate(item, agency.transform);
                    agency.items.Add(newItem);
                }
            }
            else if(type == EffectType.REALIZE_TASK)
            {
                int questIndex = questManager.GetQuests().IndexOf(quest);
                int eventIndex = -1;
                int taskIndex = -1;
                if (questIndex != -1)
                {
                    eventIndex = questManager.GetQuests()[questIndex].GetEvents().IndexOf(questEvent);
                    taskIndex = questManager.GetQuests()[questIndex].GetEvents()[eventIndex].GetTask().IndexOf(task);
                }
                else
                {
                    questIndex = questManager.GetQuestInstances().IndexOf(quest);
                    eventIndex = questManager.GetQuestInstances()[questIndex].GetEvents().IndexOf(questEvent);
                    taskIndex = questManager.GetQuestInstances()[questIndex].GetEvents()[eventIndex].GetTask().IndexOf(task);
                }
                Team owner = effectsContainer.GetTeam();
                if (owner != null)
                {
                    questManager.GetQuestInstances()[questIndex].GetEvents()[eventIndex].GetTask()[taskIndex].Realize(owner);
                }
            }
            else if(type == EffectType.REALIZE_LOGIC_MAP)
            {
                if(logicMapOwner == LogicMap.LogicMapOwnerType.QUEST)
                {
                    logicMap.RealizeLogicMap(questManager.GetQuestInstance(quest));
                }
                else if (logicMapOwner == LogicMap.LogicMapOwnerType.QUEST_TASK)
                {
                    int questIndex = questManager.GetQuests().IndexOf(quest);
                    int eventIndex = questManager.GetQuests()[questIndex].GetEvents().IndexOf(questEvent);
                    int taskIndex = questManager.GetQuests()[questIndex].GetEvents()[eventIndex].GetTask().IndexOf(task);
                    QuestTask ownerTask = questManager.GetQuestInstance(quest).GetEvents()[eventIndex].GetTask()[taskIndex];
                    logicMap.RealizeLogicMap(ownerTask);
                }
            }
            else if (type == EffectType.START_DIALOG)
            {
                Character owner = effectsContainer.GetCharacterOwner();
                Dialog.DialogManager.GetInstantiate().StartDialog(dialog, owner);
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
    }
}
