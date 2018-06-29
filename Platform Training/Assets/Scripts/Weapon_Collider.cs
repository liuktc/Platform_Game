using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Collider : MonoBehaviour
{
	GameObject Weapon;

	void Start()
	{
		Weapon = GameObject.FindWithTag("Hand");
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Collision with weapon detected");
		if (Weapon.GetComponent<WeaponController>().Weapon_Status.Defend == true)
		{			if (col.tag == "Projectile") {
				col.GetComponent<Projectile>().Destroy(false);
			}
		}
	}
}
