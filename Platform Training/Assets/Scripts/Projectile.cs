using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public GameObject Destroy_Animation_NotBody;
	public GameObject Destroy_Animation_Body;
	public float Destroy_Delay;
	public float Damage;

	public void Destroy (bool body) {
		GameObject instance;
		if (body == true)
		{
			instance = (GameObject)Instantiate(Destroy_Animation_Body, this.transform.position, new Quaternion(0, 0, 0, 0));
		}
		else
		{
			instance = (GameObject)Instantiate(Destroy_Animation_NotBody, this.transform.position, new Quaternion(0, 0, 0, 0));
		}
		Destroy(instance, instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length +Destroy_Delay);
		Destroy(gameObject, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector2.left * Time.deltaTime);
	}
}
