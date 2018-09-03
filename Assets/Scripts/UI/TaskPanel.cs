using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class TaskPanel : MonoBehaviour
    {
        private QuestTask _task;
        public Toggle taskToggle;
        [SerializeField]
        private Text taskLabel;

        private void Awake()
        {
            taskToggle.onValueChanged.AddListener(delegate { Check(); });
        }

        public QuestTask task
        {
            get
            {
                return _task;
            }
            set
            {
                _task = value;
                taskLabel.text = _task.taskName;
                if (_task.isMain)
                {
                    taskToggle.isOn = true;
                    taskToggle.interactable = false;
                }
                else
                {
                    taskToggle.isOn = false;
                    taskToggle.interactable = true;
                }
            }

        }

        private void Check()
        {
            InterfaceManager.GetInstantiate().activitiesPanel.CheckAccept();
        }
    }
}

