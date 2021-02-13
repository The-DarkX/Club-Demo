using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	MovementController controller;

	private void Start()
	{
		controller = GetComponent<MovementController>();
	}

	void FixedUpdate()
	{
		//Inputs
		Vector2 movementAxis = InputManager.playerMovement;
		Vector3 movement = new Vector3(Mathf.RoundToInt(movementAxis.x), 0, Mathf.RoundToInt(movementAxis.y));

		controller.Move(movement);
	}
}