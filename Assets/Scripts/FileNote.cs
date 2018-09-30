using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class FileNote : MonoBehaviour
    {
        [Tooltip("Для использования регулярных выражений используйте символ <key>")]
        [TextArea]
        public string note;
        public bool forFile=true;
        public List<Sprite> sprites = new List<Sprite>();
    }
}
