using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class EquipmentContainer : MonoBehaviour
    {
        public Equipment equipment;
        public Detective owner;
        private bool attach;

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
            if (!attach)
            {
                if (HaveTag(Tag.NULL))
                {
                    equipment.trait.AttachEffects(owner);
                }
                attach = true;
            }
        }

        public void DetachEffects()
        {
            if (attach)
            {
                if (HaveTag(Tag.NULL))
                {
                    equipment.trait.DetachEffects(owner);
                }
                attach = false;
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
