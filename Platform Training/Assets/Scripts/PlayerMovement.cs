using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float Speed = 5;
	public float JumpSpeed = 5;

	public SidesCollisions Grounded;

	public LayerMask GroundingSurface;

	Rigidbody2D rigi;


	void Update () {
		CheckSidesCollision();
		Move();
		CheckJump();
	}

	void Move()
	{
		rigi = GetComponent<Rigidbody2D>();
		float moveHorizontal = Input.GetAxis("Horizontal");
		//float moveVertical = Input.GetAxis("Vertical");

		Vector2 velocity = new Vector2(moveHorizontal,0);

		rigi.AddForce(velocity * Speed);
	}

	void CheckJump()
	{
	}

	void jump()
	{
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpSpeed);
	}

	public struct SidesCollisions {
		public bool Right, Left, Up, Down;
		public void reset()
		{
			Right = Left = false;
			Up = Down = false;
		}
	}

	void CheckSidesCollision()
	{
		Grounded.reset();

		RaycastHit2D Left = Physics2D.Raycast(transform.position,Vector2.left,Mathf.Infinity,GroundingSurface);

		if (Left && Left.distance == 0)
		{
			Grounded.Left = true;
			Debug.Log("Grounded Down");
		}

		RaycastHit2D Right = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, GroundingSurface);
		if (Right.distance <= 0.001)
		{
			Grounded.Right = true;
		}

		RaycastHit2D Up = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, GroundingSurface);
		if (Up.distance <= 0.001)
		{
			Grounded.Up = true;
		}

		RaycastHit2D Down = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, GroundingSurface);
		if (Down.distance <= 0.001)
		{
			Grounded.Down = true;
		}

	}
}
