using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.Dialog
{
    public class DialogChoose : MonoBehaviour
    {
        public ChooseType type;
        public string chooseText;
        [HideInInspector]
        public List<Condition> conditions = new List<Condition>();
        [HideInInspector]
        public List<Effect> effects = new List<Effect>();
        public List<DialogLink> links = new List<DialogLink>();
        [HideInInspector]
        public Rect nodePosition;
        public bool isOnce;
        public bool realized;

        private void Awake()
        {
            realized = false;
        }

        public bool isShowned()
        {
            if (!isOnce)
            {
                return true;
            }
            else
            {
                return !realized;
            }
        }
    }

    public enum ChooseType
    {
        NONE,
        CONTINUE,
        END
    }
}
