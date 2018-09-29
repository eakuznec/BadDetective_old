using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BadDetective.Control;

namespace BadDetective.UI
{
    [RequireComponent (typeof(Image))]
    public class DetectiveIcon : MonoBehaviour, iMouseoverUI
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

        private void Awake()
        {
            icon = gameObject.GetComponent<Image>();
            fileButton.onClick.AddListener(OpenFile);
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
                    shadowPanel.gameObject.SetActive(false);
                    activityPictogram.sprite = interfaceManager.officePictogram;
                }
                else if (_detective.activity == DetectiveActivity.IN_HOME)
                {
                    shadowPanel.gameObject.SetActive(true);
                    activityPictogram.sprite = interfaceManager.homePictogram;
                }
                else if (_detective.activity == DetectiveActivity.IN_WAY)
                {
                    shadowPanel.gameObject.SetActive(true);
                    activityPictogram.sprite = interfaceManager.walkPictogram;
                }
                else if (_detective.activity == DetectiveActivity.IN_EVENT)
                {
                    shadowPanel.gameObject.SetActive(true);
                    activityPictogram.sprite = interfaceManager.eventPictogram;
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ControlManager.GetInstantiate().mouseover = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(ControlManager.GetInstantiate().mouseover == this)
            {
                ControlManager.GetInstantiate().mouseover = null;
            }
        }

        private void OpenFile()
        {
            InterfaceManager.GetInstantiate().detectiveFile.Open(detective);
        }
    }
}
