using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 0.25f;

	public LayerMask wallLayer;
	public LayerMask crateLayer;
	public LayerMask obstacleLayer;

	bool isAxisInUse = false;
	
	Vector3 movement;

	Vector3 startPos;
	Vector3 targetPos;

	bool canMove;

	Transform crate;

	CrateCollector[] collectors;

	private void Start()
	{
		collectors = FindObjectsOfType<CrateCollector>();
	}

	private void FixedUpdate()
	{
		Movement();
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

					foreach (CrateCollector collector in collectors)
					{
						if (targetPos + movement == collector.gameObject.transform.position) 
						{
							Destroy(crate.gameObject);
							print("destroyed");
							break;
						}
					}
				}

				if (Physics.Raycast(crate.position, movement, 0.5f, obstacleLayer))
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