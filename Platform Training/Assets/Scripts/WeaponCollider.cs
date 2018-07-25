using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour {
	public WeaponController w;
	public GameObject DamageEffect;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy" && w.Weapon_Status.Attack1 == true)
		{
			GameObject instance = (GameObject)Instantiate(DamageEffect, col.transform.position, new Quaternion(0, 0, 0, 0));
			Destroy(instance, instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
			col.GetComponent<Enemy>().GetAttack1(w.Attack_Damage);
		}
	}
}
