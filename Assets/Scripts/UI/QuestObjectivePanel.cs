using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    public class QuestObjectivePanel : MonoBehaviour
    {
        private QuestObjective objective;
        [SerializeField] private Toggle toggle;
        [SerializeField] private Text label;

        public void SetObjective(QuestObjective objective)
        {
            this.objective = objective;
            if (objective.main)
            {
                label.text = objective.objective;
            }
            else
            {
                label.text = string.Format("{0} (дополнительно)", objective.objective);
            }
            toggle.isOn = objective.state == MainState.Completed;
        }
    }
}
