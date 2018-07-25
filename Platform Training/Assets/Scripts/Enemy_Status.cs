using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Status : MonoBehaviour {
	public float Max_Health;
	[HideInInspector]
	public float Health;
	public GameObject DieEffect;
	// Use this for initialization
	void Start()
	{
		SetMaxLife();
	}

	// Update is called once per frame
	void Update()
	{
		if (Health <= 0)
		{
			Die();
		}
	}

	public void GetDamage(float damage)
	{
		//Debug.Log(damage + " damage");
		GetComponent<Enemy>().GetAttack2();
		Health -= damage;
	}
	public void SetMaxLife()
	{
		Health = Max_Health;
	}

	public void Die()
	{
		GameObject instance = (GameObject)Instantiate(DieEffect, transform.position, new Quaternion(0, 0, 0, 0));
		Destroy(instance, instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
		Destroy(gameObject);
	}
}
