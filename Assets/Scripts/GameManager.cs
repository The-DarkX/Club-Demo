using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public GameObject timeOutUI;
	public GameObject winUI;

	public bool isTimerRunning = false;

	List<CrateID> crates = new List<CrateID>();

	private void Start()
	{
		timeOutUI.gameObject.SetActive(false);
		winUI.gameObject.SetActive(false);
		
		crates.AddRange(FindObjectsOfType<CrateID>().ToList());
	}

	private void Update()
	{
		print(crates.Count);

		for (int i = 0; i < crates.Count; i++) 
		{
			if (crates[i] == null) 
			{
				crates.RemoveAt(i);
			}
		}

		if (crates.Count == 0) 
		{
			WinSequence();
		}
	}

	public void BeginGame() 
	{
		isTimerRunning = true;
		FindObjectOfType<PlayerController>().enabled = true;
	}

	public void WinSequence() 
	{
		Time.timeScale = 0;

		winUI.gameObject.SetActive(true);
	}

	public void LoseSequence() 
	{
		Time.timeScale = 0;

		timeOutUI.gameObject.SetActive(true);
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
}