using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    [RequireComponent(typeof(Image))]
    public class DetectiveReportIcon : MonoBehaviour
    {
        private Detective _detective;
        [SerializeField] private Button fileButton;
        [SerializeField] private Button homeButton;


        private void Awake()
        {
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
                    GetComponent<Image>().sprite = _detective.characterAvatar;
                    name = string.Format("DetectiveIcon_{0}", _detective.characterName);
                }
            }
        }

        public bool canReturnHome
        {
            set
            {
                homeButton.gameObject.SetActive(value);
            }
        }

        private void OpenFile()
        {
            InterfaceManager.GetInstantiate().detectiveFile.Open(detective);
        }

        private void ReturnToHome()
        {
            _detective.ReturnToHome();
            InterfaceManager.GetInstantiate().dialogPanel.ResetDetectives();
        }
    }
}
