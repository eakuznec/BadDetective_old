using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BadDetective
{
    public class QuestEditor : EditorWindow
    {
        public static QuestEditor editor;
        public GameObject selectGameObject;

        public static Quest quest;

        public Vector2 mousePos;

        [MenuItem("Window/Quest Editor")]
        public static void ShowEditor()
        {
            editor = (QuestEditor)EditorWindow.GetWindow(typeof(QuestEditor), false, "Quest Editor");
            
        }

        private void Awake()
        {
            if(quest == null)
            {
                GameObject newQuest = new GameObject("Quest");
                quest = newQuest.AddComponent<Quest>();
            }
            else
            {

            }
        }

        void OnGUI()
        {
            Control();
        }
        private void Control()
        {
            Event e = Event.current;
            mousePos = e.mousePosition;
            if(e.type == EventType.MouseUp)
            {
                selectGameObject = quest.gameObject;

                Selection.activeGameObject = selectGameObject;
            }
        }
    }
}
