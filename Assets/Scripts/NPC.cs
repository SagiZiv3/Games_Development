using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        navMeshAgent.SetDestination(target.position);
        animator.SetFloat("speed", navMeshAgent.velocity.magnitude);
    }
}
