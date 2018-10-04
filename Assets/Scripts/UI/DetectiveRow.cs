using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class DetectiveRow : MonoBehaviour
    {
        [SerializeField]
        private DetectiveIcon detectiveIcon;
        private List<DetectiveIcon> icons = new List<DetectiveIcon>();

        public void ResetRow()
        {
            List<Detective> detectives = Agency.GetInstantiate().GetDetectives();
            int dif = detectives.Count - icons.Count;
            if (dif > 0)
            {
                for(int i=0; i < dif; i++)
                {
                    icons.Add(Instantiate(detectiveIcon, transform));
                }
            }
            else if (dif < 0)
            {
                for (int i = 0; i < dif; i++)
                {
                    Destroy(icons[icons.Count - i - 1]);
                }
            }
            for(int i=0; i < icons.Count; i++)
            {
                icons[i].detective = detectives[i];
            }
        }
    }
}
