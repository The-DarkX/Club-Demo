using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
	public TMP_Text timerText;
	public TMP_Text countdownText;

	public float countdownTime = 3;
	public float levelTime = 10;

	GameManager manager;

	private void Start()
	{
		manager = GetComponent<GameManager>();

		countdownText.gameObject.SetActive(true);
		timerText.gameObject.SetActive(false);

		StartCoroutine(CountdownToStart());
	}

	void Update()
	{
		if (manager.isTimerRunning)
		{
			if (levelTime >= 1f)
			{
				levelTime -= Time.deltaTime;

				DisplayTime(levelTime);
			}
			else
			{
				levelTime = 0;
				DisplayTime(0);
				manager.LoseSequence();
				manager.isTimerRunning = false;
			}
		}
	}

	void DisplayTime(float timeToDisplay)
	{
		//timeToDisplay += 1;

		float minutes = Mathf.FloorToInt(timeToDisplay / 60);
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);

		timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
	}

	IEnumerator CountdownToStart() 
	{
		while (countdownTime > 0) 
		{
			countdownText.text = countdownTime.ToString();

			yield return new WaitForSeconds(1f);

			countdownTime--;
		}

		countdownText.text = "GO!";
		timerText.gameObject.SetActive(true);
		manager.BeginGame();

		yield return new WaitForSeconds(1f);
		countdownText.gameObject.SetActive(false);
	}
}
