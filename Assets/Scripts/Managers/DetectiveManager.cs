using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    public class DetectiveManager : MonoBehaviour
    {
        private static DetectiveManager instance;
        [SerializeField]
        private List<Detective> allDetectives = new List<Detective>();

        public static DetectiveManager GetInstantiate()
        {
            if(instance == null)
            {
                instance = FindObjectOfType<DetectiveManager>();
            }
            if (instance == null)
            {
                Game game = Game.GetInstantiate();
                instance = Instantiate(game.detectiveManager, game.transform);
            }
            return instance;
        }

        private void Awake()
        {
            GetInstantiate();
        }

        public List<Detective> GetDetectives()
        {
            return allDetectives;
        }

        public void Registrate(Detective detective)
        {
            allDetectives.Add(detective);
            UnityEditor.PrefabUtility.ReplacePrefab(this.gameObject, UnityEditor.PrefabUtility.GetPrefabParent(this));
        }

        public Team CreateTeam(List<Detective> detectives, iActivityPlace start)
        {
            Agency agency = Agency.GetInstantiate();
            GameObject teamFolder = agency.teamFolder;
            if (teamFolder == null && agency.transform.Find("Teams"))
            {
                teamFolder = agency.transform.Find("Teams").gameObject;
                agency.teamFolder = teamFolder;
            }
            if (teamFolder == null)
            {
                teamFolder = new GameObject("Teams");
                teamFolder.transform.parent = agency.transform;
                agency.teamFolder = teamFolder;
            }
            GameObject goTeam = new GameObject(string.Format("Team_{0}", detectives[0].characterName));
            goTeam.transform.parent = teamFolder.transform;
            Team team = goTeam.AddComponent<Team>();
            team.detectives = detectives;
            team.startPlace = start;
            team.CreateLineRenderer();
            agency.teams.Add(team);
            return team;
        }

        public void TeamOnTarget(List<Detective> detectives, iActivityPlace target, List<QuestTask> questTasks = null, iActivityPlace start = null)
        {
            Agency agency = Agency.GetInstantiate();
            if (start == null)
            {
                start = agency.GetOffice();
            }
            Team team = CreateTeam(detectives, start);
            if (target is QuestEvent)
            {
                team.targetTasks = questTasks;
            }
            team.GoTo(target, WayType.MAINSTREET, true);
        }
    }
}
