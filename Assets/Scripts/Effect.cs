using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Effect : MonoBehaviour
    {
        public EffectType type;
        LogicMap.iLogicMapContainer logicMapContainer;
        public Quest quest;
        public MainState mainState;
        public QuestEvent questEvent;
        public QuestTask task;
        public FileNote fileNote;
        public Dialog.Dialog dialog;
        public int intValue;
        public bool boolValue;

        public void Realize(LogicMap.iLogicMapContainer logicMapContainer)
        {
            this.logicMapContainer = logicMapContainer;
            QuestManager questManager = QuestManager.GetInstantiate();
            if (type == EffectType.CHANGE_QUEST)
            {
                questManager.GetQuestInstance(quest).ChangeMainState(mainState);
            }
            else if (type == EffectType.CHANGE_TASK)
            {
                int questIndex = questManager.GetQuestInstances().IndexOf(quest);
                int eventIndex = questManager.GetQuestInstances()[questIndex].GetEvents().IndexOf(questEvent);
                int taskIndex = questManager.GetQuestInstances()[questIndex].GetEvents()[eventIndex].GetTask().IndexOf(task);
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
                Character owner = null;
                if(logicMapContainer is QuestTask)
                {
                    owner = ((QuestTask)logicMapContainer).GetTeam().detectives[0];
                }
                else if(logicMapContainer is Dialog.Dialog)
                {
                    owner = ((Dialog.Dialog)logicMapContainer).owner;
                }
                Dialog.DialogManager.GetInstantiate().StartDialog(dialog, owner);
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
