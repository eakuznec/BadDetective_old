using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class FileNoteContainer : MonoBehaviour
    {
        private GameTime recordTime;
        private FileNote note;
        private string word;
        private List<string> words = new List<string>();
        public int curSprite;

        public void Set(FileNote fileNote, string key = "")
        {
            recordTime = Timeline.GetInstantiate().GetGameTime();
            note = fileNote;
            this.word = key;
        }

        public void Set(FileNote fileNote, List<string> keys)
        {
            recordTime = Timeline.GetInstantiate().GetGameTime();
            note = fileNote;
            this.words = keys;
        }

        public FileNoteType noteType
        {
            get
            {
                return note.type;
            }
        }

        public string GetText()
        {
            string result = note.note;
            result = result.Replace(@"<DateTime>", recordTime.ToString());
            if (word != "")
            {
                result = result.Replace(@"<key>", word);
            }
            else if (words.Count > 0)
            {
                string pattern = "";
                for (int i = 0; i < words.Count; i++)
                {
                    pattern += words[i];
                    if (i != words.Count - 1)
                    {
                        pattern += ", ";
                    }
                }
            }
            return result;
        }

        public List<Sprite> sprites
        {
            get
            {
                return note.sprites;
            }
        }

        public static FileNoteContainer Create(FileNote fileNote, Transform parent, string key = "")
        {
            GameObject goNote = new GameObject(fileNote.name);
            FileNoteContainer fileNoteContainer = goNote.AddComponent<FileNoteContainer>();
            fileNoteContainer.Set(fileNote, key);
            fileNoteContainer.transform.parent = parent;
            return fileNoteContainer;
        }

        public static FileNoteContainer Create(FileNote fileNote, Transform parent, List<string> keys)
        {
            GameObject goNote = new GameObject(fileNote.name);
            FileNoteContainer fileNoteContainer = goNote.AddComponent<FileNoteContainer>();
            fileNoteContainer.Set(fileNote, keys);
            fileNoteContainer.transform.parent = parent;
            return fileNoteContainer;
        }
    }
}
