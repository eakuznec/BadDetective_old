using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective.Control
{
    public class TimelineControl : MonoBehaviour
    {
        private void Update()
        {
            Game game = Game.GetInstantiate();
            Timeline timeline = Timeline.GetInstantiate();
            if(game.GetGameState()== GameState.IN_GAME)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if(timeline.GetTimelineState() != TimelineState.STOP)
                    {
                        timeline.ChangeState(TimelineState.STOP);
                    }
                    else
                    {
                        timeline.ChangeState(timeline.GetPrevTimelineState());
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                        timeline.ChangeState(TimelineState.PLAY);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    timeline.ChangeState(TimelineState.x2);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    timeline.ChangeState(TimelineState.x4);
                }
            }
        }
    }
}
