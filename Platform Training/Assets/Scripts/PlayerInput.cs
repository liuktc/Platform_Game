using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;
	WeaponController weapon;
	public bool Attack2Active = false;

	public bool ControlEnabled = true;

	void Start () {
		player = GetComponent<Player> ();
		weapon = GameObject.FindWithTag("Hand").GetComponent<WeaponController>();
	}

	void Update () {
		if (ControlEnabled)
		{
			Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"),0);
			player.SetDirectionalInput(directionalInput);
		}

		if ((Input.GetButtonDown ("Jump") || Input.GetKeyDown(KeyCode.Joystick1Button0)) && ControlEnabled) {
			player.OnJumpInputDown ();
		}
		if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.Joystick1Button0))&&ControlEnabled)
		{
			player.OnJumpInputUp();
		}
		if ((Input.GetAxis("Attack1") > 0 || Input.GetMouseButtonDown(0))&&ControlEnabled)
		{
			weapon.Attack(0);
			weapon.Stop_Defend();
		}
		if (((Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetMouseButtonDown(2))&&Attack2Active) && ControlEnabled)
		{
			weapon.AttackThrow();
		}
		if ((Input.GetAxis("Defend") > 0 || Input.GetMouseButtonDown(1)) && ControlEnabled)
		{
			weapon.Defend();
		}
		if (Input.GetKeyDown(KeyCode.V) && ControlEnabled)
		{
			weapon.Attack(1);
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
