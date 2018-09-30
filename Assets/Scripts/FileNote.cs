using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    [CreateAssetMenu(fileName = "FileNote_", menuName = "File Note", order = 53)]
    public class FileNote : ScriptableObject
    {
        [Tooltip("Для использования регулярных выражений используйте символ <key>")]
        [TextArea]
        public string note;
        public bool forFile=true;
        public List<Sprite> sprites = new List<Sprite>();
    }
}
