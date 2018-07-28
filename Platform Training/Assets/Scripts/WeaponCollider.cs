using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour {
	public WeaponController w;
	public GameObject DamageEffect;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy" && w.isAttacking)
		{
			Vector3 HitPos = col.gameObject.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position);
			GameObject instance = (GameObject)Instantiate(DamageEffect, new Vector3(HitPos.x, HitPos.y,HitPos.z - 0.1f), new Quaternion(0, 0, 0, 0));
			Destroy(instance, instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
			col.GetComponent<Enemy_Status>().GetDamage(w.attackList[w.attackIndex].attack_Damage);
		}
	}
}
