using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadDetective.UI;

namespace BadDetective.Control
{
    public class MapUIControl : MonoBehaviour
    {
        public iMouseoverUI mouseover;

        private Detective dragDetective;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mouseover is DetectiveIcon)
                {
                    dragDetective = ((DetectiveIcon)mouseover).detective;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if(mouseover is EventPanel)
                {
                    if(dragDetective != null)
                    {
                        if (!((EventPanel)mouseover).questEvent.plannedDetectivesOnEvent.Contains(dragDetective))
                        {
                            ((EventPanel)mouseover).questEvent.plannedDetectivesOnEvent.Add(dragDetective);
                            ((EventPanel)mouseover).CheckDetectives();
                            dragDetective = null;
                        }
                    }
                }
                else
                {
                    dragDetective = null;
                }
            }
        }
    }
}
