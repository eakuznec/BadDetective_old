using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class QuestManager : MonoBehaviour
    {
        private static QuestManager instance;
        [SerializeField]
        private List<Quest> allQuests = new List<Quest>();
        [HideInInspector]
        private List<Quest> questInstances = new List<Quest>();

        public static QuestManager GetInstantiate()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestManager>();
            }
            if (instance == null)
            {
                Game game = Game.GetInstantiate();
                instance = Instantiate(game.questManager, game.transform);
            }
            return instance;
        }

        private void Awake()
        {
            GetInstantiate();
        }

        private void Start()
        {
            Agency agency = Agency.GetInstantiate();
            GameObject goFolder = null;
            if (agency.transform.Find("Quests"))
            {
                goFolder = agency.transform.Find("Quests").gameObject;
            }
            else
            {
                goFolder = new GameObject("Quests");
                goFolder.transform.parent = agency.transform;
            }
            questInstances.Clear();
            for (int i=0; i<allQuests.Count; i++)
            {
                Quest quest = Instantiate(allQuests[i], goFolder.transform);
                agency.quests.Add(quest);
                questInstances.Add(quest);
            }
        }

        public List<Quest> GetQuests()
        {
            return allQuests;
        }

        public string[] GetQuestNames()
        {
            List<string> questNames = new List<string>();
            foreach(Quest quest in allQuests)
            {
                questNames.Add(quest.questName);
            }
            return questNames.ToArray();
        }

        public void Registrate(Quest quest)
        {
            allQuests.Add(quest);
            UnityEditor.PrefabUtility.ReplacePrefab(this.gameObject, UnityEditor.PrefabUtility.GetPrefabParent(this));
        }

        public Quest GetQuestInstance(Quest quest)
        {
            int index = allQuests.IndexOf(quest);
            return questInstances[index];
        }

        public List<Quest> GetQuestInstances()
        {
            return questInstances;
        }
    }
}
