using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadDetective.UI;
using UnityEngine.Events;

namespace BadDetective
{
    public class TimelineAction : MonoBehaviour, IComparable
    {
        public TimelineActionType actionType;
        [HideInInspector]
        public float timer;
        public Office office;
        public Team team;
        public Detective detective;
        public TraitContainer trait;
        public Quest quest;
        public QuestState questState;
        public MainState mainState;
        public iActivityPlace activityPlace;
        public UnityAction action;
        public FilePanel filePanel;
        public Dialog.Dialog dialog;
        public Money money;
        public int value;
        public bool flag;
        public string specialValue;


        public int CompareTo(object obj)
        {
            TimelineAction action = obj as TimelineAction;
            int retVal = 0;
            if (action.timer < timer)
            {
                retVal = 1;
            }
            if (action.timer > timer)
            {
                retVal = -1;
            }

            // The orders are equivalent.
            return retVal;
        }

        public void RealizeAction()
        {
            if(actionType == TimelineActionType.OFFICE_LEASE)
            {
                Agency agency = Agency.GetInstantiate();
                agency.ChangeMoney(office.lease);
                office.CreateLeaseAction();
            }
            else if(actionType == TimelineActionType.DETECTIVE_SALARY)
            {
                Agency agency = Agency.GetInstantiate();
                agency.ChangeMoney(detective.salary);
                detective.CreateSalaryAction();
            }
            else if(actionType == TimelineActionType.TEMPORARY_TRAIT)
            {
                trait.Remove();
            }
            else if(actionType == TimelineActionType.CHANGE_QUEST)
            {
                quest.ChangeMainState(mainState);
            }
            else if (actionType == TimelineActionType.SHOW_FILE_PANEL)
            {
                filePanel.Open();
            }
            else if (actionType == TimelineActionType.WAIT)
            {
                action();
            }
            else if (actionType == TimelineActionType.START_DIALOG)
            {
                Dialog.DialogManager.GetInstantiate().StartDialog(dialog, null, null);
            }
            else if (actionType == TimelineActionType.CHANGE_QUEST_STATE)
            {
                if (questState.type == QuestStateType.BOOL)
                {
                    questState.boolValue = flag;
                }
                else if (questState.type == QuestStateType.INT)
                {
                    questState.intValue = value;
                }
                else if (questState.type == QuestStateType.SPECIAL)
                {
                    questState.specialValue = specialValue;
                }
            }
            else if (actionType == TimelineActionType.TEAM_GO_TO)
            {
                team.GoTo(activityPlace, team.GetPriorityWay(), true);
            }
        }
    }
}
