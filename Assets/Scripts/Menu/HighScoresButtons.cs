using UnityEngine.SceneManagement;
using UnityEngine;

public class HighScoresButtons : MonoBehaviour 
{
	public void Return()
	{
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
}
