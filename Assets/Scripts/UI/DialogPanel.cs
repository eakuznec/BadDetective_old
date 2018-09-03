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
        [SerializeField]
        private Text speekerName;
        [SerializeField]
        private Image speekerImage;
        [SerializeField]
        private Text phraseText;
        [SerializeField]
        private RectTransform choosesPanel;
        [SerializeField]
        private ChoosePanel choosePanel;

        public void Set(DialogPhrase phrase, Character owner)
        {
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
            List<DialogChoose> avaliableChooses = new List<DialogChoose>();
            foreach(DialogChoose choose in phrase.chooses)
            {
                bool flag = true;
                foreach(Condition condition in choose.conditions)
                {
                    if (!condition.isFulfilled())
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    avaliableChooses.Add(choose);
                }
            }
            int dif = avaliableChooses.Count - choosesPanel.childCount;
            if (dif > 0)
            {
                for(int i=0; i<dif; i++)
                {
                    Instantiate(choosePanel, choosesPanel);
                }
            }
            else if (dif < 0)
            {
                for(int i = 0; i < -dif; i++)
                {
                    Destroy(choosesPanel.GetChild(choosesPanel.childCount - 1 - i).gameObject);
                }
            }
            for(int i =0; i < avaliableChooses.Count; i++)
            {
                ChoosePanel choosePanel = choosesPanel.GetChild(i).GetComponent<ChoosePanel>();
                choosePanel.SetChoose(avaliableChooses[i], i+1);
            }
        }
    }
}
