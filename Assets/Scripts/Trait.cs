using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Trait : MonoBehaviour
    {
        [HideInInspector]
        public Detective owner;

        public string traitName;
        [TextArea]
        public string traitDescription;
        [Space (10)]
        public List<TraitEffect> traitEffects = new List<TraitEffect>();
        [Space(10)]
        public List<Tag> tags = new List<Tag>();
        [Space (10)]
        public TraitType type;
        [HideInInspector]
        public bool isHidden;
        [HideInInspector]
        public Trait mimicryTrait;

        [HideInInspector]
        public float liveTime;
        [HideInInspector]
        public int removePoint;

        private void Start()
        {
            if (type == TraitType.TEMPORARY)
            {
                CreateTemporaryTraitAction();
            }
            AttachEffects();
        }

        public int GetTotalEffectValue(TraitEffectType type)
        {
            int retVal = 0;
            foreach(TraitEffect effect in traitEffects)
            {
                if(effect.type == type)
                {
                    retVal += effect.value;
                }
            }
            return retVal;
        }

        public void CreateTemporaryTraitAction()
        {
            Timeline timeline = Timeline.GetInstantiate();
            GameObject goAction = new GameObject(string.Format("TimelineAction_TamporaryTrait_{0}", traitName));
            goAction.transform.parent = timeline.transform;
            TimelineAction traitAction = goAction.AddComponent<TimelineAction>();
            traitAction.actionType = TimelineActionType.TEMPORARY_TRAIT;
            traitAction.trait = this;
            traitAction.timer = timeline.GetTime() + liveTime;
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

        }

        public void DetachEffects()
        {

        }
    }
}
