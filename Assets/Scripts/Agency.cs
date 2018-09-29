using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    [RequireComponent(typeof(Timeline))]
    public class Agency : MonoBehaviour
    {
        private static Agency instance;
        public string agencyName;
        public Sprite agencySymbol;
        [SerializeField]
        private Office office;
        [SerializeField]
        private List<Detective> detectives = new List<Detective>();
        [HideInInspector]
        public List<Team> teams = new List<Team>();
        [SerializeField]
        private Money money;
        [HideInInspector]
        public List<Quest> quests = new List<Quest>();
        [HideInInspector]
        public List<Item> items = new List<Item>();
        [HideInInspector] public List<QuestState> globalStates = new List<QuestState>();

        [Header("Folders")]
        [HideInInspector]
        public GameObject teamFolder;

        public static Agency GetInstantiate()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Agency>();
            }
            if (instance == null)
            {
                instance = Instantiate(Game.GetInstantiate().newAgency);
            }
            return instance;
        }

        private void Awake()
        {
            GetInstantiate();
            DontDestroyOnLoad(instance);
        }

        private void Start()
        {
            //Инициализация офиса
            office = Instantiate(office, this.transform);
            //Инициализация детективов
            for(int i=0; i<detectives.Count; i++)
                {
                Detective detective = detectives[i];
                if(detective == null)
                {
                    detectives.RemoveAt(i);
                    i--;
                }
                else
                {
                    detectives[i] = Instantiate(detective, this.transform);
                    office.detectivesInOffice.Add(detectives[i]);
                    detectives[i].activityPlace = office;
                }
            }
        }

        public Office GetOffice()
        {
            return office;
        }

        public List<Detective> GetDetectives()
        {
            return detectives;
        }

        public Money GetMoney()
        {
            return money;
        }

        public void ChangeMoney(Money value)
        {
            money += value;
        }

        public List<QuestState> GetGlobalStates()
        {
            return globalStates;
        }

        public List<string> GetGlobalStateNames()
        {
            List<string> retVal = new List<string>();
            foreach (QuestState state in globalStates)
            {
                if (state != null)
                {
                    retVal.Add(state.stateName);
                }
                else
                {
                    retVal.Add("-NULL-");
                }
            }
            return retVal;
        }
    }
}