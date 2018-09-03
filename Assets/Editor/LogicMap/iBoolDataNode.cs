using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public interface iBoolDataNode
    {
        BaseFunction GetDataFunction();
        bool GetSelectDataLink();
        void SetSelectDataLink(bool link);
        void SetDataOutputLink();
    }
}
