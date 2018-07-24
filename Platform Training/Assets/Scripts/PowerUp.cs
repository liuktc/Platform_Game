using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	public enum Type { ShurikenActivation }

	public Type PowerUpType;
	public float RotationSpeed;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PlayAnimation();
	}

	void ShurikenActivation(GameObject Player)
	{
		Player.GetComponent<PlayerInput>().Attack2Active = true;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			//Debug.Log("Yeee");
			if (PowerUpType == Type.ShurikenActivation)
			{
				ShurikenActivation(col.gameObject);
			}
			Destroy(gameObject);
		}
	}
	void PlayAnimation()
	{
		transform.Rotate(0, 10 * RotationSpeed, 0);
	}
}
