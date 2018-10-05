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

        public DialogChoose GetChoose()
        {
            return choose;
        }

        public void SetChoose(DialogChoose choose, int index)
        {
            this.choose = choose;
            if(choose.type == ChooseType.CONTINUE)
            {
                chooseText.text = string.Format("{0}. {1}", index, "[Продолжить]");
            }
            else
            {
                chooseText.text = string.Format("{0}. {1}", index, choose.chooseText);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            chooseText.color = Color.blue;
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
