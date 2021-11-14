using System.Collections.Generic;
using NewCode.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace NewCode.Characters.Npc.Behaviours
{
    public class AttackBehaviour : MonoBehaviourBehaviour
    {
        [SerializeField] private WeaponHandler weaponHandler;
        [SerializeField] private NpcMovement movementHandler;
        [SerializeField] private float stoppingDistance = 5f;
        [SerializeField] private UnityEvent onTargetsDied;
        private readonly List<Character> targets = new List<Character>();

        public void AddTarget(Character target)
        {
            targets.Add(target);
        }
        
        public override void OnEnter()
        {
            movementHandler.SetStoppingDistance(stoppingDistance);
            movementHandler.SetTarget(targets[0].transform);
        }

        public override void Handle()
        {
            if (movementHandler.ArrivedToTarget())
            {
                transform.parent.LookAt(targets[0].transform);
                transform.parent.rotation = Quaternion.Euler(0, transform.parent.eulerAngles.y, 0);
                weaponHandler.UseFirearm();
                // Remove all the dead targets
                targets.RemoveAll(target => target.HealthPercentage == 0);
                if (targets.Count == 0)
                {
                    onTargetsDied.Invoke();
                }
                else // If there ara more targets, keep attacking them.
                {
                    movementHandler.SetTarget(targets[0].transform);
                }
            }
        }

        public override void OnExit()
        {
            movementHandler.Stop();
            targets.Clear(); // If we exit attack mode, we don't suppose to have any more targets.
        }

        public override void Pause()
        {
            movementHandler.Stop();
        }

        public override void Resume()
        {
            movementHandler.SetTarget(targets[0].transform);
        }
    }
}