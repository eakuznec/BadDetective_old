using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class Timeline : MonoBehaviour
    {
        private static Timeline instance;
        public float secondsInGameHour = 7.5f;
        private TimelineState state = TimelineState.PLAY;
        private TimelineState prevState;
        private float curTime;
        public List<TimelineAction> actions = new List<TimelineAction>();

        public static Timeline GetInstantiate()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Timeline>();
            }
            if (instance == null)
            {
                instance = Instantiate(Game.GetInstantiate().timeline);
            }
            return instance;
        }

        private void Awake()
        {
            GetInstantiate();
            DontDestroyOnLoad(instance);
            //Начало игры
            curTime = 8;
            //
            for(int i=0; i<actions.Count;i++)
            {
                actions[i] = Instantiate(actions[i], transform);
            }
        }

        private void Update()
        {
            if (Game.GetInstantiate().GetGameState() == GameState.IN_GAME)
            {
                Agency agency = Agency.GetInstantiate();
                float deltaTime = Time.deltaTime / secondsInGameHour * (int)state;
                curTime += deltaTime;
                for(int i=0; i<actions.Count; i++)
                {
                    if(actions[i].timer <= curTime)
                    {
                        TimelineAction action = actions[i];
                        curTime = action.timer;
                        action.RealizeAction();
                        Destroy(action.gameObject);
                        actions.Remove(action);
                        i--;
                    }
                }
                //Детективов
                foreach(Detective detective in agency.GetDetectives())
                {
                    //усталость
                    float deltaStress = deltaTime * detective.activityPlace.GetStressPerHour();
                    detective.ChangeCurStress(deltaStress);
                }
                //Team
                for(int i=0; i< agency.teams.Count; i++)
                {
                    if (agency.teams[i].destroy)
                    {
                        //Уничтожение
                        Destroy(agency.teams[i].gameObject);
                        Agency.GetInstantiate().teams.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        //в пути
                        Team team = agency.teams[i];
                        if (team.activity == DetectiveActivity.IN_WAY && team.showWay)
                        {
                            team.DrawWay(deltaTime);
                        }
                    }
                }
            }
            UI.InterfaceManager.GetInstantiate().timePanel.CheckTime(GameTime.Convert(curTime));
        }

        public TimelineState GetTimelineState()
        {
            return state;
        }

        public void ChangeState(TimelineState newState)
        {
            prevState = state;
            state = newState;
            UI.InterfaceManager.GetInstantiate().timePanel.CheckTimeSpeed(state);
        }

        public TimelineState GetPrevTimelineState()
        {
            return prevState;
        }

        public float GetTime()
        {
            return curTime;
        }

        public List<TimelineAction> GetActions()
        {
            return actions;
        }

        public void RegistrateAction(TimelineAction action)
        {
            actions.Add(action);
            actions.Sort();
        }
    }
}