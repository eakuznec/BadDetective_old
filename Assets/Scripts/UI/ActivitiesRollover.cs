using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadDetective.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class ActivitiesRollover : MonoBehaviour
    {
        private List<iActivityPlace> activities = new List<iActivityPlace>();
        [SerializeField] private ActivitiesRolloverLine rolloverLine;

        public void Show(List<iActivityPlace> openedActivities, Vector2 point)
        {
            gameObject.SetActive(true);
            activities = openedActivities;
            int count = activities.Count;
            foreach(iActivityPlace activity in activities)
            {
                if(activity is QuestEvent)
                {
                    foreach(QuestTask task in ((QuestEvent)activity).tasks)
                    {
                        if(task.mainState == MainState.Started)
                        {
                            count++;
                        }
                    }
                }
            }
            RectTransform rect = rolloverLine.GetComponent<RectTransform>();
            int dif = transform.childCount - count;
            if (dif > 0)
            {
                for(int i = 0; i < dif; i++)
                {
                    Destroy(transform.GetChild(transform.childCount - 1 - i).gameObject);
                }
            }
            else if (dif < 0)
            {
                for(int i=0; i < -dif; i++)
                {
                    Instantiate(rolloverLine, transform);
                }
            }
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20 + rect.rect.height * count);
            GetComponent<RectTransform>().SetPositionAndRotation(point + new Vector2(0, 40), Quaternion.identity);
            int countNum = 0;
            for (int i=0; i < activities.Count; i++)
            {
                ActivitiesRolloverLine activitiesRollover = transform.GetChild(countNum).GetComponent<ActivitiesRolloverLine>();
                activitiesRollover.text.text = string.Format("{0}",activities[i].GetPlaceName());
                activitiesRollover.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, rect.rect.height * countNum + 10, rect.rect.height);
                countNum++;
                if(activities[i] is QuestEvent)
                {
                    foreach (QuestTask task in ((QuestEvent)activities[i]).tasks)
                    {
                        if (task.mainState == MainState.Started)
                        {
                            ActivitiesRolloverLine taskLine = transform.GetChild(countNum).GetComponent<ActivitiesRolloverLine>();
                            taskLine.text.text = string.Format("- {0}", task.taskName);
                            taskLine.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, rect.rect.height * countNum + 10, rect.rect.height);
                            countNum++;
                        }
                    }
                }
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}