using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public interface iBoolData
    {
        bool GetResult();

        void SetCheckDataNode(bool check);
        bool GetChackDataNode();
        void SetDataInputLink(BaseFunction input);
        void SetDataOutputLink(BaseFunction output);
        void RemoveDataInput(BaseFunction input);
        void RemoveDataOutput(BaseFunction output);
    }
}
