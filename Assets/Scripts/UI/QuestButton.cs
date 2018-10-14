using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class QuestButton : MonoBehaviour
    {
        private Quest quest;
        [SerializeField]
        private Text questName;
        [SerializeField]
        private Text questState;
        [SerializeField] private Button endQuestButton;
        [SerializeField] private Button fileButton;

        public void SetQuest(Quest quest)
        {
            this.quest = quest;
            questName.text = quest.questName;
            questState.text = quest.mainState.ToString();
            if(quest.mainState == MainState.NotStarted)
            {
                questState.color = Color.gray;
            }
            else if (quest.mainState == MainState.Started)
            {
                questState.color = Color.black;
            }
            else if (quest.mainState == MainState.Completed)
            {
                questState.color = Color.green;
            }
            else if (quest.mainState == MainState.Failed)
            {
                questState.color = Color.red;
            }
            fileButton.onClick.RemoveAllListeners();
            fileButton.onClick.AddListener(ShowQuest);

            endQuestButton.onClick.RemoveAllListeners();
            endQuestButton.onClick.AddListener(delegate { quest.endLogicMap.RealizeLogicMap(quest, null); });
        }

        private void ShowQuest()
        {
            InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
            interfaceManager.questFile.Open(quest);
        }


    }
}