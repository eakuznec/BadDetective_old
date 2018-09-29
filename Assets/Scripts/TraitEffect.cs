using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    [CreateAssetMenu(fileName = "TraitEffect_", menuName = "Trait Effect", order = 52)]
    public class TraitEffect : ScriptableObject
    {
        public TraitEffectType type;
        public Trait trait;
        public int value;
        public float floatValue;
    }
}
