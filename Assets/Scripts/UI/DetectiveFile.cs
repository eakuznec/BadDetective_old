using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class DetectiveFile : MonoBehaviour
    {
        private Detective detective;
        private GameState prevState;
        private PageType curPage;

        [Header("Buttons")]
        [SerializeField] private Button closeButton;
        [SerializeField] private Button statsTab;
        [SerializeField] private Button equipmentTab;
        [SerializeField] private Button bioTab;
        [Header("StatsPanel")]
        [SerializeField] private RectTransform statsPanel;
        [SerializeField] private Text detectiveName;
        [SerializeField] private Image portrait;
        [SerializeField] private Text fenotype;
        [SerializeField] private Text sex;
        [SerializeField] private Text age;
        [SerializeField] private Text salary;
        [SerializeField] private Text health;
        [SerializeField] private Text stress;
        [SerializeField] private Text experience;
        [SerializeField] private Text loyalty;
        [SerializeField] private Text confidence;
        [SerializeField] private Text brutal;
        [SerializeField] private Text careful;
        [SerializeField] private Text diplomatic;
        [SerializeField] private Text science;
        [SerializeField] private RectTransform traitPanel;
        [SerializeField] private TraitLine traitLine;
        [Header("EquipmentPanel")]
        [SerializeField] private RectTransform equipmentPanel;
        [Header("BioPanel")]
        [SerializeField] private RectTransform bioPanel;
        [SerializeField] private RectTransform bioPage1;
        [SerializeField] private RectTransform bioPage2;
        [SerializeField] private Button prevPageButton;
        [SerializeField] private Button nextPageButton;
        private int curPageNum;
        [SerializeField] private NotePanel notePanel;
        private bool checkBio;
        private bool secondFrame;
        private Dictionary<int, List<FileNoteContainer>> bioPages = new Dictionary<int, List<FileNoteContainer>>();

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
            statsTab.onClick.AddListener(delegate { OpenPage(PageType.STATS); });
            equipmentTab.onClick.AddListener(delegate { OpenPage(PageType.EQUIPMENT); });
            bioTab.onClick.AddListener(delegate { OpenPage(PageType.BIO); });
            prevPageButton.onClick.AddListener(PrevPage);
            nextPageButton.onClick.AddListener(NextPage);
        }

        public void Open(Detective detective)
        {
            Game game = Game.GetInstantiate();
            this.detective = detective;
            prevState = game.GetGameState();
            game.ChangeGameState(GameState.IN_DETECTIVE_FILE);
            SetDetective();
            gameObject.SetActive(true);
            OpenPage(curPage);
        }

        public void Close()
        {
            Game game = Game.GetInstantiate();
            game.ChangeGameState(prevState);
            gameObject.SetActive(false);
        }

        private void OpenPage(PageType pageType)
        {
            curPage = pageType;
            if (pageType == PageType.STATS)
            {
                statsPanel.gameObject.SetActive(true);
            }
            else if(pageType == PageType.EQUIPMENT)
            {
                statsPanel.gameObject.SetActive(false);
                equipmentPanel.gameObject.SetActive(true);
            }
            else if (pageType == PageType.BIO)
            {
                statsPanel.gameObject.SetActive(false);
                equipmentPanel.gameObject.SetActive(false);
                bioPanel.gameObject.SetActive(true);
            }
        }

        private void SetDetective()
        {
            detectiveName.text = detective.characterName;
            portrait.sprite = detective.characterAvatar;
            sex.text = string.Format("Sex: {0}", detective.sex.ToString());
            age.text = string.Format("Age: {0}", detective.age.ToString());
            salary.text = string.Format("Salary: {0} / week", detective.salary.ToString());
            health.text = string.Format("Health: {0} {1}/{2}/{3}", detective.GetHealthDescription(), detective.minHealth, (int)detective.curHealth, detective.maxHealth);
            loyalty.text = string.Format("Loyalty: {0}/{1}/{2}", detective.minLoyalty, (int)detective.curLoyalty, detective.maxLoyalty);
            confidence.text = string.Format("Confidence: {0}/{1}/{2}", detective.minConfidence, (int)detective.curConfidence, detective.maxConfidence);
            // experience.text = string.Format("Experience: {0}/{1}/{2}", detective., (int)detective.curLoyalty, detective.maxLoyalty);
            stress.text = string.Format("Stress: {0} {1}/{2}/{3}", detective.GetStressDescription(), detective.minStress, (int)detective.curStress, detective.maxStress);
            brutal.text = string.Format("- {0}: {1} {2}/{3}", Method.Brutal.ToString(), detective.GetMethodDescription(Method.Brutal), detective.GetMethodValue(Method.Brutal), detective.GetMaxMethodValue(Method.Brutal));
            careful.text = string.Format("- {0}: {1} {2}/{3}", Method.Careful.ToString(), detective.GetMethodDescription(Method.Careful), detective.GetMethodValue(Method.Careful), detective.GetMaxMethodValue(Method.Careful));
            diplomatic.text = string.Format("- {0}: {1} {2}/{3}", Method.Diplomatic.ToString(), detective.GetMethodDescription(Method.Diplomatic), detective.GetMethodValue(Method.Diplomatic), detective.GetMaxMethodValue(Method.Diplomatic));
            science.text = string.Format("- {0}: {1} {2}/{3}", Method.Scientific.ToString(), detective.GetMethodDescription(Method.Scientific), detective.GetMethodValue(Method.Scientific), detective.GetMaxMethodValue(Method.Scientific));
            List<TraitContainer> traits = new List<TraitContainer>();
            for (int i=0; i<traitPanel.childCount; i++)
            {
                TraitLine line = traitPanel.GetChild(i).GetComponent<TraitLine>();
                if (line != null)
                {
                    if (!detective.traits.Contains(line.trait))
                    {
                        Destroy(line.gameObject);
                    }
                    else
                    {
                        traits.Add(line.trait);
                    }
                }
            }
            foreach (TraitContainer trait in detective.traits)
            {
                if (trait.trait.category == TraitCategory.FENOTYPE)
                {
                    fenotype.text = string.Format("Fenotype: {0}", trait.trait.traitName);
                }
                else if(!traits.Contains(trait))
                {
                    TraitLine line = Instantiate(traitLine, traitPanel);
                    line.SetTrait(trait);
                }
            }
            while(bioPage1.childCount < detective.notes.Count)
            {
                Instantiate(notePanel, bioPage1);
            }
            for(int i=0; i< bioPage1.childCount; i++)
            {
                if (i < detective.notes.Count)
                {
                    bioPage1.GetChild(i).GetComponent<NotePanel>().SetNote(detective.notes[i]);
                }
                else
                {
                    bioPage1.GetChild(i).GetComponent<NotePanel>().SetNote(null);
                }
            }
            checkBio = false;
        }

        private void Update()
        {
            if (secondFrame)
            {
                bioPages.Clear();
                int counter = 0;
                float h = 0;
                for(int i=0; i< detective.notes.Count; i++)
                {
                    if(h+bioPage1.GetChild(i).GetComponent<NotePanel>().getHeight() <= bioPage1.rect.height)
                    {
                        if (!bioPages.ContainsKey(counter))
                        {
                            bioPages.Add(counter, new List<FileNoteContainer>());
                        }
                        bioPages[counter].Add(detective.notes[i]);
                        h += bioPage1.GetChild(i).GetComponent<NotePanel>().getHeight();
                    }
                    else
                    {
                        counter++;
                        if (!bioPages.ContainsKey(counter))
                        {
                            bioPages.Add(counter, new List<FileNoteContainer>());
                        }
                        bioPages[counter].Add(detective.notes[i]);
                        h = bioPage1.GetChild(i).GetComponent<NotePanel>().getHeight();
                    }
                }
                SetBioPage(0);
                checkBio=true;
                secondFrame = false;
            }
            if (!checkBio)
            {
                secondFrame = true;
            }
        }

        private void SetBioPage(int num)
        {
            curPageNum = num;
            if(curPageNum == 0)
            {
                prevPageButton.gameObject.SetActive(false);
            }
            else
            {
                prevPageButton.gameObject.SetActive(true);
            }
            if (!bioPages.ContainsKey(curPageNum + 2))
            {
                nextPageButton.gameObject.SetActive(false);
                            }
            else
            {
                nextPageButton.gameObject.SetActive(true);
            }
            if (bioPages.ContainsKey(num))
            {
                while (bioPage1.childCount < bioPages[num].Count)
                {
                    Instantiate(notePanel, bioPage1);
                }
                for(int i = 0; i < bioPage1.childCount; i++)
                {
                    if (i < bioPages[num].Count)
                    {
                        bioPage1.GetChild(i).GetComponent<NotePanel>().SetNote(bioPages[num][i]);
                    }
                    else
                    {
                        bioPage1.GetChild(i).GetComponent<NotePanel>().SetNote(null);
                    }
                } 
            }
            else
            {
                for(int i=0; i < bioPage1.childCount; i++)
                {
                    bioPage1.GetChild(i).GetComponent<NotePanel>().SetNote(null);
                }
            }
            if (bioPages.ContainsKey(num + 1))
            {
                while (bioPage2.childCount < bioPages[num+1].Count)
                {
                    Instantiate(notePanel, bioPage2);
                }
                for (int i = 0; i < bioPage2.childCount; i++)
                {
                    if (i < bioPages[num+1].Count)
                    {
                        bioPage2.GetChild(i).GetComponent<NotePanel>().SetNote(bioPages[num+1][i]);
                    }
                    else
                    {
                        bioPage2.GetChild(i).GetComponent<NotePanel>().SetNote(null);
                    }
                }
            }
            else
            {
                for (int i = 0; i < bioPage2.childCount; i++)
                {
                    bioPage2.GetChild(i).GetComponent<NotePanel>().SetNote(null);
                }
            }
        }

        private void NextPage()
        {
            SetBioPage(++curPageNum);
        }

        private void PrevPage()
        {
            SetBioPage(--curPageNum);
        }

        private enum PageType
        {
            STATS,
            EQUIPMENT,
            BIO
        }
    }
}
