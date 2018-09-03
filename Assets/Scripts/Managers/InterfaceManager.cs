using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class InterfaceManager : MonoBehaviour
    {
        private static InterfaceManager instance;
        public ActivitiesPanel activitiesPanel;
        public DetectiveRow detectiveRow;
        public DialogPanel dialogPanel;
        public FilesPanel filesPanel;
        [Header("Sprites")]
        public Sprite officePictogram;
        public Sprite homePictogram;
        public Sprite eventPictogram;
        public Sprite walkPictogram;
        
        public static InterfaceManager GetInstantiate()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InterfaceManager>();
            }
            if (instance == null)
            {
                Game game = Game.GetInstantiate();
                instance = Instantiate(game.interfaceManager);
            }
            return instance;
        }

        private void Awake()
        {
            GetInstantiate();
            DontDestroyOnLoad(instance);
        }

        private void Start()
        {
            detectiveRow.ResetRow();
        }
    }
}
