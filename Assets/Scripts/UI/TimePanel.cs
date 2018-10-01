using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class TimePanel : MonoBehaviour
    {
        public Text timeText;
        public Image timeSpeedImage;
        public Sprite pauseSprite;
        public Sprite playSprite;
        public Sprite fastPlaySprite;
        public Sprite veryFastPlaySprite;
        
        public void CheckTime(GameTime time)
        {
            timeText.text = time.ToString();
        }

        public void CheckTimeSpeed(TimelineState state)
        {
            if(state == TimelineState.STOP)
            {
                timeSpeedImage.sprite = pauseSprite;
            }
            else if(state == TimelineState.PLAY)
            {
                timeSpeedImage.sprite = playSprite;
            }
            else if (state == TimelineState.x2)
            {
                timeSpeedImage.sprite = fastPlaySprite;
            }
            else if (state == TimelineState.x4)
            {
                timeSpeedImage.sprite = veryFastPlaySprite;
            }
        }

    }
}