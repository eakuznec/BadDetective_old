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

        public List<Trait> GetTraits()
        {
            return allTraits;
        }

        public void Registrate(Trait trait)
        {
            allTraits.Add(trait);
            UnityEditor.PrefabUtility.ReplacePrefab(this.gameObject, UnityEditor.PrefabUtility.GetPrefabParent(this));
        }
    }
}
