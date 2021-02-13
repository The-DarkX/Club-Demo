using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	public float moveSpeed = 0.25f;

	[SerializeField] LayerMask wallLayer;
	[SerializeField] LayerMask crateLayer;
	[SerializeField] LayerMask obstacleLayer;

	bool isAxisInUse = false;

	Vector3 startPos;
	Vector3 targetPos;

	bool canMove;

	Transform crate;

	CrateCollector[] collectors;

	private void Start()
	{
		collectors = FindObjectsOfType<CrateCollector>();
	}

	public void Move(Vector3 movementDirection) 
    {
		if (canMove)
		{
			if (crate != null)
			{
				if (targetPos == crate.position)
				{
					crate.parent = transform;

					foreach (CrateCollector collector in collectors)
					{
						if (targetPos + movementDirection == collector.gameObject.transform.position)
						{
							Destroy(crate.gameObject);
							break;
						}
					}
				}

				if (Physics.Raycast(crate.position, movementDirection, 0.5f, obstacleLayer))
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
		if (Physics.Raycast(transform.position, movementDirection, out hit, 1, crateLayer))
		{
			crate = hit.transform;
		}
		else
		{
			crate = null;
		}

		if (movementDirection.x != 0 || movementDirection.z != 0) //Button Pressed
		{
			if (Mathf.Abs(movementDirection.x) == 1 && Mathf.Abs(movementDirection.z) == 1) return; //Cancel diagonal movement

			if (isAxisInUse == false)
			{
				//Ignore if moving toward a wall
				if (Physics.Raycast(transform.position, movementDirection, 1, wallLayer)) return;

				targetPos = transform.position + movementDirection;
				startPos = transform.position;

				//Move
				canMove = true;

				transform.LookAt(targetPos);

				isAxisInUse = true;
			}
		}
		if (movementDirection.x == 0 && movementDirection.z == 0) //Not pressing anything
		{
			isAxisInUse = false;
		}
	}
}
