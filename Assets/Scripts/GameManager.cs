using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public TMP_Text timerText;
	public float timeRemaining = 10;

	public bool isTimerRunning = false;

	private void Update()
	{
		if (isTimerRunning) 
		{
			if (timeRemaining > 0)
			{
				timeRemaining -= Time.deltaTime;
			}
			else 
			{
				LoseSequence();

				timeRemaining = 0;
				isTimerRunning = false;
			}

			DisplayTime(timeRemaining);
		}
	}

	void DisplayTime(float timeToDisplay) 
	{
		float minutes = Mathf.FloorToInt(timeToDisplay / 60);
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);

		timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
	}

	#region Spawner
	public static void Spawn(GameObject prefab, Vector3 position)
	{
		Instantiate(prefab, position, Quaternion.identity);
	}

	public static void Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		Instantiate(prefab, position, rotation);
	}

	public static void Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent) 
	{
		Instantiate(prefab, position, rotation, parent);
	}
	#endregion

	#region Scene Management
	public static void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public static void LoadNext() 
	{
		if (SceneManager.sceneCount < SceneManager.GetActiveScene().buildIndex)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public static void LoadPrevious() 
	{
		if (SceneManager.sceneCount > 0)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
	#endregion

	public void StartTimer() 
	{
		isTimerRunning = true;
	}

	public void EndTimer() 
	{
		isTimerRunning = false;
		WinSequence();
	}

	void WinSequence() 
	{
		print("You Won!");
	}

	void LoseSequence() 
	{
		print("Time Ran Out!");
	}
}