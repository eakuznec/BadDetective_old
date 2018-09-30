using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class TraitContainer : MonoBehaviour
    {
        public Trait trait;
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
        public bool isDettached;

        [HideInInspector]
        public bool showInInterface = false;

        private void Start()
        {
            owner = GetComponentInParent<Detective>();
            if (trait.type == TraitType.TEMPORARY)
            {
                CreateTemporaryTraitAction();
            }
            else if(trait.type == TraitType.REMOVABLE)
            {
                removePoint = trait.removePoint;
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
                trait.AttachEffects(owner);
            }
        }

        public void DetachEffects()
        {
            if (HaveTag(Tag.NULL))
            {
                trait.DetachEffects(owner);
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