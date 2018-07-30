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
		/*if (ControlEnabled)
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
		if (Input.GetMouseButtonDown(1) && ControlEnabled)
		{
			weapon.Attack(1);
			weapon.flip = false;
		}
		if (Input.GetKeyDown(KeyCode.V) && ControlEnabled)
		{
			weapon.Attack(1);
		}
		if (Input.GetJoystickNames().Length > 0)
		{
			if (Input.GetAxis("Defend") == 0)
			{
				weapon.Stop_Defend();
			}
		}
		else
		//{
			if (Input.GetMouseButtonUp(1)) {
				weapon.Stop_Defend();
			}
		//}
		*/
		if (ControlEnabled)
		{
			Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
			player.SetDirectionalInput(directionalInput);
			if (FindObjectOfType<GameManager>().useController)
			{
				if (Input.GetKeyDown(KeyCode.JoystickButton0))					//Xbox 360 controller = A
				{
					player.OnJumpInputDown ();
				}
				if (Input.GetKeyUp(KeyCode.JoystickButton0))
				{
					player.OnJumpInputUp();
				}
				if (Input.GetAxis("Attack") > 0)								//Xbox 360 controller = RT
				{
					weapon.Attack(0);
					weapon.Stop_Defend();
				}
				if (Input.GetKeyDown(KeyCode.JoystickButton5))					//Xbox 360 controller = RB
				{
					Debug.Log("Throw");
					weapon.AttackThrow();
				}
				if (Input.GetKeyDown(KeyCode.JoystickButton4))					//Xbox 360 controller = LB
				{
					weapon.Attack(1);
					weapon.flip = false;
				}
				if (Input.GetAxis("Defend") > 0)
				{
					weapon.Defend();
				}
				else
				{
					weapon.Stop_Defend();
				}
			}
		}
	}
}
