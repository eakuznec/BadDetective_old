using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadDetective.UI;
using UnityEngine.Events;

namespace BadDetective.Dialog
{
    public class DialogManager : MonoBehaviour
    {
        private static DialogManager instance;
        private List<Dialog> dialogs = new List<Dialog>();
        private Dialog curDialog;
        private GameState prevState;
        private UnityAction endAction;
        [Header("Standart dialogs")]
        public Dialog endEventDialog;


        public static DialogManager GetInstantiate()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogManager>();
            }
            if (instance == null)
            {
                Game game = Game.GetInstantiate();
                instance = Instantiate(game.dialogManager, game.transform);
            }
            return instance;
        }

        private void Awake()
        {
            GetInstantiate();
        }

        public void StartDialog(Dialog dialog, Team teamOwner, Quest quest, UnityAction action = null)
        {
            Game game = Game.GetInstantiate();
            dialog.teamOwner = teamOwner;
            if(teamOwner != null)
            {
                dialog.owner = teamOwner.GetLeader();
            }
            dialog.questOwner = quest;
            dialogs.Add(dialog);
            if (dialogs.Count == 1)
            {
                prevState = game.GetGameState();
                endAction = action;
                game.ChangeGameState(GameState.IN_DIALOG);
                curDialog = dialog;
                StartPhrase(curDialog.startPhrase);
            }
        }

        private void FinalizeDialog()
        {
            dialogs.Remove(curDialog);
            if (curDialog.teamOwner != null)
            {
                curDialog.teamOwner.Report();
            }
            //curDialog = null;
            if(dialogs.Count == 0)
            {
                Game game = Game.GetInstantiate();
                InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
                interfaceManager.dialogPanel.gameObject.SetActive(false);
                game.ChangeGameState(prevState);
                if (endAction != null)
                {
                    endAction();
                }
            }
            else
            {
                curDialog = dialogs[0];
                StartPhrase(curDialog.startPhrase);
            }
        }

        private void StartPhrase(DialogPhrase phrase)
        {
            InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
            interfaceManager.dialogPanel.gameObject.SetActive(true);
            foreach (Effect effect in phrase.effects)
            {
                effect.Realize(curDialog);
            }
            if(phrase.type == PhraseType.DIALOG_PHRASE)
            {
                interfaceManager.dialogPanel.Set(phrase, curDialog.owner, curDialog.questOwner);
            }
            else if (phrase.type == PhraseType.REPORT)
            {
                interfaceManager.dialogPanel.SetReport(phrase, curDialog.teamOwner.reportNotes, curDialog.teamOwner, curDialog.questOwner);
            }
        }

        public void RealizeChoose(DialogChoose choose)
        {
            if (choose != null)
            {
                choose.realized = true;
                if(choose.type == ChooseType.END)
                {
                    curDialog.end = choose;
                    FinalizeDialog();
                    foreach (Effect effect in choose.effects)
                    {
                        effect.Realize(curDialog);
                    }
                }
                else
                {
                    foreach (Effect effect in choose.effects)
                    {
                        effect.Realize(curDialog);
                    }
                    List<DialogLink> avaliableLinks = new List<DialogLink>();
                    foreach (DialogLink link in choose.links)
                    {
                        bool flag = true;
                        foreach (Condition condition in link.conditions)
                        {
                            if (!condition.isFulfilled(curDialog.questOwner))
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            avaliableLinks.Add(link);
                        }
                    }
                    if (avaliableLinks.Count > 0)
                    {
                        StartPhrase(avaliableLinks[0].output);
                    }
                    else
                    {
                        FinalizeDialog();
                    }
                }
            }
            else
            {
                FinalizeDialog();
            }
        }
    }
}
