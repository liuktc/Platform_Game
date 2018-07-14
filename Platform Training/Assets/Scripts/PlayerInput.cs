using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;
	WeaponController weapon;

	void Start () {
		player = GetComponent<Player> ();
		weapon = GameObject.FindWithTag("Hand").GetComponent<WeaponController>();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetButtonDown ("Jump") || Input.GetKeyDown(KeyCode.Joystick1Button0)) {
			player.OnJumpInputDown ();
		}
		if (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.Joystick1Button0))
		{
			player.OnJumpInputUp();
		}
		if (Input.GetAxis("Attack1") > 0 || Input.GetMouseButtonDown(0)) 
		{
			weapon.Attack1();
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetMouseButtonDown(2))
		{
			weapon.Attack2();
		}
		if (Input.GetAxis("Defend") > 0 || Input.GetMouseButtonDown(1))
		{
			weapon.Defend();
		}
		/*if (Input.GetJoystickNames().Length > 0)
		{
			if (Input.GetAxis("Defend") == 0)
			{
				weapon.Stop_Defend();
			}
		}
		else*/
		//{
			if (Input.GetMouseButtonUp(1)) {
				weapon.Stop_Defend();
			}
		//}
	}
}
