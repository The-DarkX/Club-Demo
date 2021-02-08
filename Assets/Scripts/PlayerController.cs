using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 0.25f;

	public LayerMask wallLayer;
	public LayerMask crateLayer;
	public LayerMask core;

	bool isAxisInUse = false;
	
	Vector3 movement;

	Vector3 startPos;
	Vector3 targetPos;

	bool canMove;

	Transform crate;

	GameManager gameManager;

	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
	}

	private void FixedUpdate()
	{
		Movement();

		RaycastHit hit;
		if (Physics.Raycast(transform.position, movement, out hit, 1, core)) 
		{
			if (hit.transform.CompareTag("Start"))
			{
				gameManager.StartTimer();
			}
			else if (hit.transform.CompareTag("Finish")) 
			{
				gameManager.EndTimer();
				canMove = false;
			}
		}
	}

	void Movement()
	{
		//Inputs
		Vector2 movementAxis = InputManager.playerMovement;
		movement = new Vector3(Mathf.RoundToInt(movementAxis.x), 0, Mathf.RoundToInt(movementAxis.y));

		if (canMove)
		{
			if (crate != null)
			{
				if (targetPos == crate.position)
				{
					crate.parent = transform;
				}

				if (Physics.Raycast(crate.position, movement, 0.5f, wallLayer))
				{
					canMove = false;
					transform.position = startPos;
					crate.parent = null;
					return;
				}
			}

			if (Vector3.Distance(startPos, transform.position) > 1f)
			{
				transform.position = targetPos;
				canMove = false;

				if (crate != null)
					crate.parent = null;

				return;
			}

			transform.position += (targetPos - startPos) * moveSpeed * Time.deltaTime;

			return;
		}

		RaycastHit hit;
		if (Physics.Raycast(transform.position, movement, out hit, 1, crateLayer))
		{
			crate = hit.transform;
		}
		else 
		{
			crate = null;
		}

		if (movement.x != 0 || movement.z != 0) //Button Pressed
		{
			if (Mathf.Abs(movement.x) == 1 && Mathf.Abs(movement.z) == 1) return; //Cancel diagonal movement

			if (isAxisInUse == false)
			{
				//Ignore if moving toward a wall
				if (Physics.Raycast(transform.position, movement, 1, wallLayer)) return;

				targetPos = transform.position + movement;
				startPos = transform.position;

				//Move
				canMove = true;

				transform.LookAt(targetPos);

				isAxisInUse = true;
			}
		}
		if (movement.x == 0 && movement.z == 0) //Not pressing anything
		{
			isAxisInUse = false;
		}
	}
}