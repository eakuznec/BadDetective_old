using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadDetective.UI;

namespace BadDetective.Dialog
{
    public class DialogManager : MonoBehaviour
    {
        private static DialogManager instance;
        private List<Dialog> dialogs = new List<Dialog>();
        private Dialog curDialog;
        private GameState prevState;

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

        public void StartDialog(Dialog dialog, Character owner)
        {
            Game game = Game.GetInstantiate();
            dialog.owner = owner;
            dialogs.Add(dialog);
            if (dialogs.Count == 1)
            {
                prevState = game.GetGameState();
                game.ChangeGameState(GameState.IN_DIALOG);
                curDialog = dialog;
                StartPhrase(curDialog.startPhrase);
            }
        }

        private void FinalizeDialog()
        {
            dialogs.Remove(curDialog);
            curDialog = null;
            if(dialogs.Count == 0)
            {
                Game game = Game.GetInstantiate();
                InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
                interfaceManager.dialogPanel.gameObject.SetActive(false);
                game.ChangeGameState(prevState);
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
            interfaceManager.dialogPanel.Set(phrase, curDialog.owner);
        }

        public void RealizeChoose(DialogChoose choose)
        {
            if (choose != null)
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
                        if (!condition.isFulfilled())
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
            else
            {
                FinalizeDialog();
            }
        }
    }
}
