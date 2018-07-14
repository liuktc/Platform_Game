using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	GameObject Weapon;

	public GameObject Destroy_Animation_NotBody;
	public GameObject Destroy_Animation_Body;
	public float Destroy_Delay;
	public float Damage;
	public float speed;


	void Start()
	{
		Weapon = GameObject.FindWithTag("Hand");
	}
	public void Destroy (bool body,Collider2D col) {
		GameObject instance;
		if (body == true)
		{
			instance = (GameObject)Instantiate(Destroy_Animation_Body, this.transform.FindChild("Collision_Point").gameObject.transform.position, new Quaternion(0, 0, 0, 0));
			instance.transform.parent = GameObject.FindWithTag("Player").transform;
		}
		else
		{
			instance = (GameObject)Instantiate(Destroy_Animation_NotBody, this.transform.FindChild("Collision_Point").gameObject.transform.position, new Quaternion(0, 0, 0, 0));
		}
		Destroy(instance, instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length +Destroy_Delay);
		Destroy(gameObject, 0.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Vector2.right * Time.deltaTime * speed);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy")
		{
			Destroy(true, col);
			col.gameObject.GetComponent<Enemy_Status>().GetDamage(Damage);
		}
		else
		{
			if (col.gameObject.layer == 9)
			{
				Destroy(false, col);
			}
			else
			{
				if (col.gameObject.tag == "Weapon_Collider" && Weapon.GetComponent<WeaponController>().Weapon_Status.Defend == true)
				{
					Destroy(false, col);
				}
				else
				{
					if (col.gameObject.tag == "Player")
					{
						col.gameObject.GetComponent<PlayerStatus>().GetDamage(Damage);
						Destroy(true, col);
					}
				}
			}
		}

	}
}
