using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class TraitContainer : MonoBehaviour
    {
        public Trait trait;
        [HideInInspector]
        public TraitCategory category;
        [HideInInspector]
        public Detective owner;

        [HideInInspector]
        public bool isHidden;
        [HideInInspector]
        public bool isMimicry;
        [HideInInspector]
        public Trait mimicryTrait;
        [HideInInspector]
        public int removePoint;

        [HideInInspector]
        public bool showInInterface = false;

        private void Start()
        {
            owner = GetComponentInParent<Detective>();
            if (trait.type == TraitType.TEMPORARY)
            {
                CreateTemporaryTraitAction();
            }
            AttachEffects();
        }

        public void CreateTemporaryTraitAction()
        {
            Timeline timeline = Timeline.GetInstantiate();
            GameObject goAction = new GameObject(string.Format("TimelineAction_TamporaryTrait_{0}", trait.traitName));
            goAction.transform.parent = timeline.transform;
            TimelineAction traitAction = goAction.AddComponent<TimelineAction>();
            traitAction.actionType = TimelineActionType.TEMPORARY_TRAIT;
            traitAction.trait = this;
            traitAction.timer = timeline.GetTime() + trait.liveTime;
            timeline.RegistrateAction(traitAction);
        }

        public void Remove()
        {
            DetachEffects();
            owner.traits.Remove(this);
            Destroy(gameObject);
        }

        public void AttachEffects()
        {
            if (HaveTag(Tag.NULL))
            {
                foreach(TraitEffect effect in trait.traitEffects)
                {
                    if(effect.type == TraitEffectType.CHANGE_BRUTAL)
                    {
                        owner.ChangeMethodValue(Method.Brutal, effect.value);
                    }
                    else if(effect.type == TraitEffectType.CHANGE_CAREFUL)
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
                    else if(effect.type == TraitEffectType.CHANGE_MAX_HEALTH)
                    {
                        owner.ChangeMaxHealth(effect.value);
                    }
                    else if(effect.type == TraitEffectType.CHANGE_MIN_STRESS)
                    {
                        owner.ChangeMinStress(effect.value);
                    }
                    else if(effect.type == TraitEffectType.DETACH_TRAIT)
                    {
                        TraitContainer container = owner.GetTrait(trait);
                        if (container != null)
                        {
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
                }
            }
        }

        public void DetachEffects()
        {
            if (HaveTag(Tag.NULL))
            {
                foreach (TraitEffect effect in trait.traitEffects)
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
                        TraitContainer container = owner.GetTrait(trait);
                        if (container != null)
                        {
                            container.AttachEffects();
                        }
                    }
                    else if(effect.type == TraitEffectType.CHANGE_SPEED_MOD)
                    {
                        owner.speedMod /= effect.floatValue;
                    }
                    else if(effect.type == TraitEffectType.CHANGE_BLOCKED_EQUIPMENT_SLOT)
                    {
                        owner.ChangeBlockedSlot(-effect.value);
                    }
                }
            }
        }

        public List<Tag> GetTags()
        {
            return trait.tags;
        }

        public int GetTotalEffectValue(TraitEffectType type)
        {
            return trait.GetTotalEffectValue(type);
        }

        public bool HaveTag(Tag tag)
        {
            if(tag == Tag.NULL)
            {
                return GetTags().Count == 0;
            }
            else
            {
                return GetTags().Contains(tag);
            }
        }
    }
}