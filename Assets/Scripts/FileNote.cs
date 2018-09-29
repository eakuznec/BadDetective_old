using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class FileNote : MonoBehaviour
    {
        [TextArea]
        public string note;
        public List<Sprite> sprites = new List<Sprite>();
        public int curSprite;
    }
}
