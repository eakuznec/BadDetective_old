using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public interface iEffectsContainer
    {
        Team GetTeam();
        Character GetCharacterOwner();
    }
}
