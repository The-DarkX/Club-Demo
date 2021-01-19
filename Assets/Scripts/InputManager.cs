using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	PlayerActions actions;

	public static Vector2 playerMovement { get; set; }
	public static Vector2 scrollDelta { get; set; }

	private void Awake()
	{
		actions = new PlayerActions();

		actions.Gameplay.Movement.performed += ctx => playerMovement = ctx.ReadValue<Vector2>();
		actions.Gameplay.Movement.canceled += ctx => playerMovement = Vector2.zero;

		actions.Gameplay.Look.performed += ctx => scrollDelta = ctx.ReadValue<Vector2>();
		actions.Gameplay.Look.canceled += ctx => scrollDelta = Vector2.zero;
	}

	private void OnEnable()
	{
		actions.Enable();
	}

	private void OnDisable()
	{
		actions.Disable();
	}
}
