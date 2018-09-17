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

        private void Awake()
        {
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
                    GetComponent<Image>().sprite = _detective.characterAvatar;
                    name = string.Format("DetectiveIcon_{0}", _detective.characterName);
                }
            }
        }

        private void OpenFile()
        {
            InterfaceManager.GetInstantiate().detectiveFile.Open(detective);
        }
    }
}
