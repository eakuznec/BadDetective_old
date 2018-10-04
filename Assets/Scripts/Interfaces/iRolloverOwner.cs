using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BadDetective
{
    public interface iRolloverOwner : IPointerEnterHandler, IPointerExitHandler
    {
        RectTransform GetRectTransform();
    }
}
