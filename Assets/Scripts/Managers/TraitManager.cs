using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class TraitManager : MonoBehaviour
    {
        private static TraitManager instance;
        [SerializeField]
        private List<Trait> allTraits = new List<Trait>();

        public static TraitManager GetInstantiate()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TraitManager>();
            }
            if (instance == null)
            {
                Game game = Game.GetInstantiate();
                instance = Instantiate(game.traitManager, game.transform);
            }
            return instance;
        }

        private void Awake()
        {
            GetInstantiate();
        }

        public List<Trait> GetTraits(TraitCategory category = TraitCategory.NULL)
        {
            if(category == TraitCategory.NULL)
            {
                return allTraits;
            }
            else
            {
                List<Trait> retVal = new List<Trait>();
                foreach(Trait t in allTraits)
                {
                    if(t.category == category)
                    {
                        retVal.Add(t);
                    }
                }
                return retVal;
            }
        }

        public List<string> GetTraitNames(TraitCategory category = TraitCategory.NULL)
        {
            List<string> retVal = new List<string>();
            foreach(Trait trait in allTraits)
            {
                if(category == TraitCategory.NULL || category == trait.category)
                {
                    retVal.Add(trait.traitName);
                }
            }
            return retVal;
        }

        public void Registrate(Trait trait)
        {
            allTraits.Add(trait);
            UnityEditor.PrefabUtility.ReplacePrefab(this.gameObject, UnityEditor.PrefabUtility.GetPrefabParent(this));
        }
    }
}
