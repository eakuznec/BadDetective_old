using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.Control
{
    public class ControlManager : MonoBehaviour
    {
        private static ControlManager instance;

        public iMouseoverUI mouseover
        {
            get
            {
                return mapUIControl.mouseover;
            }
            set
            {
                mapUIControl.mouseover = value;
            }
        }

        private TimelineControl timelineControl;
        private MapControl mapControl;
        private MapUIControl mapUIControl;

        public static ControlManager GetInstantiate()
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ControlManager>();
            }
            if(instance == null)
            {
                Game game = Game.GetInstantiate();
                instance = Instantiate(game.controlManager);
            }
            return instance;
        }

        private void Awake()
        {
            GetInstantiate();
            DontDestroyOnLoad(instance);

            if(timelineControl == null)
            {
                timelineControl = FindObjectOfType<TimelineControl>();
            }
            if(timelineControl == null)
            {
                timelineControl = gameObject.AddComponent<TimelineControl>();
            }

            if (mapControl == null)
            {
                mapControl = FindObjectOfType<MapControl>();
            }
            if (mapControl == null)
            {
                mapControl = gameObject.AddComponent<MapControl>();
            }

            if (mapUIControl == null)
            {
                mapUIControl = FindObjectOfType<MapUIControl>();
            }
            if (mapUIControl == null)
            {
                mapUIControl = gameObject.AddComponent<MapUIControl>();
            }
        }
    }
}
