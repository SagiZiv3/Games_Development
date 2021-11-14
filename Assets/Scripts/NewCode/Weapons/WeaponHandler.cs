using System.Collections.Generic;
using UnityEngine;

namespace NewCode.Weapons
{
    public class WeaponHandler : MonoBehaviour
    {
        public bool HasFirearm { get; private set; }
        public bool HasGrenade { get; private set; }
        [SerializeField] private Transform[] weaponsPositions;
        private Firearm currentFirearm;
        private Grenade grenade;
        private readonly Dictionary<int, Firearm> pickedFirearmsByTransform = new Dictionary<int, Firearm>();

        public void SetFirearm(Firearm firearmPrefab)
        {
            Transform instantiationPosition = FindFirearmPosition(firearmPrefab); // TODO: Find the correct transform
            // Debug.Log((name, " picks ", firearmPrefab.name), instantiationPosition);
            if (currentFirearm != null)
            {
                currentFirearm.gameObject.SetActive(false);
            }

            if (pickedFirearmsByTransform.TryGetValue(instantiationPosition.GetHashCode(), out Firearm firearm))
            {
                currentFirearm = firearm;
                firearm.gameObject.SetActive(true);
            }
            else
            {
                pickedFirearmsByTransform[instantiationPosition.GetHashCode()] = currentFirearm =
                    Instantiate(firearmPrefab, instantiationPosition, false);
            }

            HasFirearm = true;
        }

        public void SetGrenade(Grenade grenadePrefab)
        {
            grenade = grenadePrefab;
            HasGrenade = true;
        }

        private Transform FindFirearmPosition(Firearm firearmPrefab)
        {
            foreach (Transform weaponsPosition in weaponsPositions)
            {
                if (weaponsPosition.name.StartsWith(firearmPrefab.WeaponName))
                {
                    return weaponsPosition;
                }
            }

            return null;
        }

        public void UseFirearm()
        {
            currentFirearm.Fire();
        }

        public void UseGrenade(Transform referencePoint, float throwForce)
        {
            ThrowGrenade(referencePoint, throwForce);
        }

        private void ThrowGrenade(Transform referencePoint, float throwForce)
        {
            // Create an instance of the grenade prefab
            Grenade grenadeInstance = Instantiate(grenade, referencePoint.position, referencePoint.rotation);
            // Get reference to the Rigidbody
            Rigidbody rb = grenadeInstance.GetComponent<Rigidbody>();
            // Apply the throw force in the direction of the camera (the direction the player looks at)
            rb.AddForce(referencePoint.forward * throwForce, ForceMode.VelocityChange);
        }
    }
}