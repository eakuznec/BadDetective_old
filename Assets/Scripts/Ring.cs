using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Ring : MonoBehaviour
    {
        [SerializeField]
        private List<Tier> allTiers = new List<Tier>();
        public List<Path> paths = new List<Path>();

        public List<Tier> GetTiers()
        {
            return allTiers;
        }

        public List<string> GetTierNames()
        {
            List<string> retVal = new List<string>();
            foreach (Tier tier in allTiers)
            {
                if (tier.tierName == "")
                {
                    retVal.Add(tier.name);
                }
                else
                {
                    retVal.Add(tier.tierName);
                }
            }
            return retVal;
        }

    }
}
