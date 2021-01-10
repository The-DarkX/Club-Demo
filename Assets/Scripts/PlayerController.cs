using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float movementDelay = 1f;

	public LayerMask wallLayer;

	public float delay;

	private void Start()
	{
		delay = movementDelay;
	}

	private void FixedUpdate()
	{
		Movement();
	}

	void Movement()
	{
		int horizontal = 0;
		int vertical = 0;

		//Inputs
		Inputs(ref horizontal, ref vertical);

		Vector3 movementDirection = new Vector3(horizontal, 0, vertical).normalized;

		if (Physics.Raycast(transform.position, movementDirection, 1f, wallLayer)) return;

		transform.position += movementDirection;

		delay = movementDelay;
	}

	void Inputs(ref int horizontal, ref int vertical)
	{
		if (Input.GetKeyUp(KeyCode.A))
		{
			horizontal = -1;
		}
		else if (Input.GetKeyUp(KeyCode.D))
		{
			horizontal = 1;
		}
		else if (Input.GetKeyUp(KeyCode.S))
		{
			vertical = -1;
		}
		else if (Input.GetKeyUp(KeyCode.W))
		{
			vertical = 1;
		}
		else
		{
			horizontal = 0;
			vertical = 0;
		}
	}
}
