using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour {

	public float Angle;
	float lastAngle;

	public Text DebugText;

	public GameObject Weapon_Pivot;
	public GameObject Weapon_Collider;
	WeaponStatus Weapon_Status;
	public GameObject Player;

	public bool left, right,lastLeft,lastRight;

	public GameObject Projectile;

	void Start()
	{
		Player = GameObject.FindWithTag("Player");
		Weapon_Pivot = GameObject.FindWithTag("Weapon");
		Weapon_Collider = GameObject.Find("WeaponSprite_Collider");
	}
	void Update () {
		if (Input.GetJoystickNames().Length > 0){
			//Debug.Log("Controller connected");
			Angle = JoystickAngle();
		}
		else {
			//Debug.Log("No controller connected");
			Angle = MouseAngle();
		}
		if (Angle == 0 && lastLeft){
			Angle = 180;
		}
		ApplyAngle();

		lastLeft = left;
		lastRight = right;
	}

	float JoystickAngle()
	{
		float Horizontal = Input.GetAxis("RightStickX");
		float Vertical = Input.GetAxis("RightStickY");

		return -Mathf.Atan2(Vertical, Horizontal) * Mathf.Rad2Deg;
	}

	float MouseAngle()
	{
		float MouseX = Input.mousePosition.x;
		float MouseY = Input.mousePosition.y;

		Camera MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		GameObject Origin = Player.gameObject;

		float OriginX = MainCamera.WorldToScreenPoint(Origin.transform.position).x;
		float OriginY = MainCamera.WorldToScreenPoint(Origin.transform.position).y;

		float relMouseX = MouseX - OriginX;
		float relMouseY = MouseY - OriginY;

		float AngleTemp = Mathf.Atan(Mathf.Abs(relMouseY) / Mathf.Abs(relMouseX)) * Mathf.Rad2Deg;

		if (relMouseX >= 0 && relMouseY >= 0)
		{
			return AngleTemp;
		}
		if (relMouseX < 0 && relMouseY >= 0)
		{
			return 180 - AngleTemp;
		}
		if (relMouseX < 0 && relMouseY < 0)
		{
			return AngleTemp + 180;
		}
		if (relMouseX >= 0 && relMouseY < 0)
		{
			return 360 - AngleTemp;
		}

		return 0;
	}

	void ApplyAngle()
	{
		if (Angle < 0)
		{
			Angle += 360;
		}
		//ClampAngle();
		//CalculateFlip();

		DebugText.text = "α = " + Angle;
		transform.eulerAngles = new Vector3(0,0,Angle);
	}

	void ClampAngle()
	{
		if (Angle >= 230 && Angle <= 270)
		{
			Angle = 230;
		}
		if (Angle >= 270 && Angle <= 310)
		{
			Angle = 310;
		}
	}

	void CalculateFlip()
	{
		if ((Angle <= 90 && Angle >= 0) || (Angle >= 270 && Angle <= 360))
		{
			right = true;
			left = false;
			Weapon_Pivot.transform.localScale = new Vector3(1, 1, 1);
		}
		if ((Angle > 90 && Angle <= 180) || (Angle < 270 && Angle >= 180))
		{
			right = false;
			left = true;
			Weapon_Pivot.transform.localScale = new Vector3(1, -1, 1);
		}
	}

	public void Attack1()
	{
		Weapon_Status.Attack1 = true;
	}

	public void Attack2()
	{
		Weapon_Status.Attack2 = true;
	}

	public void Defend()
	{
		Weapon_Status.Defend = true;	}

	public void Stop_Defend()
	{
		Weapon_Status.Defend = false;	}

	struct WeaponStatus {
		public bool Attack1, Attack2, Defend;
		public void reset()
		{
			Attack1 = Attack2 = Defend = false;
		}
	}
}
