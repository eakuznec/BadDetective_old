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
    }
}
