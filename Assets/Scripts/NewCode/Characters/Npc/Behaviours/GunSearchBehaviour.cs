using System;
using NewCode.PickableItems;
using NewCode.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace NewCode.Characters.Npc.Behaviours
{
    public class GunSearchBehaviour : MonoBehaviourBehaviour
    {
        [SerializeField] private WeaponHandler weaponHandler;
        [SerializeField] private NpcMovement movementHandler;
        [SerializeField] private WeaponsPlacer weaponsPlacer;
        [SerializeField] private NpcSensorHandler sensorHandler;
        [SerializeField] private WeaponPickedEvent arrivedToTarget;
        [SerializeField] private UpdatesViewer updatesViewer;
        private Transform closestWeapon;

        public override void OnEnter()
        {
            closestWeapon = weaponsPlacer.GetClosestAvailableWeapon(transform.position).transform;
            movementHandler.SetTarget(closestWeapon, false);
            sensorHandler.ToGunSearchMode(GunDetected);
        }

        private void GunDetected(Transform weaponTransform)
        {
            PickableFirearm firearm = weaponTransform.GetComponent<PickableFirearm>();
            weaponHandler.SetFirearm(firearm.FirearmPrefab);
            arrivedToTarget.Invoke(firearm.FirearmPrefab);
            Destroy(weaponTransform.gameObject);
            updatesViewer.WriteCharacterPickedGun(transform.parent.name, firearm.ItemName);
        }

        public override void Handle()
        {
            // If the character arrived to the target but the gun disappeared, go to another gun.
            if (movementHandler.ArrivedToTarget())
            {
                closestWeapon = weaponsPlacer.GetClosestAvailableWeapon(transform.position).transform;
                movementHandler.SetTarget(closestWeapon, false);
            }
        }

        public override void OnExit()
        {
            movementHandler.Stop();
            sensorHandler.ResetSensor();
        }

        public override void Pause()
        {
            movementHandler.Stop();
        }

        public override void Resume()
        {
            movementHandler.SetTarget(closestWeapon, false);
        }

        [Serializable]
        private class WeaponPickedEvent : UnityEvent<Firearm>
        {
        }
    }
}