using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Input : MonoBehaviour {
	WeaponController weapon;

	void Start()
	{
		weapon = GameObject.FindWithTag("Hand").GetComponent<WeaponController>();
	}

	public void Update () {
		if (Input.GetAxis("Attack1") > 0 || Input.GetMouseButtonUp(0)) 
		{
			weapon.Attack1();
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetMouseButtonUp(2))
		{
			weapon.Attack2();
		}
		if (Input.GetAxis("Defend") > 0 || Input.GetMouseButtonUp(1))
		{
			weapon.Defend();
		}
	}
}
