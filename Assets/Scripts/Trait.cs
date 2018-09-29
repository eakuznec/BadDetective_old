using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    [CreateAssetMenu(fileName = "Trait_", menuName = "Trait", order = 51)]
    public class Trait : ScriptableObject
    {
        public string traitName;
        public TraitCategory category;
        [TextArea]
        public string traitDescription;
        [Space (10)]
        public List<TraitEffect> traitEffects = new List<TraitEffect>();
        [Space(10)]
        public List<Tag> tags = new List<Tag>();
        [Space (10)]
        public TraitType type;

        [HideInInspector]
        public float liveTime;
        [HideInInspector]
        public int removePoint;

        public int GetTotalEffectValue(TraitEffectType type)
        {
            int retVal = 0;
            foreach (TraitEffect effect in traitEffects)
            {
                if (effect.type == type)
                {
                    retVal += effect.value;
                }
            }
            return retVal;
        }
    }

    public enum TraitCategory
    {
        FENOTYPE,
        TEMPER,
        WEAPON,
        WOUND,
        FEATURE,
        PSYCHOSIS,
        NULL
    }
}
