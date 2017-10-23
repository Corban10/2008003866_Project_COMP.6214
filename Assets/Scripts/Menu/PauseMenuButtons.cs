using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour 
{
    public Transform canvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy? true : false);
            Time.timeScale = !canvas.gameObject.activeInHierarchy? 1 : 0;
        }
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
}
