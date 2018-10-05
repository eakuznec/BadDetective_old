using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadDetective.Dialog;
using BadDetective.UI;

namespace BadDetective.Control
{
    public class DialogControl : MonoBehaviour
    {
        void Update()
        {
            Game game = Game.GetInstantiate();
            if(game.GetGameState() == GameState.IN_DIALOG)
            {
                int key = -1;
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    key = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    key = 2;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    key = 3;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    key = 4;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    key = 5;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    key = 6;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    key = 7;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    key = 8;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    key = 9;
                }
                if (key != -1)
                {
                    InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
                    DialogChoose choose = interfaceManager.dialogPanel.GetChoose(key);
                    if (choose != null)
                    {
                        DialogManager.GetInstantiate().RealizeChoose(choose);
                    }
                }
            }
        }
    }
}
