using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//public float movementDelay = 1;

	public LayerMask wallLayer;

	bool isAxisInUse = false;

	Vector3 movement;
	Vector3 forwardDirection;

	private void FixedUpdate()
	{
		Movement();
	}

	void Movement()
	{
		//Inputs
		Vector2 movementAxis = InputManager.playerMovement;
		movement = new Vector3(Mathf.RoundToInt(movementAxis.x), 0, Mathf.RoundToInt(movementAxis.y));
		forwardDirection = transform.position + movement;

		if (movement.x != 0 || movement.z != 0) //Button Pressed
		{
			if (isAxisInUse == false)
			{
				//Ignor if moving toward a wall
				if (Physics.Raycast(transform.position, movement, 1, wallLayer)) return;

				transform.LookAt(forwardDirection);

				//Move
				transform.position += movement;

				isAxisInUse = true;
			}
		}
		if (movement.x == 0 && movement.z == 0) //Not pressing anything
		{
			isAxisInUse = false;
		}
	}

	private void OnDrawGizmos()
	{
		//Forward Direction representation
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, forwardDirection);
	}
}