using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class DetectiveRollover : MonoBehaviour
    {
        private iRolloverOwner owner;

        [SerializeField] private Text detectiveName;
        [SerializeField] private Text temper;
        [SerializeField] private Text health;
        [SerializeField] private Text stress;
        [SerializeField] private Text brutal;
        [SerializeField] private Text accuracy;
        [SerializeField] private Text diplomacy;
        [SerializeField] private Text science;
        // Use this for initialization
        public void Show(iRolloverOwner rolloverOwner)
        {
            gameObject.SetActive(true);
            owner = rolloverOwner;
            Detective detective = null;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            transform.parent = owner.GetRectTransform();
            rectTransform.SetPositionAndRotation(new Vector2(owner.GetRectTransform().position.x, rectTransform.position.y), Quaternion.identity);
            if (owner is DetectiveIcon)
            {
                detective = ((DetectiveIcon)owner).detective;
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, owner.GetRectTransform().rect.yMax + 10, rectTransform.rect.height);
            }
            else if (owner is DetectiveReportIcon)
            {
                detective = ((DetectiveReportIcon)owner).detective;
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, owner.GetRectTransform().rect.yMin + 10 + rectTransform.rect.height, rectTransform.rect.height);
            }
            else if (owner is DetectiveEventPanelIcon)
            {
                detective = ((DetectiveEventPanelIcon)owner).detective;
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, owner.GetRectTransform().rect.yMin + 10 + rectTransform.rect.height, rectTransform.rect.height);
            }
            transform.parent = InterfaceManager.GetInstantiate().canvas.transform;
            if (rectTransform.offsetMin.x < -Screen.width/2)
            {
                rectTransform.SetPositionAndRotation(new Vector2(rectTransform.position.x-(rectTransform.offsetMin.x + Screen.width / 2)+10, rectTransform.position.y), Quaternion.identity);
            }
            else if (rectTransform.offsetMax.x > Screen.width/2)
            {
                rectTransform.SetPositionAndRotation(new Vector2(rectTransform.position.x + (Screen.width / 2 - rectTransform.offsetMin.x) - 10, rectTransform.position.y), Quaternion.identity);
            }
            if (detective != null)
            {
                detectiveName.text = detective.characterName;
                temper.text = detective.temper.ToString().ToLower();
                health.text = detective.GetHealthDescription();
                stress.text = detective.GetStressDescription();
                brutal.text = detective.GetMethodDescription(Method.Brutal);
                accuracy.text = detective.GetMethodDescription(Method.Accuracy);
                diplomacy.text = detective.GetMethodDescription(Method.Diplomacy);
                science.text = detective.GetMethodDescription(Method.Science);
            }
        }

        public void Hide()
        {
            owner = null;
            gameObject.SetActive(false);
        }
    }
}
