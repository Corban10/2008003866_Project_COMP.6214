using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;
public class HighScoresButtons : MonoBehaviour
{
    public int[] topTenScores = new int[10]; 
    // store highscores in an array after reading from file
	public Text hsText; //GUI text object
    void Start()
	{
		ReadHighScoresFromFile();
		DisplayHighScores();
	}
    //this method reads from the highscores.txt file and stores it into the topTenScores array
    void ReadHighScoresFromFile()
    {
        try
        {
            StreamReader sr = new StreamReader("HighScores.txt");
            for (int i = 0; i < topTenScores.Length; i++)
            {
                String line = sr.ReadLine();
                topTenScores[i] = string.IsNullOrEmpty(line) ? 0 : Convert.ToInt32(line);
            }
            sr.Close();
        }
        catch (Exception e)
        {
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
        }
    }
    //displays highscores to text ui element
	public void DisplayHighScores() 
    {
		hsText.text = "";
        for (int i = 0; i < topTenScores.Length; i++)
        {
			hsText.text += topTenScores[i] + "\n";
        }
    }
    public void Return()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
