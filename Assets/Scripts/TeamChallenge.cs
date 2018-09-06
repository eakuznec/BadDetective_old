using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class TeamChallenge : MonoBehaviour
    {
        private Team curTeam;
        public ChallengeType type;
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
                if(type == ChallengeType.HAVE_TAG)
                {
                    if (executor == ExecutorType.LEADER)
                    {
                        return curTeam.IsLeaderHaveTag(_tag);
                    }
                    else if (executor == ExecutorType.TEAM)
                    {
                        return curTeam.IsTeamHaveTag(_tag);
                    }
                }
                else if(type == ChallengeType.METHOD)
                {
                    if(executor == ExecutorType.LEADER)
                    {
                        return curTeam.IsLeaderChallenge(method, level, difficulty, _tag);
                    }
                    else if(executor == ExecutorType.TEAM)
                    {
                        return curTeam.IsTeamChallenge(method, level, difficulty, _tag);
                    }
                }
            }
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
