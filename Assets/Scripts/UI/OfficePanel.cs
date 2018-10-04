using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class OfficePanel : MonoBehaviour, iActivityPanel
    {
        private Office _office;
        [Header("UI")]
        [SerializeField] private Text officeName;
        [SerializeField] private Text officeDescription;
        [SerializeField] private Button goToOfficeButton;

        private void Awake()
        {
            goToOfficeButton.onClick.AddListener(ReturnToOffice);
        }
        public float GetHeight()
        {
            return GetComponent<RectTransform>().rect.height;
        }

        public Office office
        {
            get
            {
                return _office;
            }
            set
            {
                _office = value;
                officeName.text = _office.GetPlaceName();
                officeDescription.text = _office.officeDescription;
                goToOfficeButton.gameObject.SetActive(InterfaceManager.GetInstantiate().activitiesPanel.prevState == GameState.WAIT_ACTIVITY_CHOICE);
            }
        }

        private void ReturnToOffice()
        {
            Team team = DetectiveManager.GetInstantiate().teamOnWait;
            team.GoTo(Agency.GetInstantiate().GetOffice(), team.GetPriorityWay(), true);
            InterfaceManager.GetInstantiate().activitiesPanel.Close();
            Game.GetInstantiate().ChangeGameState(GameState.IN_GAME);
        }
    }
}
