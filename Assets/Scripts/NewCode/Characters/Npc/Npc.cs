using System.Collections;
using NewCode.Weapons;
using UnityEngine;

namespace NewCode.Characters.Npc
{
    public class Npc : Character
    {
        [SerializeField] protected NpcBehavioursController behavioursController;
        [SerializeField] private AnimationHandler animationHandler;
        [SerializeField] private NpcSensorHandler sensorHandler;

        public override void Activate()
        {
            base.Activate();
            if (!gameObject.activeSelf)
                return;
            behavioursController.Activate();
            team.OnNewLeaderSelected += OnLeaderReplaced;
        }

        public override void Pause()
        {
            base.Pause();
            behavioursController.Pause();
        }

        public override void Resume()
        {
            base.Resume();
            behavioursController.Resume();
        }

        public void OnWeaponPicked(Firearm firearm)
        {
            behavioursController.ReturnToBaseState();
            sensorHandler.ToPatrolMode(OtherCharacterDetected);
            animationHandler.SwitchToCombatAnimator();
        }

        private void OtherCharacterDetected(Transform characterTransform)
        {
            if (!characterTransform.TryGetComponent(out Character character) // If it is not a character
                || ReferenceEquals(character.team, this.team)) // Or if we are on the same team
            {
                return;
            }

            behavioursController.StartAttackBehaviour(character);
        }

        private void OnLeaderReplaced(Character newLeader)
        {
            // If this NPC is the new leader, change the state to wandering.
            if (ReferenceEquals(newLeader, this))
            {
                // behavioursController.SetBaseState(wanderBehaviour);
                behavioursController.SetBaseBehaviourToWanderBehaviour();
                updatesViewer.WriteNewLeader(transform.name, team.name);
            }
        }

        protected override void OnCharacterDied()
        {
            team.OnNewLeaderSelected -= OnLeaderReplaced;
            sensorHandler.ResetSensor();
            base.OnCharacterDied();
            behavioursController.Deactivate();
            animationHandler.ShowDeadAnimation();
        }
    }
}