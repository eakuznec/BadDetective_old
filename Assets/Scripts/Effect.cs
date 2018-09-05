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
        public FileNote fileNote;
        public Dialog.Dialog dialog;
        public LogicMap.LogicMapOwnerType logicMapOwner;
        public LogicMap.LogicMap logicMap;
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
                int eventIndex = questManager.GetQuests()[questIndex].GetEvents().IndexOf(questEvent);
                int taskIndex = questManager.GetQuests()[questIndex].GetEvents()[eventIndex].GetTask().IndexOf(task);
                QuestEvent qEvent = questManager.GetQuestInstances()[questIndex].GetEvents()[eventIndex];
                qEvent.ChangeTask(qEvent.GetTask()[taskIndex], mainState);
            }
            else if(type == EffectType.ADD_FILE_NOTE)
            {
                int questIndex = questManager.GetQuestInstances().IndexOf(quest);
                questManager.GetQuestInstances()[questIndex].notes.Add(fileNote);
            }
            else if(type == EffectType.START_DIALOG)
            {
                Character owner = effectsContainer.GetCharacterOwner();
                Dialog.DialogManager.GetInstantiate().StartDialog(dialog, owner);
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
            else if(type == EffectType.CHECK_QUEST)
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
