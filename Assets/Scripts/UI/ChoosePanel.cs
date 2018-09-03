using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BadDetective.Dialog;
using UnityEngine.EventSystems;

namespace BadDetective.UI
{
    public class ChoosePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private DialogChoose choose;
        [SerializeField]
        private Text chooseText;

        public void SetChoose(DialogChoose choose, int index)
        {
            this.choose = choose;
            chooseText.text = string.Format("{0}. {1}", index, choose.chooseText);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            chooseText.color = Color.yellow;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            chooseText.color = Color.black;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            DialogManager.GetInstantiate().RealizeChoose(choose);
        }
    }
}
