using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public interface iEffectsContainer
    {
        Team GetTeam();
        Character GetCharacterOwner();
        Quest GetQuest();
        Dialog.Dialog GetDialog();
    }

    public interface iConditionContainer
    {
        Quest GetQuest();
        Dialog.Dialog GetDialog();
    }
}
