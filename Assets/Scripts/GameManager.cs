using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{

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