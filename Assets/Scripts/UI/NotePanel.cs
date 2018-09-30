using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class NotePanel : MonoBehaviour
    {
        [HideInInspector]
        public FileNoteContainer note;
        [SerializeField] private RectTransform imagePanel;
        [SerializeField] private Image noteImage;
        [SerializeField] private Button prevButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Text noteText;
        private bool withImage;
        private float offset = 20;

        private RectTransform rect
        {
            get { return GetComponent<RectTransform>(); }
        }

        private void Awake()
        {
            prevButton.onClick.AddListener(PrevImage);
            nextButton.onClick.AddListener(NextImage);
        }

        public void SetNote(FileNoteContainer note)
        {
            this.note = note;
            if (note != null)
            {
                gameObject.SetActive(true);
                if (note.sprites.Count == 0)
                {
                    imagePanel.gameObject.SetActive(false);
                    noteText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.rect.width);
                    withImage = false;
                }
                else
                {
                    imagePanel.gameObject.SetActive(true);
                    withImage = true;
                    noteImage.sprite = note.sprites[note.curSprite];
                    if (note.sprites.Count > 1)
                    {
                        prevButton.gameObject.SetActive(true);
                        nextButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        prevButton.gameObject.SetActive(false);
                        nextButton.gameObject.SetActive(false);
                    }
                    noteText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (rect.rect.width - offset - imagePanel.rect.width));
                }
                noteText.text = note.GetText();
                if(note.noteType == FileNoteType.RAPORT)
                {
                    noteText.fontStyle = FontStyle.Normal;
                }
                else if (note.noteType == FileNoteType.DIALOG)
                {
                    noteText.fontStyle = FontStyle.Italic;
                }
                else if (note.noteType == FileNoteType.TECH)
                {
                    noteText.fontStyle = FontStyle.Bold;
                }

            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void NextImage()
        {
            note.curSprite++;
            if (note.curSprite >= note.sprites.Count)
            {
                note.curSprite = 0;
            }
            noteImage.sprite = note.sprites[note.curSprite];
        }

        private void Update()
        {
            float textHeight = noteText.GetComponent<RectTransform>().rect.height + offset;
            if (!withImage)
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textHeight);
            }
            else if(textHeight > imagePanel.rect.height)
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textHeight);
            }
            else
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imagePanel.rect.height);
            }
        }

        private void PrevImage()
        {
            note.curSprite--;
            if (note.curSprite < 0)
            {
                note.curSprite = note.sprites.Count-1;
            }
            noteImage.sprite = note.sprites[note.curSprite];
        }

        public float getHeight()
        {
            return rect.rect.height;
        }
    }
}
