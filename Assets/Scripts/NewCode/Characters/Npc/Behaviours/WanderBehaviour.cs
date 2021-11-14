using UnityEngine;

namespace NewCode.Characters.Npc.Behaviours
{
    public class WanderBehaviour : MonoBehaviourBehaviour
    {
        [SerializeField] private NpcBehavioursController behavioursController;
        [SerializeField] private NpcMovement movementHandler;
        [SerializeField] private Transform[] walkPoints;
        [SerializeField] private float minWaitTime = 2f, maxWaitTime = 7f;
        private int currentWalkPointIndex = -1;

        public override void OnEnter()
        {
            GetNewWalkingPointIndex();
            movementHandler.SetTarget(walkPoints[currentWalkPointIndex], false);
            movementHandler.SetStoppingDistance(0f);
        }

        public override void Handle()
        {
            if (movementHandler.ArrivedToTarget())
            {
                behavioursController.StartWaitBehaviour(Random.Range(minWaitTime, maxWaitTime));
            }
        }

        public override void OnExit()
        {
            movementHandler.Stop();
        }

        public override void Pause()
        {
            movementHandler.Stop();
        }

        public override void Resume()
        {
            movementHandler.SetTarget(walkPoints[currentWalkPointIndex], false);
        }

        private void GetNewWalkingPointIndex()
        {
            int newIndex;
            do
            {
                newIndex = Random.Range(0, walkPoints.Length);
            } while (newIndex == currentWalkPointIndex);

            currentWalkPointIndex = newIndex;
        }
    }
}