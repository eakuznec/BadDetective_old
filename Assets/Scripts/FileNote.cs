using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    [CreateAssetMenu(fileName = "FileNote_", menuName = "File Note", order = 53)]
    public class FileNote : ScriptableObject
    {
        public FileNoteType type = FileNoteType.REPORT;
        [Tooltip("Для использования регулярных выражений используйте символ <key>")]
        [TextArea]
        public string note;
        public List<Sprite> sprites = new List<Sprite>();
    }

    public enum FileNoteType
    {
        REPORT,
        DIALOG,
        TECH
    }
}
