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
            // pauses game on escape key, resumes on second press
            canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy ? true : false);
            Time.timeScale = !canvas.gameObject.activeInHierarchy ? 1 : 0;
        }
        if (PlayerScript.playerLives <= 0)// if player lives less than 0, waitthengameover coroutine
        {
            StartCoroutine("WaitThenGameOver");
        }
    }
    IEnumerator WaitThenGameOver() // wait 2 seconds then bring up respawn screen canvas
    {
        yield return new WaitForSeconds(2);
        respawnCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ExitToMainMenu() //Exits to main menu
    {
        if (PlayerScript.playerLives > 0)
        {
            GetComponent<WriteNewScore>().GetNewScore();
            GetComponent<WriteNewScore>().UpdateScore();
        }
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    public void Resume() //exits pause menu
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart() //restarts game
    {
        respawnCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("SceneOne", LoadSceneMode.Single);
    }
}
