using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class EquipmentContainer : MonoBehaviour
    {
        public Equipment equipment;
        public Detective owner;

        private void Start()
        {
            AttachEffects();
        }

        public Trait trait
        {
            get
            {
                if (equipment != null)
                {
                    return equipment.trait;
                }
                else
                {
                    return null;
                }
            }
        }

        public void AttachEffects()
        {
            if (HaveTag(Tag.NULL))
            {
                equipment.trait.AttachEffects(owner);
            }
        }

        public void DetachEffects()
        {
            if (HaveTag(Tag.NULL))
            {
                equipment.trait.DetachEffects(owner);
            }
        }

        public List<Tag> GetTags()
        {
            return equipment.trait.tags;
        }

        public bool HaveTag(Tag tag)
        {
            if (tag == Tag.NULL)
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
