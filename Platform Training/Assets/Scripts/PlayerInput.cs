using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;

	void Start () {
		player = GetComponent<Player> ();
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
	}
}
