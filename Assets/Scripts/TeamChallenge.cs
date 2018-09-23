using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class TeamChallenge : MonoBehaviour
    {
        private Team curTeam;
        public ChallengeType type = ChallengeType.METHOD;
        public Method method;
        public Tag _tag;
        public ExecutorType executor;
        public int level;
        public int difficulty;

        public bool Challage(Team team)
        {
            curTeam = team;
            if (curTeam != null)
            {
                bool result = false;
                if(type == ChallengeType.HAVE_TAG)
                {
                    if (executor == ExecutorType.LEADER)
                    {
                        result = curTeam.IsLeaderHaveTag(_tag);
                    }
                    else if (executor == ExecutorType.TEAM)
                    {
                        result = curTeam.IsTeamHaveTag(_tag);
                    }
                    Debug.Log(string.Format("Челендж have_tag {0} - {1}", _tag, result), this);
                    return result;
                }
                else if(type == ChallengeType.METHOD)
                {
                    if(executor == ExecutorType.LEADER)
                    {
                        result = curTeam.IsLeaderChallenge(method, level, difficulty, _tag);
                    }
                    else if(executor == ExecutorType.TEAM)
                    {
                        result = curTeam.IsTeamChallenge(method, level, difficulty, _tag);
                    }
                    Debug.Log(string.Format("Челендж {0}, level {1}, difficult {2} - {3}", method, level, difficulty, result), this);
                    return result;
                }
            }
            Debug.Log(string.Format("Челендж не прошел"), this);
            return false;
        }
    }

    public enum ChallengeType
    {
        HAVE_TAG,
        METHOD
    }

    public enum ExecutorType
    {
        TEAM,
        LEADER
    }
}
