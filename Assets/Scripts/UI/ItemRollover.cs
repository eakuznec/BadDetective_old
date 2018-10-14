using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class ItemRollover : MonoBehaviour
    {
        private iRolloverOwner owner;

        [SerializeField] private Text itemName;

        public void Show(iRolloverOwner rolloverOwner)
        {
            gameObject.SetActive(true);
            owner = rolloverOwner;
            Item item = ((InventoryIcon)rolloverOwner).item;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            transform.parent = owner.GetRectTransform();
            rectTransform.SetPositionAndRotation(new Vector2(owner.GetRectTransform().position.x, rectTransform.position.y), Quaternion.identity);
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, owner.GetRectTransform().rect.yMax - 10 - rectTransform.rect.height, rectTransform.rect.height);
            transform.parent = InterfaceManager.GetInstantiate().canvas.transform;

            itemName.text = item.itemName;
        }

        public void Hide()
        {
            owner = null;
            gameObject.SetActive(false);
        }
    }
}
