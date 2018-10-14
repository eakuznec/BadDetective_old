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
            result = result.Replace(@"<AgencyName>", Agency.GetInstantiate().agencyName);
            if (word!="" && word!=null)
            {
                result = result.Replace(@"<key>", word);
            }
            else if (words.Count > 0)
            {
                List<string> array = new List<string>();
                for (int i = 0; i < words.Count; i++)
                {
                    if (result.Contains(string.Format("<{0}>", i)))
                    {
                        result = result.Replace(string.Format("<{0}>", i), words[i]);
                    }
                    else
                    {
                        array.Add(words[i]);
                    }
                }
                if (array.Count > 0)
                {
                    string pattern = "";
                    for(int i = 0; i < array.Count; i++)
                    {
                        pattern += array[i];
                        if (i < array.Count - 1)
                        {
                            pattern += ", ";
                        }
                    }
                    result = result.Replace(@"<array>", pattern);
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
