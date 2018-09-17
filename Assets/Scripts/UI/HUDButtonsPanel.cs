using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class HUDButtonsPanel : MonoBehaviour
    {
        [SerializeField]
        private Button questsButton;

        private void Awake()
        {
            InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
            questsButton.onClick.AddListener(delegate { interfaceManager.questBureau.Open(); });
        }
    }
}
