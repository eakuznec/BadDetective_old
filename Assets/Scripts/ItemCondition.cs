using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class ItemCondition : MonoBehaviour
    {
        public ItemConditionType type;
        public Method method;
        public Tag _tag;
        public bool boolValue;

        public int IsFulfilled(Detective detective, int weight, Team team)
        {
            int retVal = 0;
            if(type == ItemConditionType.HAVE_EMPTY_SLOT)
            {
                int emptySlot = detective.GetMaxItemSlot() - detective.blockedSlots - detective.GetEquipment().Count;
                return emptySlot * weight;
            }
            else if(type == ItemConditionType.HAVE_TAG)
            {
                if (detective.HaveTag(_tag))
                {
                    return weight;
                }
                else
                {
                    return 0;
                }
            }
            else if (type == ItemConditionType.HAVE_KNOWN_TAG)
            {
                if (detective.HaveHiddenTag(_tag, false))
                {
                    return weight;
                }
                else
                {
                    return 0;
                }
            }
            else if (type == ItemConditionType.HAVE_HIDE_TAG)
            {
                if (detective.HaveHiddenTag(_tag, true))
                {
                    return weight;
                }
                else
                {
                    return 0;
                }
            }
            else if (type == ItemConditionType.HAVE_MAX_METHOD)
            {
                bool flag = true;
                foreach(Detective teamMember in team.detectives)
                {
                    if(teamMember != detective && teamMember.GetMethodValue(method, _tag) > detective.GetMethodValue(method, _tag))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    return weight;
                }
                else
                {
                    return 0;
                }
            }
            else if (type == ItemConditionType.HAVE_MIN_METHOD)
            {
                bool flag = true;
                foreach (Detective teamMember in team.detectives)
                {
                    if (teamMember != detective && teamMember.GetMethodValue(method, _tag) < detective.GetMethodValue(method, _tag))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    return weight;
                }
                else
                {
                    return 0;
                }
            }
            return retVal;
        }
    }

    public enum ItemConditionType
    {
        HAVE_EMPTY_SLOT,
        HAVE_TAG,
        HAVE_KNOWN_TAG,
        HAVE_HIDE_TAG,
        HAVE_MAX_METHOD,
        HAVE_MIN_METHOD
    }
}
