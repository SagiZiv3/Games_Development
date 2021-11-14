using System;
using UnityEngine;

namespace NewCode.Characters.Npc
{
    public class NpcSensorHandler : MonoBehaviour
    {
        [SerializeField] private Sensor sensor;

        [Header("Patrol Mode"), SerializeField]
        private LayerMask charactersLayer;

        [SerializeField] private float attackRange = 3.5f;

        [Header("Gun Search Mode"), SerializeField]
        private LayerMask weaponsLayer;

        [SerializeField] private float detectionRadius = 1f;

        public void ToGunSearchMode(Action<Transform> onTransformEnter)
        {
            SetSensorParameters(weaponsLayer, detectionRadius, onTransformEnter);
        }

        public void ToPatrolMode(Action<Transform> onTransformEnter)
        {
            SetSensorParameters(charactersLayer, attackRange, onTransformEnter);
        }

        public void ResetSensor()
        {
            sensor.onTransformEntered.RemoveAllListeners();
        }

        private void SetSensorParameters(LayerMask layers, float radius, Action<Transform> onTransformEnter)
        {
            ResetSensor();
            sensor.SetDetectionLayer(layers);
            sensor.SetDetectionRadius(radius);
            sensor.onTransformEntered.AddListener(onTransformEnter.Invoke);
        }
    }
}