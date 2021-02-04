using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] Camera fpCamera;
	[SerializeField] float raycastRange;
	[SerializeField] float weaponDamage = 30f;
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip gunshot;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
            Shoot();
		}
    }

	private void Shoot()
	{
		audioSource.PlayOneShot(gunshot);
		RaycastHit hit;
		if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, raycastRange))
		{
			Debug.Log("I hit: " + hit.transform.name);

			// TODO Add a hit effect

			EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
			if(target == null) { return; }
			else
			{
				target.TakeDamage(weaponDamage);
			}

		}
		
	}
}
