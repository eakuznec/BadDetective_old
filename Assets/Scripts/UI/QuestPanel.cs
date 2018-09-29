using System.Collections;
using System.Collections.Generic;
using BadDetective.LogicMap;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class QuestPanel : FilePanel, LogicMap.iLogicMapContainer
    {
        [HideInInspector]
        public Quest quest;
        [HideInInspector]
        public LogicMap.LogicMap ignorLogicMap;
        [HideInInspector]
        public LogicMap.LogicMap acceptLogicMap;
        [Header ("UI")]
        public Text questName;
        public Text clientName;
        public Text reward;
        public Text deadline;
        public Text description;
        public Image photo;
        public Button acceptButton;
        public Button ignorButton;

        public Character GetCharacterOwner()
        {
            return null;
        }

        public Team GetTeam()
        {
            return null;
        }

        public Quest GetQuest()
        {
            return null;
        }

        public Dialog.Dialog GetDialog()
        {
            return null;
        }

        public List<string> GetLogicMapNames()
        {
            return null;
        }

        public List<LogicMap.LogicMap> GetLogicMaps()
        {
            return null;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        private void Start()
        {
            if (quest != null)
            {
                if (questName != null)
                {
                    questName.text = quest.questName;
                }
                if (clientName != null)
                {
                    clientName.text = quest.client.characterName;
                }
                if (reward != null)
                {
                    reward.text = quest.reward.ToString();
                }
                if(deadline != null)
                {
                    if (quest.withDeadline)
                    {
                        if (!quest.relativeDeadline)
                        {
                            deadline.text = quest.deadline.ToString();
                        }
                    }
                    else
                    {
                        deadline.gameObject.SetActive(false);
                    }
                }
                if (description != null)
                {
                    description.text = quest.shortDescription;
                }
                if (photo != null)
                {
                    photo.sprite = quest.client.characterAvatar;
                }
            }
            else
            {
                if (questName != null)
                {
                    questName.gameObject.SetActive(false);
                }
                if (clientName != null)
                {
                    clientName.gameObject.SetActive(false);
                }
                if (reward != null)
                {
                    reward.gameObject.SetActive(false);
                }
                if (deadline != null)
                {
                    deadline.gameObject.SetActive(false);
                }
                if (description != null)
                {
                    description.gameObject.SetActive(false);
                }
                if (photo != null)
                {
                    photo.gameObject.SetActive(false);
                }
            }
            if (ignorButton != null)
            {
                ignorButton.onClick.RemoveAllListeners();
                if (ignorLogicMap != null)
                {
                    ignorButton.onClick.AddListener(delegate { ignorLogicMap.RealizeLogicMap(this); });
                }
                ignorButton.onClick.AddListener(Close);
            }
            if (acceptButton != null)
            {
                acceptButton.onClick.RemoveAllListeners();
                if (acceptLogicMap != null)
                {
                    acceptButton.onClick.AddListener(delegate { acceptLogicMap.RealizeLogicMap(this); });
                }
                acceptButton.onClick.AddListener(Close);

            }
        }
    }
}
