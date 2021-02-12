using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 100f;
	[SerializeField] float lookSpeed = 2f;
	Animator animator;

    NavMeshAgent navMeshAgent;
    Transform destination;
    float distanceToTarget = Mathf.Infinity;
	bool isProvoked = false;


    void Start()
    {
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
    }

    void Update()
	{

		CheckDistance();
		if (isProvoked)
		{
			EngageTarget();
		}
        else
        {
			DisengageTarget();
        }
	}

	public void OnDamageTaken()
    {
		isProvoked = true;
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
		
		navMeshAgent.isStopped = false;
		if (distanceToTarget >= navMeshAgent.stoppingDistance)
		{
			animator.SetTrigger("move");
			navMeshAgent.SetDestination(target.position);
		}


		if (distanceToTarget <= navMeshAgent.stoppingDistance)
		{
			animator.SetBool("attack", true);

		}
        else
        {
			animator.SetBool("attack", false);
		}
		FaceTarget();
	}
	private void DisengageTarget()
	{
		navMeshAgent.isStopped = true;
        animator.SetTrigger("idle");

	}
	private void FaceTarget()
    {
		Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);

    }


	void OnDrawGizmosSelected()
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, chaseRange);
	}
}
