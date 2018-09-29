using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.LogicMap
{
    public interface iLogicMapContainer: iEffectsContainer, iConditionContainer
    {
        List<LogicMap> GetLogicMaps();
        List<string> GetLogicMapNames();
    }
}