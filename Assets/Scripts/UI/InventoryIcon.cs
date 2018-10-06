using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BadDetective.UI
{
    [RequireComponent(typeof(Image))]
    public class InventoryIcon : MonoBehaviour, iMouseoverUI
    {
        public Item item;
        [SerializeField] private Sprite blockedSlot;
        [SerializeField] private Button equipeButton;
        [SerializeField] private Sprite equipeSprite;
        [SerializeField] private Sprite unequipeSprite;
        private bool isEquiped;

        private void Awake()
        {
            equipeButton.onClick.AddListener(Equipe);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }

        public void Redraw(Item item, bool equipe=false, bool block = false)
        {
            this.item = item;
            isEquiped = equipe;
            Image image = GetComponent<Image>();
            if (block)
            {
                image.sprite = blockedSlot;
                equipeButton.gameObject.SetActive(false);
            }
            else if(item == null)
            {
                image.sprite = null;
                equipeButton.gameObject.SetActive(false);
            }
            else
            {
                image.sprite = item.inventoryImage;
                if(item is Equipment)
                {
                    equipeButton.gameObject.SetActive(true);
                    if (isEquiped)
                    {
                        equipeButton.GetComponent<Image>().sprite = unequipeSprite;
                    }
                    else
                    {
                        equipeButton.GetComponent<Image>().sprite = equipeSprite;
                    }
                }
                else
                {
                    equipeButton.gameObject.SetActive(false);
                }
            }
        }

        private void Equipe()
        {
            InterfaceManager interfaceManager = InterfaceManager.GetInstantiate();
            Detective detective = interfaceManager.detectiveFile.GetDetective();
            detective.EquipeItem((Equipment)item, !isEquiped);
            interfaceManager.detectiveFile.SetInventory();
        }
    }
}
