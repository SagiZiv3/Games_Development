using UnityEngine;

namespace NewCode.Characters.Npc.Behaviours
{
    public class WaitBehaviour : MonoBehaviourBehaviour
    {
        [SerializeField] private NpcBehavioursController behavioursController;
        private IBehaviour previousBehaviour;
        private float waitTime, timePassed;
        private bool paused;

        public void Initialize(IBehaviour callingBehaviour, float time)
        {
            previousBehaviour = callingBehaviour;
            waitTime = time;
            paused = false;
        }

        public override void OnEnter()
        {
            timePassed = 0;
        }

        public override void Handle()
        {
            if (paused) return;
            timePassed += Time.deltaTime;
            if (timePassed >= waitTime)
            {
                behavioursController.SetBehaviour(previousBehaviour);
            }
        }

        public override void OnExit()
        {
        }

        public override void Pause()
        {
            paused = true;
        }

        public override void Resume()
        {
            paused = false;
        }
    }
}