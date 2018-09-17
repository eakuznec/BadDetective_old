using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BadDetective.UI
{
    public class TraitLine : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Trait trait;
        [SerializeField] private Text traitName;

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            trait.showInInterface = true;
            traitName.color = Color.black;
        }

        public void SetTrait(Trait trait)
        {
            this.trait = trait;
            traitName.text = trait.traitName;
            if (!trait.showInInterface)
            {
                traitName.color = Color.blue;
            }
        }
    }
}