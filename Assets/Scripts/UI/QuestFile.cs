using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class QuestFile : MonoBehaviour
    {
        private Quest quest;
        private GameState prevState;

        [Header("Buttons")]
        [SerializeField] private Button closeButton;
        [SerializeField] private Button statsTab;
        [Header("NotesPanel")]
        [SerializeField] private RectTransform statsPage;
        [SerializeField] private Text questName;
        [SerializeField] private Text mainState;
        [SerializeField] private Text clientName;
        [SerializeField] private Text shortDescription;
        [SerializeField] private Text deadline;
        [SerializeField] private Toggle registrasteToogle;
        [SerializeField] private RectTransform questObjectivesPanel;
        [SerializeField] private QuestObjectivePanel questObjectivePanel;
        [SerializeField] private RectTransform notePageL;
        [SerializeField] private RectTransform notePageR;
        [SerializeField] private Button prevPageButton;
        [SerializeField] private Button nextPageButton;
        private int curPageNum;
        [SerializeField] private NotePanel notePanel;
        private bool checkNote;
        private bool secondFrame;
        private Dictionary<int, List<FileNote>> notePages = new Dictionary<int, List<FileNote>>();

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
            statsTab.onClick.AddListener(delegate { SetNotePage(0); });
            prevPageButton.onClick.AddListener(PrevPage);
            nextPageButton.onClick.AddListener(NextPage);
        }

        public void Open(Quest quest)
        {
            Game game = Game.GetInstantiate();
            this.quest = quest;
            prevState = game.GetGameState();
            game.ChangeGameState(GameState.IN_QUEST_FILE);
            SetQuest();
            gameObject.SetActive(true);
            SetNotePage(0);
        }

        public void Close()
        {
            Game game = Game.GetInstantiate();
            game.ChangeGameState(prevState);
            gameObject.SetActive(false);
        }

        private void SetQuest()
        {
            questName.text = quest.questName;
            mainState.text = quest.mainState.ToString();
            clientName.text = string.Format("Client: {0}", quest.client.characterName);
            shortDescription.text = quest.shortDescription;
            if (quest.withDeadline)
            {
                deadline.gameObject.SetActive(false);
            }
            else
            {
                deadline.gameObject.SetActive(false);
            }
            registrasteToogle.isOn = quest.registrated;
            List<QuestObjective> objectives = new List<QuestObjective>();
            foreach(QuestObjective objective in quest.questObjectives)
            {
                if(objective.state != MainState.NotStarted)
                {
                    objectives.Add(objective);
                }
            }
            int dif = questObjectivesPanel.childCount - objectives.Count;
            if (dif > 0)
            {
                for(int i = 0; i < dif; i++)
                {
                    Destroy(questObjectivesPanel.GetChild(questObjectivesPanel.childCount - 1 - i));
                }
            }
            else if(dif < 0)
            {
                for(int i=0; i<-dif; i++)
                {
                    Instantiate(questObjectivePanel, questObjectivesPanel);
                }
            }
            for(int i=0; i < objectives.Count;i++)
            {
                questObjectivesPanel.GetChild(i).GetComponent<QuestObjectivePanel>().SetObjective(objectives[i]);
            }

            while (notePageR.childCount < quest.notes.Count)
            {
                Instantiate(notePanel, notePageR);
            }
            for (int i = 0; i < notePageR.childCount; i++)
            {
                if (i < quest.notes.Count)
                {
                    notePageR.GetChild(i).GetComponent<NotePanel>().SetNote(quest.notes[i]);
                }
                else
                {
                    notePageR.GetChild(i).GetComponent<NotePanel>().SetNote(null);
                }
            }
            checkNote = false;
        }

        private void Update()
        {
            if (secondFrame)
            {
                notePages.Clear();
                int counter = 0;
                float h = 0;
                for (int i = 0; i < quest.notes.Count; i++)
                {
                    if (h + notePageR.GetChild(i).GetComponent<NotePanel>().getHeight() <= notePageR.rect.height)
                    {
                        if (!notePages.ContainsKey(counter))
                        {
                            notePages.Add(counter, new List<FileNote>());
                        }
                        notePages[counter].Add(quest.notes[i]);
                        h += notePageR.GetChild(i).GetComponent<NotePanel>().getHeight();
                    }
                    else
                    {
                        counter++;
                        if (!notePages.ContainsKey(counter))
                        {
                            notePages.Add(counter, new List<FileNote>());
                        }
                        notePages[counter].Add(quest.notes[i]);
                        h = notePageR.GetChild(i).GetComponent<NotePanel>().getHeight();
                    }
                }
                SetNotePage(0);
                checkNote = true;
                secondFrame = false;
            }
            if (!checkNote)
            {
                secondFrame = true;
            }
        }


        private void SetNotePage(int num)
        {
            curPageNum = num;
            if (curPageNum == 0)
            {
                statsPage.gameObject.SetActive(true);
                prevPageButton.gameObject.SetActive(false);
            }
            else
            {
                statsPage.gameObject.SetActive(false);
                prevPageButton.gameObject.SetActive(true);
            }
            if (!notePages.ContainsKey(curPageNum + 2))
            {
                nextPageButton.gameObject.SetActive(false);
            }
            else
            {
                nextPageButton.gameObject.SetActive(true);
            }
            if (notePages.ContainsKey(num))
            {
                while (notePageR.childCount < notePages[num].Count)
                {
                    Instantiate(notePanel, notePageR);
                }
                for (int i = 0; i < notePageR.childCount; i++)
                {
                    if (i < notePages[num].Count)
                    {
                        notePageR.GetChild(i).GetComponent<NotePanel>().SetNote(notePages[num][i]);
                    }
                    else
                    {
                        notePageR.GetChild(i).GetComponent<NotePanel>().SetNote(null);
                    }
                }
            }
            else
            {
                for (int i = 0; i < notePageR.childCount; i++)
                {
                    notePageR.GetChild(i).GetComponent<NotePanel>().SetNote(null);
                }
            }
            if (notePages.ContainsKey(num - 1))
            {
                while (notePageL.childCount < notePages[num - 1].Count)
                {
                    Instantiate(notePanel, notePageL);
                }
                for (int i = 0; i < notePageL.childCount; i++)
                {
                    if (i < notePages[num - 1].Count)
                    {
                        notePageL.GetChild(i).GetComponent<NotePanel>().SetNote(notePages[num - 1][i]);
                    }
                    else
                    {
                        notePageL.GetChild(i).GetComponent<NotePanel>().SetNote(null);
                    }
                }
            }
            else
            {
                for (int i = 0; i < notePageL.childCount; i++)
                {
                    notePageL.GetChild(i).GetComponent<NotePanel>().SetNote(null);
                }
            }
        }

        private void NextPage()
        {
            SetNotePage(++curPageNum);
        }

        private void PrevPage()
        {
            SetNotePage(--curPageNum);
        }
    }
}
