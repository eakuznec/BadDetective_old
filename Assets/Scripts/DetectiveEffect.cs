using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class DetectiveEffect : MonoBehaviour
    {
       public DetectiveEffectType type;
        public TraitCategory traitCategory;
       public Trait trait;
       public int value;
       public float floatValue;
       public bool flag;
        
        public void Realize(Detective detective)
        {
            if(type == DetectiveEffectType.CHANGE_HEALTH)
            {
                detective.ChangeCurHealth(value);
            }
            else if(type == DetectiveEffectType.CHANGE_STRESS)
            {
                detective.ChangeCurStress(value);
            }
            else if(type == DetectiveEffectType.CHANGE_LOYALTY)
            {
                detective.ChangeCurLoyalty(value);
            }
            else if(type == DetectiveEffectType.CHANGE_CONFIDENCE)
            {
                detective.ChangeCurConfidence(value);
            }
            else if(type == DetectiveEffectType.CHANCE_ADD_TRAIT)
            {
                float chance = Random.Range(0, 100f);
                if (chance <= floatValue)
                {
                    detective.AddTrait(trait);
                }
            }
        }
    }

    public enum DetectiveEffectType
    {
        NONE,
        CHANGE_HEALTH,
        CHANGE_STRESS,
        CHANGE_CONFIDENCE,
        CHANGE_LOYALTY,
        CHANCE_ADD_TRAIT,
        
    }
}
