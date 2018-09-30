using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Item : MonoBehaviour
    {
        public string itemName;
        [TextArea]
        public string itemDescription;
        public Sprite image;
        [HideInInspector]
        public List<ItemCondition> conditions = new List<ItemCondition>();
        [HideInInspector]
        public List<int> weights = new List<int>();

        private Dictionary<Detective, int> owners = new Dictionary<Detective, int>();

        public Detective GetPriorityOwner(Team team)
        {
            Detective retVal = null;
            owners.Clear();
            foreach(Detective teamMember in team.detectives)
            {
                if (!owners.ContainsKey(teamMember))
                {
                    owners.Add(teamMember, 0);
                }
                for(int i = 0; i < conditions.Count; i++)
                {
                    owners[teamMember] += conditions[i].IsFulfilled(teamMember, weights[i], team);
                }
            }
            int max = 0;
            foreach(KeyValuePair<Detective, int> kvp in owners)
            {
                if(retVal==null || max < kvp.Value)
                {
                    retVal = kvp.Key;
                    max = kvp.Value;
                }
            }
            return retVal;
        }
    }
}
