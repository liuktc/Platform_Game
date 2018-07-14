using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Status : MonoBehaviour {
	public float Max_Health;
	[HideInInspector]
	public float Health;
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
		Debug.Log(damage + " damage");
		Health -= damage;
	}
	public void SetMaxLife()
	{
		Health = Max_Health;
	}

	public void Die()
	{
		Destroy(gameObject);
	}
}
