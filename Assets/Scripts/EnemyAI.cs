using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 100f;

    NavMeshAgent navMeshAgent;
    Transform destination;
    float distanceToTarget = Mathf.Infinity;
	bool isProvoked = false;


    void Start()
    {
		navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
	{

		CheckDistance();
		if (isProvoked)
		{
			EngageTarget();
		}
	}

	private void CheckDistance()
	{
		distanceToTarget = Vector3.Distance(gameObject.transform.position, target.position);

		if (distanceToTarget <= chaseRange)
		{
			isProvoked = true;
		}
	}

	private void EngageTarget()
	{
		if (distanceToTarget >= navMeshAgent.stoppingDistance)
		{
			navMeshAgent.SetDestination(target.position);
		}
		if (distanceToTarget <= navMeshAgent.stoppingDistance)
		{
			print("Attack!");

		}
	}

	void OnDrawGizmosSelected()
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, chaseRange);
	}
}
