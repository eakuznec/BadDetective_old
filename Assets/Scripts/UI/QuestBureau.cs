using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class QuestBureau : MonoBehaviour
    {
        private GameState prevState;
        [SerializeField]
        private Button closeButton;
        [SerializeField]
        private RectTransform questPanel; 
        [SerializeField]
        private QuestButton questButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
        }

        public void Open()
        {
            Game game = Game.GetInstantiate();
            prevState = game.GetGameState();
            game.ChangeGameState(GameState.IN_QUEST_BEREAU);
            CheckQuests();
            gameObject.SetActive(true);
        }

        public void Close()
        {
            Game.GetInstantiate().ChangeGameState(prevState);
            gameObject.SetActive(false);
        }

        public void CheckQuests()
        {
            Agency agency = Agency.GetInstantiate();
            int dif = questPanel.childCount - agency.quests.Count;
            if (dif > 0)
            {
                for(int i=0; i < dif; i++)
                {
                    Destroy(questPanel.GetChild(questPanel.childCount - 1 - i));
                }
            }
            else if (dif < 0)
            {
                for (int i = 0; i < -dif; i++)
                {
                    Instantiate(questButton, questPanel);
                }
            }
            for(int i=0; i < agency.quests.Count; i++)
            {
                questPanel.GetChild(i).GetComponent<QuestButton>().SetQuest(agency.quests[i]);
            }
        }
    }
}