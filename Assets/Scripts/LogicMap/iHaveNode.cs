using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public interface iHaveNode
    {
        Rect GetWindowRect();
        void SetWindowRect(Rect rect);
    }
}
