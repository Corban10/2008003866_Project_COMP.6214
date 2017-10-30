using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;
public class HighScoresButtons : MonoBehaviour
{
    public int[] topTenScores = new int[10];
	public Text hsText;
    void Start()
	{
		ReadHighScoresFromFile();
		DisplayHighScores();
	}
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
	public void DisplayHighScores() //to text ui element
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
