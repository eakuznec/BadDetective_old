using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    [CreateAssetMenu(fileName = "TraitEffect_", menuName = "Trait Effect", order = 51)]
    public class TraitEffect : ScriptableObject
    {
        public TraitEffectType type;
        public int value;
    }
}
