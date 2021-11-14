using UnityEngine;
using UnityEngine.AI;

namespace NewCode.Characters.Npc
{
    public class NpcMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private AnimationHandler animationHandler;
        private Transform target;
        private bool updateNavmeshAgent;

        public void SetTarget(Transform newTarget, bool isMovingTarget = true)
        {
            target = newTarget;
            updateNavmeshAgent = isMovingTarget;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.position);
        }

        public void Stop()
        {
            updateNavmeshAgent = false;
            navMeshAgent.isStopped = true;
            animationHandler.SetWalkingSpeed(Vector3.zero);
        }

        public bool ArrivedToTarget()
        {
            // Debug.Log((transform.name, navMeshAgent.pathPending, navMeshAgent.hasPath, navMeshAgent.remainingDistance), transform);
            return !navMeshAgent.hasPath || navMeshAgent.pathPending || navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
        }

        private void Update()
        {
            if (updateNavmeshAgent)
                navMeshAgent.SetDestination(target.position);
            animationHandler.SetWalkingSpeed(navMeshAgent.desiredVelocity);
        }

        public void SetStoppingDistance(float stoppingDistance)
        {
            navMeshAgent.stoppingDistance = stoppingDistance;
        }
    }
}