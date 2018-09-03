﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class ActivitiesPanel : MonoBehaviour
    {
        [HideInInspector]
        public List<iActivityPlace> activities = new List<iActivityPlace>(); 
        [SerializeField]
        private EventPanel eventPanelPrefab;
        [Header("UI")]
        [SerializeField]
        private RectTransform activitiesContent;
        public Button closeButton;
        public Button acceptButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
            acceptButton.onClick.AddListener(Accept);
        }

        public void Open(List<iActivityPlace> openedActivities)
        {
            Game game = Game.GetInstantiate();
            gameObject.SetActive(true);
            game.ChangeGameState(GameState.IN_ACTIVITIES_PANEL);
            activities = openedActivities;
            float height = 0;
            foreach(iActivityPlace activity in activities)
            {
                if(activity is QuestEvent)
                {
                    EventPanel eventPanel = Instantiate(eventPanelPrefab, activitiesContent);
                    eventPanel.questEvent = (QuestEvent)activity;
                    eventPanel.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height, eventPanel.GetHeight());
                    height += eventPanel.GetHeight();
                }
            }
            height += 20;
            RectTransform rect = GetComponent<RectTransform>();
            if (height > Screen.height - 20 - 120)
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height - 20 - 120);
            }
            else
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            }
            CheckAccept();
        }

        public void Close()
        {
            Game game = Game.GetInstantiate();
            gameObject.SetActive(false);
            game.ChangeGameState(GameState.IN_GAME);
            for(int i=0; i<activitiesContent.childCount; i++)
            {
                Destroy(activitiesContent.GetChild(i).gameObject);
            }
        }

        private void Accept()
        {
            for (int i = 0; i < activitiesContent.childCount; i++)
            {
                EventPanel eventPanel = activitiesContent.GetChild(i).GetComponent<EventPanel>();
                if (eventPanel.CheckAccept())
                {
                    eventPanel.Accept();
                }
            }
            Close();
        }

        public void CheckAccept()
        {
            bool flag = false;
            for(int i=0; i< activitiesContent.childCount; i++)
            {
                EventPanel eventPanel = activitiesContent.GetChild(i).GetComponent<EventPanel>();
                if (eventPanel.CheckAccept())
                {
                    flag = true;
                }
                break;
            }
            if (flag)
            {
                acceptButton.gameObject.SetActive(true);
            }
            else
            {
                acceptButton.gameObject.SetActive(false);
            }
        }
    }
}
