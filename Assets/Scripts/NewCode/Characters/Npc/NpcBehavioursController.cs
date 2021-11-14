using NewCode.Characters.Npc.Behaviours;
using UnityEngine;

namespace NewCode.Characters.Npc
{
    public class NpcBehavioursController : MonoBehaviour
    {
        [SerializeField] private MonoBehaviourBehaviour baseBehaviour, initialBehaviour;
        [SerializeField] private AttackBehaviour attackBehaviour;
        [SerializeField] private WaitBehaviour waitBehaviour;
        [SerializeField] private WanderBehaviour wanderBehaviour;
        private IBehaviour currentBehaviour;

        public void Activate()
        {
            currentBehaviour = initialBehaviour;
            currentBehaviour.OnEnter();
            enabled = true;
        }

        public void Deactivate()
        {
            currentBehaviour.OnExit();
            currentBehaviour = baseBehaviour;
            enabled = false;
        }

        public void ReturnToBaseState()
        {
            SetBehaviour(baseBehaviour);
        }

        public void SetBaseBehaviourToWanderBehaviour()
        {
            // If we are currently in the base behaviour, change to the new state.
            if (currentBehaviour != null && ReferenceEquals(currentBehaviour, baseBehaviour))
            {
                SetBehaviour(wanderBehaviour);
            }

            baseBehaviour = wanderBehaviour;
        }

        public void SetBehaviour(IBehaviour newBehaviour)
        {
            if (ReferenceEquals(newBehaviour, currentBehaviour))
                return;
            currentBehaviour.OnExit();
            currentBehaviour = newBehaviour;
            currentBehaviour.OnEnter();
        }

        private void Update()
        {
            currentBehaviour.Handle();
        }

        private void OnValidate()
        {
            enabled = false;
        }

        public void StartAttackBehaviour(Character character)
        {
            attackBehaviour.AddTarget(character);
            SetBehaviour(attackBehaviour);
        }

        public void StartWaitBehaviour(float waitTime)
        {
            waitBehaviour.Initialize(currentBehaviour, waitTime);
            SetBehaviour(waitBehaviour);
        }

        public void Pause()
        {
            currentBehaviour.Pause();
            enabled = false;
        }

        public void Resume()
        {
            currentBehaviour.Resume();
            enabled = true;
        }
    }
}