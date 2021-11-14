using System;
using UnityEngine;
using UnityEngine.Events;

namespace NewCode
{
    // [RequireComponent(typeof(SphereCollider))]
    public class Sensor : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        public TransformDetectedEvent onTransformEntered;
        public TransformDetectedEvent onTransformExited;

        public void SetDetectionLayer(LayerMask newLayer)
        {
            layerMask = newLayer;
        }

        public void SetDetectionRadius(float radius)
        {
            GetComponent<SphereCollider>().radius = radius;
        }

        // public void TurnOn()
        // {
        //     GetComponent<Collider>().enabled = true;
        // }
        //
        // public void TurnOff()
        // {
        //     GetComponent<Collider>().enabled = false;
        // }

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsObjectInLayerMask(other.gameObject))
            {
                onTransformEntered.Invoke(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsObjectInLayerMask(other.gameObject))
            {
                onTransformExited.Invoke(other.transform);
            }
        }

        private bool IsObjectInLayerMask(GameObject gObj)
        {
            // Source: https://forum.unity.com/threads/ontriggerenter-and-layer-mask-need-help.225570/#post-6949700
            return ((1 << gObj.layer) & layerMask) != 0;
        }

        [Serializable]
        public class TransformDetectedEvent : UnityEvent<Transform>
        {
        }
    }
}