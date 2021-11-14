using System;
using System.Collections.Generic;
using System.Linq;
using NewCode.PickableItems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NewCode.Weapons
{
    public class WeaponsPlacer : MonoBehaviour
    {
        [SerializeField] private FirearmQuantity[] firearmsPrefabs;
        [SerializeField] private List<PositionWithProbability> hidingPositions;
        private List<PickableFirearm> availableWeapons;

        public PickableFirearm GetClosestAvailableWeapon(Vector3 position)
        {
            PickableFirearm closest = null;
            float closestDistance = float.PositiveInfinity;
            foreach (PickableFirearm availableWeapon in availableWeapons)
            {
                float distance = Vector3.Distance(availableWeapon.transform.position, position);
                if (closest == null || distance < closestDistance)
                {
                    closest = availableWeapon;
                    closestDistance = distance;
                }
            }

            availableWeapons.Remove(closest);
            return closest;
        }

        public void PlaceWeapons()
        {
            availableWeapons = new List<PickableFirearm>(hidingPositions.Count);
            foreach (FirearmQuantity firearmPrefab in firearmsPrefabs)
            {
                if (hidingPositions.Count == 0)
                {
                    break;
                }

                PlaceFirearm(firearmPrefab);
            }
        }

        private void PlaceFirearm(FirearmQuantity firearmPrefab)
        {
            for (int i = 0; i < firearmPrefab.quantity; i++)
            {
                var hidingPosition = GetHidingPosition();
                PickableFirearm firearm = Instantiate(firearmPrefab.pickableFirearmPrefab,
                    hidingPosition.position.position, Quaternion.identity);
                hidingPositions.Remove(hidingPosition);
                availableWeapons.Add(firearm);
            }
        }

        private PositionWithProbability GetHidingPosition()
        {
            float randomValue = Random.Range(0f, hidingPositions.Sum(pos => pos.probability));
            float cumulativeProbability = 0.0f;
            foreach (PositionWithProbability positionWithProbability in hidingPositions)
            {
                // Loop until the random number is less than our cumulative probability
                cumulativeProbability += positionWithProbability.probability;
                if (randomValue <= cumulativeProbability)
                {
                    return positionWithProbability;
                }
            }

            return null;
        }

        // private void OnValidate()
        // {
        //     hidingPositions = hidingPositions.OrderBy(pos => pos.probability).ToList();
        // }

        [ContextMenu("Distribute evenly")]
        private void EvenDistribution()
        {
            float probability = 1f / hidingPositions.Count;
            foreach (var positionWithProbability in hidingPositions)
            {
                positionWithProbability.probability = probability;
            }

            hidingPositions = hidingPositions.OrderBy(pos => pos.probability).ToList();
        }

        [Serializable]
        private class PositionWithProbability
        {
            public Transform position;
            [Range(0f, 1f)] public float probability;
        }

        [Serializable]
        private class FirearmQuantity
        {
            public PickableFirearm pickableFirearmPrefab;
            public byte quantity;
        }
    }
}