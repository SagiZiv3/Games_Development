using System;
using NewCode.Characters;
using UnityEngine;

namespace NewCode
{
    public class TeamsBuilder : MonoBehaviour
    {
        [SerializeField] private TeamSelection[] teamSelections;

        private void Awake()
        {
            foreach (TeamSelection teamSelection in teamSelections)
            {
                teamSelection.AddMembersToTeam();
            }

            Destroy(this);
        }

        [Serializable]
        private class TeamSelection
        {
            public Team team;
            public Character leader;
            public Character[] teamMembers;

            public void AddMembersToTeam()
            {
                team.AddCharacter(leader, true);
                foreach (Character teamMember in teamMembers)
                {
                    team.AddCharacter(teamMember, false);
                }
            }
        }
    }
}