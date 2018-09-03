using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.Dialog
{
    public class DialogLink : MonoBehaviour
    {
        public DialogChoose input;
        public DialogPhrase output;
        public List<Condition> conditions = new List<Condition>();
        [HideInInspector]
        public Rect nodePosition;

        public void Delete()
        {
            if (input != null)
            {
                input.links.Remove(this);
            }
            Debug.Log(this);

            //if (this != null && this.gameObject != null)
            //{
            //    DestroyImmediate(this.gameObject);
            //}
        }
    }
}
