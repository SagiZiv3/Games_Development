using UnityEngine;

namespace NewCode.Characters.Npc.Behaviours
{
    public class FollowBehaviour : MonoBehaviourBehaviour
    {
        [SerializeField] private NpcMovement movementHandler;
        [SerializeField] private float stoppingDistance = 3f;
        [SerializeField] private Team team;

        public override void OnEnter()
        {
            movementHandler.SetStoppingDistance(stoppingDistance);
            team.OnNewLeaderSelected += UpdateTarget;
            if (team.Leader != null)
            {
                UpdateTarget(team.Leader);
            }
        }

        private void UpdateTarget(Character newLeader)
        {
            movementHandler.SetTarget(newLeader.transform);
        }

        public override void Handle()
        {
        }

        public override void OnExit()
        {
            team.OnNewLeaderSelected -= UpdateTarget;
            movementHandler.Stop();
            movementHandler.SetStoppingDistance(0f);
        }

        public override void Pause()
        {
            movementHandler.Stop();
        }

        public override void Resume()
        {
            UpdateTarget(team.Leader);
        }
    }
}