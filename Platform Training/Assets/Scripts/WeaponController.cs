using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour {

	//[HideInInspector]
	public float Angle;
	//float add = 0;
	float lastAngle;

	public Text DebugText;

	//[HideInInspector]
	public GameObject Weapon_Pivot;
	/*[HideInInspector]
	public Collider2D Weapon_Collider;*/
	[HideInInspector]
	public GameObject Weapon_Sprite;
	[HideInInspector]
	public WeaponStatus Weapon_Status;
	//[HideInInspector]
	public GameObject Player;

	[HideInInspector]
	public bool left, right,lastLeft,lastRight;

	public GameObject Projectile;
	public float FireRate;
	private float LastFire;

	public Animator Anim_Weapon;
	public Animator Anim_Angle;

	public float Attack_Force_x;
	public float Attack_Force_y;
	public float Attack_Force_Time;
	public float Attack_Damage;
	public float Attack_Vertical_Multiplicator;

	public float AttackRate;
	private float LastAttack;

	public Animator UIAttackAnimator;

	void Start()
	{
		//Anim = transform.FindChild("Weapon").GetComponent<Animator>()
		Player = GameObject.FindWithTag("Player");
		Weapon_Pivot = GameObject.FindWithTag("Weapon");
		//Weapon_Collider = GameObject.Find("WeaponSprite_Collider").GetComponent<Collider2D>();
		Weapon_Sprite = GameObject.Find("WeaponSprite_Collider");
		if (Input.GetJoystickNames().Length > 0)
		{
			Debug.Log("Controller connected");
		}
	}
	void Update () {
		/*if (Input.GetJoystickNames().Length > 0){
			//Debug.Log("Controller connected");
			Angle = JoystickAngle();
		}
		else {
			//Debug.Log("No controller connected");
			Angle = MouseAngle();
		}*/

		Angle = MouseAngle();
		//add = 0;

		ApplyAngle();

		lastLeft = left;
		lastRight = right;
	}

	float JoystickAngle()
	{
		float Horizontal = Input.GetAxis("RightStickX");
		float Vertical = Input.GetAxis("RightStickY");

		if ((Horizontal<0.05 && Horizontal > - 0.05) && (Vertical < 0.05 && Vertical > -0.05) && lastLeft){
			return 180;
		}
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
		CalculateFlip();

		//DebugText.text = "α = " + Angle;
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
		if (Time.time > AttackRate + LastAttack)
		{
			UIAttackAnimator.SetTrigger("Use");
			Weapon_Status.Attack1 = true;
			Debug.Log("Attack with angle = " + Angle);
			Vector2 Force;
			if (Player.GetComponent<Controller2D>().collisions.below == true)
			{
				Force = new Vector2(Mathf.Cos(Angle * Mathf.Deg2Rad) * Attack_Force_x * Time.deltaTime, Mathf.Sin(Angle * Mathf.Deg2Rad) * Attack_Force_y * Time.deltaTime * Attack_Vertical_Multiplicator);
			}
			else
			{
				Force = new Vector2(Mathf.Cos(Angle * Mathf.Deg2Rad) * Attack_Force_x * Time.deltaTime, Mathf.Sin(Angle * Mathf.Deg2Rad) * Attack_Force_y * Time.deltaTime);
			}
			Player.GetComponent<Player>().AddDirectionalInput(Force, Attack_Force_Time);
			LastAttack = Time.time;
			Anim_Weapon.SetTrigger("Attack");
			Anim_Angle.SetTrigger("Attack");
			//add += 30;
		}
	}

	public void Attack2()
	{
		if (Time.time > FireRate + LastFire)
		{
			Weapon_Status.Attack2 = true;
			GameObject instance;
			instance = (GameObject)Instantiate(Projectile, gameObject.transform.Find("HandSprite").gameObject.transform.position, Quaternion.Euler(0, 0, Angle));
			instance.GetComponent<Projectile>().Fired_By_Player = true;
			Destroy(instance, 5.0f);
			LastFire = Time.time;
		}
	}

	public void Defend()
	{
		Weapon_Status.Defend = true;
		Weapon_Sprite.transform.localPosition = new Vector3(Weapon_Sprite.transform.localPosition.x, 0.0f, Weapon_Sprite.transform.localPosition.z);	}

	public void Stop_Defend()
	{
		Weapon_Status.Defend = false;
		Weapon_Sprite.transform.localPosition = new Vector3(Weapon_Sprite.transform.localPosition.x, 0.35f, Weapon_Sprite.transform.localPosition.z);	}

	public struct WeaponStatus {
		public bool Attack1, Attack2, Defend;
		public void reset()
		{
			Attack1 = Attack2 = Defend = false;
		}
	}
}
