using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Collider : MonoBehaviour {
	public GameObject Player;

	void Start()
	{
		Player = GameObject.FindWithTag("Player");
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Projectile")
		{
			col.GetComponent<Projectile>().Destroy(true);
			Player.GetComponent<PlayerStatus>().GetDamage(col.gameObject.GetComponent<Projectile>().Damage);
		}
	}
}
