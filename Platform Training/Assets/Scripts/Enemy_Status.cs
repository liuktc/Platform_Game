using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Status : MonoBehaviour {
	public float Max_Health;
	[HideInInspector]
	public float Health;
	public GameObject DieEffect;
	bool dead = false;
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
		HitAnimation();
		Health -= damage;
	}
	public void SetMaxLife()
	{
		Health = Max_Health;
	}

	public void Die()
	{
		if (!dead)
		{
			dead = true;
			GameObject instance = (GameObject)Instantiate(DieEffect, transform.position, new Quaternion(0, 0, 0, 0));
			Camera.main.GetComponent<ZoomScript>().ZoomTo(transform);
			Destroy(instance, instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
			Destroy(gameObject, (Camera.main.GetComponent<ZoomScript>().zoomTime + Camera.main.GetComponent<ZoomScript>().deZoomTime)*2);
		}
	}
	public void HitAnimation()
	{
		GetComponent<Animator>().SetTrigger("Hit");
	}
}
