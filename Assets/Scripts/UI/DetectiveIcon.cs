using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BadDetective.Control;

namespace BadDetective.UI
{
    [RequireComponent (typeof(Image))]
    public class DetectiveIcon : MonoBehaviour, iMouseoverUI, iRolloverOwner
    {
        [SerializeField]
        private Detective _detective;
        private Image icon;
        [SerializeField]
        private Image shadowPanel;
        [SerializeField]
        private Image activityPictogram;
        [SerializeField]
        private Button fileButton;
        [SerializeField] private Button homeButton;

        private void Awake()
        {
            icon = gameObject.GetComponent<Image>();
            fileButton.onClick.AddListener(OpenFile);
            homeButton.onClick.AddListener(ReturnToHome);
        }

        public Detective detective
        {
            get
            {
                return _detective;
            }
            set
            {
                InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
                if (this._detective != value)
                {
                    this._detective = value;
                    icon.sprite = _detective.characterAvatar;
                    name = string.Format("DetectiveIcon_{0}", _detective.characterName);
                    activityPictogram.gameObject.SetActive(true);
                }
                if (_detective.activity == DetectiveActivity.IN_OFFICE)
                {
                    homeButton.gameObject.SetActive(true);
                    shadowPanel.gameObject.SetActive(false);
                    activityPictogram.sprite = interfaceManager.officePictogram;
                }
                else if (_detective.activity == DetectiveActivity.IN_HOME)
                {
                    homeButton.gameObject.SetActive(false);
                    shadowPanel.gameObject.SetActive(true);
                    activityPictogram.sprite = interfaceManager.homePictogram;
                }
                else if (_detective.activity == DetectiveActivity.IN_WAY)
                {
                    homeButton.gameObject.SetActive(false);
                    shadowPanel.gameObject.SetActive(true);
                    activityPictogram.sprite = interfaceManager.walkPictogram;
                }
                else if (_detective.activity == DetectiveActivity.IN_EVENT)
                {
                    homeButton.gameObject.SetActive(false);
                    shadowPanel.gameObject.SetActive(true);
                    activityPictogram.sprite = interfaceManager.eventPictogram;
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ControlManager.GetInstantiate().mouseover = this;
            InterfaceManager.GetInstantiate().detectiveRollover.Show(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(ControlManager.GetInstantiate().mouseover == this)
            {
                ControlManager.GetInstantiate().mouseover = null;
            }
            InterfaceManager.GetInstantiate().detectiveRollover.Hide();
        }

        private void OpenFile()
        {
            InterfaceManager.GetInstantiate().detectiveFile.Open(detective);
        }

        private void ReturnToHome()
        {
            detective.ReturnToHome();
        }

        public RectTransform GetRectTransform()
        {
            return gameObject.GetComponent<RectTransform>();
        }
    }
}
