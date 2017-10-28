using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour 
{
	public Transform respawnCanvas;
    public Transform canvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy? true : false);
            Time.timeScale = !canvas.gameObject.activeInHierarchy? 1 : 0;
        }
        if (PlayerScript.playerLives <= 0)
        {
			StartCoroutine("WaitThenGameOver");
        }
    }
    IEnumerator WaitThenGameOver()
    {
        yield return new WaitForSeconds(2);
        respawnCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ExitToMainMenu()
	{
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Time.timeScale = 1;
	}
    public void Resume()
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        respawnCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("SceneOne", LoadSceneMode.Single);
    }
}
