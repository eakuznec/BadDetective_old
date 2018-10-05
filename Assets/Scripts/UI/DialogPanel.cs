using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BadDetective.Dialog;

namespace BadDetective.UI
{
    public class DialogPanel : MonoBehaviour
    {
        private DialogPhrase phrase;
        [SerializeField] private RectTransform dialogPanel;
        [SerializeField] private RectTransform phrasePanel;
        [SerializeField] private RectTransform scrollView;
        [SerializeField]
        private Text speekerName;
        [SerializeField]
        private Image speekerImage;
        [SerializeField]
        private Text phraseText;
        [SerializeField] private RectTransform reportPanel;
        [SerializeField] private RectTransform portraitsPanel;
        [SerializeField] private DetectiveReportIcon detectiveIcon;
        [SerializeField] private Text questName;
        [SerializeField] private Button questFileButton;
        [SerializeField] private RectTransform notesScrollView;
        [SerializeField] private RectTransform notesPanel;
        [SerializeField] private NotePanel notePanel;
        [SerializeField]
        private RectTransform choosesPanel;
        [SerializeField]
        private ChoosePanel choosePanel;

        private Quest quest;
        private Team team;

        private void Awake()
        {
            questFileButton.onClick.AddListener(ShowQuestFile);
        }

        public void Set(DialogPhrase phrase, Character owner, Quest quest)
        {
            phrasePanel.gameObject.SetActive(true);
            reportPanel.gameObject.SetActive(false);
            this.phrase = phrase;
            if (phrase.speekerType == DialogSpeekerType.OWNER)
            {
                speekerName.text = owner.characterName;
                speekerImage.sprite = owner.characterAvatar;
            }
            else
            {
                speekerName.text = phrase.speeker.characterName;
                speekerImage.sprite = phrase.speeker.characterAvatar;
            }
            phraseText.text = phrase.phraseText;
            PrepareChooses();
        }

        public void SetReport(DialogPhrase phrase, List<FileNoteContainer> notes, Team team, Quest quest)
        {
            phrasePanel.gameObject.SetActive(false);
            reportPanel.gameObject.SetActive(true);
            this.quest = quest;
            this.team = team;
            this.phrase = phrase;
            ResetDetectives();
            questName.text = quest.questName;
            int dif = notesPanel.childCount - notes.Count;
            if (dif > 0)
            {
                for(int i = 0; i < dif; i++)
                {
                    Destroy(notesPanel.GetChild(notesPanel.childCount - 1 - i).gameObject);
                }
            }
            else if (dif < 0)
            {
                for(int i=0; i < -dif; i++)
                {
                    Instantiate(notePanel, notesPanel);
                }
            }
            for(int i=0; i < notes.Count; i++)
            {
                notesPanel.GetChild(i).GetComponent<NotePanel>().SetNote(notes[i]);
            }
            team.Report();
            PrepareChooses();
        }

        private void PrepareChooses()
        {
            List<DialogChoose> avaliableChooses = new List<DialogChoose>();
            foreach (DialogChoose choose in phrase.chooses)
            {
                bool flag = true;
                foreach (Condition condition in choose.conditions)
                {
                    if (!condition.isFulfilled(quest))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag && choose.isShowned())
                {
                    avaliableChooses.Add(choose);
                }
            }
            int dif = avaliableChooses.Count - choosesPanel.childCount;
            if (dif > 0)
            {
                for (int i = 0; i < dif; i++)
                {
                    Instantiate(choosePanel, choosesPanel);
                }
            }
            else if (dif < 0)
            {
                for (int i = 0; i < -dif; i++)
                {
                    Destroy(choosesPanel.GetChild(choosesPanel.childCount - 1 - i).gameObject);
                }
            }
            for (int i = 0; i < avaliableChooses.Count; i++)
            {
                ChoosePanel choosePanel = choosesPanel.GetChild(i).GetComponent<ChoosePanel>();
                choosePanel.SetChoose(avaliableChooses[i], i + 1);
            }
        }

        private void Update()
        {
            float chooseHeight = choosesPanel.GetChild(0).GetComponent<RectTransform>().rect.height;
            choosesPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (chooseHeight+10) * choosesPanel.childCount);
            if(phrase.type == PhraseType.DIALOG_PHRASE)
            {
                phrasePanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dialogPanel.rect.height - choosesPanel.rect.height);
                float phraseHeight = phraseText.rectTransform.rect.height + scrollView.rect.yMin + phrasePanel.rect.height - scrollView.rect.yMax;
                if(phraseHeight < speekerImage.rectTransform.rect.yMax + 10)
                {
                    phrasePanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, speekerImage.rectTransform.rect.yMax + 10);
                }
                else if(phraseHeight < phrasePanel.rect.height)
                {
                    phrasePanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, phraseHeight);
                }
            }
            else if(phrase.type == PhraseType.REPORT)
            {
                reportPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dialogPanel.rect.height - choosesPanel.rect.height);
                float notesHeight = 0;
                for(int i=0;i<notesPanel.childCount;i++)
                {
                    notesHeight += notesPanel.GetChild(i).GetComponent<RectTransform>().rect.height;
                }
                notesPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, notesHeight);
                if (notesScrollView.rect.height > notesHeight)
                {
                    reportPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, notesScrollView.rect.yMin + reportPanel.rect.height - notesScrollView.rect.yMax + notesHeight);
                }
            }
        }

        private void ShowQuestFile()
        {
            InterfaceManager.GetInstantiate().questFile.Open(quest);
        }

        public void ResetDetectives()
        {
            List<Detective> detectives = new List<Detective>();
            Detective leader = team.GetLeader();
            detectives.Add(leader);
            foreach (Detective detective in team.detectives)
            {
                if (detective != leader)
                {
                    detectives.Add(detective);
                }
            }
            int dif = portraitsPanel.childCount - detectives.Count;
            if (dif > 0)
            {
                for (int i = 0; i < dif; i++)
                {
                    Destroy(portraitsPanel.GetChild(portraitsPanel.childCount - 1 - i).gameObject);
                }
            }
            else if (dif < 0)
            {
                for (int i = 0; i < -dif; i++)
                {
                    Instantiate(detectiveIcon, portraitsPanel);
                }
            }
            for (int i = 0; i < detectives.Count; i++)
            {
                DetectiveReportIcon icon = portraitsPanel.GetChild(i).GetComponent<DetectiveReportIcon>();
                icon.detective = detectives[i];
                icon.canReturnHome = detectives.Count > 1;
            }
        }

        public DialogChoose GetChoose(int num)
        {
            if (choosesPanel.childCount < num)
            {
                return null;
            }
            else
            {
                return choosesPanel.GetChild(num - 1).GetComponent<ChoosePanel>().GetChoose();
            }
        }
    }
}
