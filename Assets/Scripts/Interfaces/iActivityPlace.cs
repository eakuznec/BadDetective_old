using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public interface iActivityPlace
    {
        string GetPlaceName();
        float GetStressPerHour();
        PointOnMap GetPoint();
    }
}
