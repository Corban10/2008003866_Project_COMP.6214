using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour 
{
	public void PlayGame()
	{
		SceneManager.LoadScene("SceneOne", LoadSceneMode.Single);
		//Application.LoadLevel(1);
	}
	public void HighScores()
	{
		SceneManager.LoadScene("HighScores", LoadSceneMode.Single);
		//Application.LoadLevel(1);
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
