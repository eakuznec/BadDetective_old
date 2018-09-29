using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class ItemManager : MonoBehaviour
    {
        private static ItemManager instance;
        [SerializeField]
        private List<Item> allItems = new List<Item>();

        public static ItemManager GetInstantiate()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemManager>();
            }
            if (instance == null)
            {
                Game game = Game.GetInstantiate();
                instance = Instantiate(game.itemManager, game.transform);
            }
            return instance;
        }

        private void Awake()
        {
            GetInstantiate();
        }

        public List<Item> GetItems()
        {
            return allItems;
        }

        public List<string> GetItemsName()
        {
            List<string> retVal = new List<string>();
            foreach(Item item in allItems)
            {
                if(item != null)
                {
                    retVal.Add(item.itemName);
                }
                else
                {
                    retVal.Add("-NULL-");
                }
            }
            return retVal;
        }

        public List<Equipment> GetEquipment()
        {
            List<Equipment> equipments = new List<Equipment>();
            foreach(Item item in allItems)
            {
                if(item is Equipment)
                {
                    equipments.Add((Equipment)item);
                }
            }
            return equipments;
        }

        public void Registrate(Item item)
        {
            allItems.Add(item);
            UnityEditor.PrefabUtility.ReplacePrefab(this.gameObject, UnityEditor.PrefabUtility.GetPrefabParent(this));
        }
    }
}