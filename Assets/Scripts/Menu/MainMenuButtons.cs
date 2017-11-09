using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour 
{
	public void PlayGame() //start new game
	{
		SceneManager.LoadScene("SceneOne", LoadSceneMode.Single);
	}
	public void HighScores() //show highscores
	{
		SceneManager.LoadScene("HighScores", LoadSceneMode.Single);
	}
	public void QuitGame() //exit to desktop
	{
		Application.Quit();
	}
}
