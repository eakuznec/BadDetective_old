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

        public void AttachEffects(Detective owner)
        {
                foreach (TraitEffect effect in traitEffects)
                {
                    if (effect.type == TraitEffectType.CHANGE_BRUTAL)
                    {
                        owner.ChangeMethodValue(Method.Brutal, effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_CAREFUL)
                    {
                        owner.ChangeMethodValue(Method.Careful, effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_DIPLOMATIC)
                    {
                        owner.ChangeMethodValue(Method.Diplomatic, effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_SCIENTIFIC)
                    {
                        owner.ChangeMethodValue(Method.Scientific, effect.value);
                    }
                    if (effect.type == TraitEffectType.CHANGE_MAX_BRUTAL)
                    {
                        owner.ChangeMaxMethodValue(Method.Brutal, effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_MAX_CAREFUL)
                    {
                        owner.ChangeMaxMethodValue(Method.Careful, effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_MAX_DIPLOMATIC)
                    {
                        owner.ChangeMaxMethodValue(Method.Diplomatic, effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_MAX_SCIENTIFIC)
                    {
                        owner.ChangeMaxMethodValue(Method.Scientific, effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_MAX_HEALTH)
                    {
                        owner.ChangeMaxHealth(effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_MIN_STRESS)
                    {
                        owner.ChangeMinStress(effect.value);
                    }
                    else if (effect.type == TraitEffectType.DETACH_TRAIT)
                    {
                        TraitContainer container = owner.GetTrait(effect.trait);
                        if (container != null)
                        {
                        container.isDettached = true;
                            container.DetachEffects();
                        }
                    }
                    else if (effect.type == TraitEffectType.CHANGE_SPEED_MOD)
                    {
                        owner.speedMod *= effect.floatValue;
                    }
                    else if (effect.type == TraitEffectType.CHANGE_BLOCKED_EQUIPMENT_SLOT)
                    {
                        owner.ChangeBlockedSlot(effect.value);
                    }
                    else if(effect.type == TraitEffectType.CHANGE_MAX_ITEM_SLOT)
                {
                    owner.ChangeMaxItemSlot(effect.value);
                }
                }
        }

        public void DetachEffects(Detective owner)
        {
                foreach (TraitEffect effect in traitEffects)
                {
                    if (effect.type == TraitEffectType.CHANGE_BRUTAL)
                    {
                        owner.ChangeMethodValue(Method.Brutal, -effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_CAREFUL)
                    {
                        owner.ChangeMethodValue(Method.Careful, -effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_DIPLOMATIC)
                    {
                        owner.ChangeMethodValue(Method.Diplomatic, -effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_SCIENTIFIC)
                    {
                        owner.ChangeMethodValue(Method.Scientific, -effect.value);
                    }
                    if (effect.type == TraitEffectType.CHANGE_MAX_BRUTAL)
                    {
                        owner.ChangeMaxMethodValue(Method.Brutal, -effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_MAX_CAREFUL)
                    {
                        owner.ChangeMaxMethodValue(Method.Careful, -effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_MAX_DIPLOMATIC)
                    {
                        owner.ChangeMaxMethodValue(Method.Diplomatic, -effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_MAX_SCIENTIFIC)
                    {
                        owner.ChangeMaxMethodValue(Method.Scientific, -effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_MAX_HEALTH)
                    {
                        owner.ChangeMaxHealth(-effect.value);
                    }
                    else if (effect.type == TraitEffectType.CHANGE_MIN_STRESS)
                    {
                        owner.ChangeMinStress(-effect.value);
                    }
                    else if (effect.type == TraitEffectType.DETACH_TRAIT)
                    {
                        TraitContainer container = owner.GetTrait(effect.trait);
                        if (container != null)
                        {
                        container.isDettached = false;
                            container.AttachEffects();
                        }
                    }
                    else if (effect.type == TraitEffectType.CHANGE_SPEED_MOD)
                    {
                        owner.speedMod /= effect.floatValue;
                    }
                    else if (effect.type == TraitEffectType.CHANGE_BLOCKED_EQUIPMENT_SLOT)
                    {
                        owner.ChangeBlockedSlot(-effect.value);
                    }
                else if (effect.type == TraitEffectType.CHANGE_MAX_ITEM_SLOT)
                {
                    owner.ChangeMaxItemSlot(-effect.value);
                }

            }
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
        NULL,
        EQUIPMENT
    }
}
