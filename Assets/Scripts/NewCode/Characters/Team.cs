using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NewCode.Characters
{
    [CreateAssetMenu(fileName = "Team", menuName = "Game Logic/New Team")]
    public class Team : ScriptableObject
    {
        public event Action<Character> OnNewLeaderSelected;
        public event Action OnAllCharactersLeft;
        private readonly List<Character> teamMembers = new List<Character>();
        internal Character Leader { get; private set; }

        public void SendPause()
        {
            foreach (Character teamMember in teamMembers)
            {
                teamMember.Pause();
            }
        }

        public void SendResume()
        {
            foreach (Character teamMember in teamMembers)
            {
                teamMember.Resume();
            }
        }

        public void Clear()
        {
            teamMembers.Clear();
            Leader = null;
        }
        
        internal void AddCharacter(Character character, bool isLeader)
        {
            teamMembers.Add(character);
            if (!isLeader)
                return;

            if (Leader != null)
            {
                throw new ArgumentException("The team already has a leader!");
            }

            Leader = character;
        }

        internal void RemoveCharacter(Character character)
        {
            teamMembers.Remove(character);
            if (teamMembers.Count == 0)
            {
                // All characters left the team.
                OnAllCharactersLeft?.Invoke();
            }
            else if (ReferenceEquals(character, Leader))
            {
                // We don't have a leader anymore, choose a new one randomly.
                Leader = teamMembers[Random.Range(0, teamMembers.Count)];
                OnNewLeaderSelected?.Invoke(Leader);
            }
        }
#if UNITY_EDITOR
        private void OnDisable()
        {
            if (teamMembers != null && teamMembers.Count > 0)
                teamMembers.Clear();
        }
#endif
    }
}