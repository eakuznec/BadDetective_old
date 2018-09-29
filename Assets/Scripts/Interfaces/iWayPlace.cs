using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public interface iWayPlace: iActivityPlace
    {
        float GetTimeToWay();
    }
}