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

        public List<DetectiveEffect> successEffects = new List<DetectiveEffect>();
        public List<DetectiveEffect> successHardEffects = new List<DetectiveEffect>();
        public List<DetectiveEffect> failEffects = new List<DetectiveEffect>();
        public List<DetectiveEffect> failHardEffects = new List<DetectiveEffect>();

        public bool Challage(Team team)
        {
            curTeam = team;
            if (curTeam != null)
            {
                bool result = false;
                List<Detective> successedDetective = new List<Detective>();
                List<Detective> failedDetective = new List<Detective>();
                if (type == ChallengeType.HAVE_TAG)
                {
                    if (executor == ExecutorType.LEADER)
                    {
                        result = curTeam.IsLeaderHaveTag(_tag);
                    }
                    else if (executor == ExecutorType.TEAM)
                    {
                        result = curTeam.IsTeamHaveTag(successedDetective, failedDetective, _tag);
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
                        result = curTeam.IsTeamChallenge(successedDetective, failedDetective, method, level, difficulty, _tag);
                    }
                    Debug.Log(string.Format("Челендж {0}, level {1}, difficult {2} - {3}", method, level, difficulty, result), this);
                    if (result)
                    {
                        foreach(Detective detective in successedDetective)
                        {
                            foreach (DetectiveEffect effect in successEffects)
                            {
                                effect.Realize(detective);
                            }
                        }
                        foreach (Detective detective in failedDetective)
                        {
                            foreach (DetectiveEffect effect in successHardEffects)
                            {
                                effect.Realize(detective);
                            }
                        }
                    }
                    else
                    {
                        foreach (Detective detective in successedDetective)
                        {
                            foreach (DetectiveEffect effect in failEffects)
                            {
                                effect.Realize(detective);
                            }
                        }
                        foreach (Detective detective in failedDetective)
                        {
                            foreach (DetectiveEffect effect in failHardEffects)
                            {
                                effect.Realize(detective);
                            }
                        }
                    }
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
