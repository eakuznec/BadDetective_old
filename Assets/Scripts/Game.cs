using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadDetective.Control;
using BadDetective.Dialog;
using BadDetective.UI;

namespace BadDetective
{
    public class Game : MonoBehaviour
    {
        private static Game instance;
        private GameState state;
        [Header("Instances")]
        public Agency newAgency;
        public Timeline timeline;
        public DetectiveManager detectiveManager;
        public TraitManager traitManager;
        public ItemManager itemManager;
        public QuestManager questManager;
        public MapManager mapManager;
        public DialogManager dialogManager;
        public InterfaceManager interfaceManager;
        public ControlManager controlManager;

        [Header("Assets")]
        public Material materialForWays;

        public static Game GetInstantiate()
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Game>();
            }
            if(instance == null)
            {
                instance = Instantiate(Resources.Load("Game")) as Game;
            }
            return instance;
        }            
        
        private void Awake()
        {
            GetInstantiate();

            DontDestroyOnLoad(instance);
            ControlManager.GetInstantiate();
            DetectiveManager.GetInstantiate();
            TraitManager.GetInstantiate();
            ItemManager.GetInstantiate();
            QuestManager.GetInstantiate();
            MapManager.GetInstantiate();
            DialogManager.GetInstantiate();
            Agency.GetInstantiate();
        }

        public GameState GetGameState()
        {
            return state;
        }

        public void ChangeGameState(GameState newState)
        {
            InterfaceManager.GetInstantiate().detectiveRollover.Hide();
            InterfaceManager.GetInstantiate().activitiesRollover.Hide();
            state = newState;
        }
    }
}
