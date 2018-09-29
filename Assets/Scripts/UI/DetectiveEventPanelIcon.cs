using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    [RequireComponent (typeof(Image))]
    public class DetectiveEventPanelIcon : MonoBehaviour
    {
        private Detective detective;
        private Image icon;
        [SerializeField]
        private Image shadowPanel;
        [SerializeField]
        private Sprite defaultSprite;

        private void Awake()
        {
            icon = GetComponent<Image>();
        }

        public void SetDetective(Detective detective, bool active)
        {
            if (detective != null)
            {
                InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
                if (this.detective != detective)
                {
                    this.detective = detective;
                    icon.sprite = detective.characterAvatar;
                    name = string.Format("DetectiveEventIcon_{0}", detective.characterName);
                    shadowPanel.gameObject.SetActive(!active);
                }
            }
            else
            {
                this.detective = detective;
                icon.sprite = defaultSprite;
                name = string.Format("DetectiveIcon_Default");
                name = string.Format("DetectiveEventIcon_Default");
                shadowPanel.gameObject.SetActive(false);
            }
        }
    }
}
