using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	float Angle;
	GameObject Player;
	GameObject Weapon;

	public GameObject Projectile;

	public float distanceToShoot;

	public float Shoot_Delay = 1f;

	bool isShooting;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag("Player");
		Weapon = gameObject.transform.Find("Hand").gameObject;
		//InvokeRepeating("Shoot", 0f, Shoot_Delay);
	}
	float distance(float x1, float y1, float x2, float y2)
	{
		return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));	}
	// Update is called once per frame
	void Update () {
		CalculateAngle();
		ApplyAngle();
		CalculateFlip();
		Debug.Log(isShooting);
		if (distanceToShoot < distance(transform.position.x, transform.position.y, Player.transform.position.x, Player.transform.position.y) && isShooting == false)
		{
			Debug.Log("Invoking Shoot");
			isShooting = true;
			InvokeRepeating("Shoot", 0f, Shoot_Delay);
		}
		else
		{
			if (distanceToShoot > distance(transform.position.x, transform.position.y, Player.transform.position.x, Player.transform.position.y))
			{
				Debug.Log("Cancel Shoot");
				isShooting = false;
				CancelInvoke("Shoot");
			}
		}
	}

	void CalculateAngle()
	{
		Camera MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		GameObject Origin = this.gameObject;

		float PlayerX = MainCamera.WorldToScreenPoint(Player.transform.position).x;
		float PlayerY = MainCamera.WorldToScreenPoint(Player.transform.position).y;
		float OriginX = MainCamera.WorldToScreenPoint(Origin.transform.position).x;
		float OriginY = MainCamera.WorldToScreenPoint(Origin.transform.position).y;

		float relPlayerX = PlayerX - OriginX;
		float relPlayerY = PlayerY - OriginY;

		Angle = Mathf.Atan(Mathf.Abs(relPlayerY) / Mathf.Abs(relPlayerX)) * Mathf.Rad2Deg;

		if (relPlayerX < 0 && relPlayerY >= 0)
		{
			Angle = 180 - Angle;
		}
		if (relPlayerX < 0 && relPlayerY < 0)
		{
			Angle = Angle + 180;
		}
		if (relPlayerX >= 0 && relPlayerY < 0)
		{
			Angle = 360 - Angle;
		}
	}

	void ApplyAngle()
	{
		Weapon.transform.eulerAngles = new Vector3(0,0,Angle);
	}

	void CalculateFlip()
	{
		if ((Angle <= 90 && Angle >= 0) || (Angle >= 270 && Angle <= 360))
		{
			GetComponent<SpriteRenderer>().flipX = false;
		}
		if ((Angle > 90 && Angle <= 180) || (Angle < 270 && Angle >= 180))
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}	}

	void Shoot()
	{
		GameObject instance;
		instance = (GameObject)Instantiate(Projectile, gameObject.transform.Find("Hand").Find("HandSprite").gameObject.transform.position,Quaternion.Euler(0, 0, Angle));
		instance.GetComponent<Projectile>().Fired_By_Player = false;
		Destroy(instance, 5.0f);
	}

	public void GetAttack1(float damage, float force)
	{
		GetComponent<Enemy_Status>().GetDamage(damage);
		GetComponent<Animator>().SetTrigger("Hit");
	}
	public void GetAttack2()
	{
		GetComponent<Animator>().SetTrigger("Hit");
	}
}
