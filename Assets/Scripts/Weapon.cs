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
	[SerializeField] ParticleSystem muzzleFlash;
	[SerializeField] GameObject bulletHitEffect;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
            Shoot();
		}
    }

	private void Shoot()
	{
		PlayMuzzleFlash();
		ProcessRayCast();
	}

	private void PlayMuzzleFlash()
	{
		muzzleFlash.Play();
	}

	private void ProcessRayCast()
	{
		audioSource.PlayOneShot(gunshot, .2f);
		RaycastHit hit;
		if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, raycastRange))
		{
			InstantiateParticles(hit);

			// TODO Add a hit effect

			EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
			if (target == null) { return; }
			else
			{
				target.TakeDamage(weaponDamage);
			}

		}
	}

	private void InstantiateParticles(RaycastHit hit)
	{
		var bulletParticle = Instantiate(bulletHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
		bulletParticle.transform.parent = gameObject.transform;
		Destroy(bulletParticle, .1f);
	}
}
