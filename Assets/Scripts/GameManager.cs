using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
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