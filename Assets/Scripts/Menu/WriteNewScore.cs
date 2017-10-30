using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System;
public class WriteNewScore : MonoBehaviour
{
    public int[] topTenScores = new int[10];
    public int newScore;
    public void GetNewScore()
    {
        newScore = PlayerScript.score; //get this from recent score after gameover
    }
    public void UpdateScore()
    {
        ReadHighScoresFromFile();
        if (newScore > topTenScores[topTenScores.Length - 1])
        {
            //sorting logic
            topTenScores[topTenScores.Length - 1] = newScore;
            Array.Sort(topTenScores);
            Array.Reverse(topTenScores);
            WriteHighScoresToFile();
        }
    }
    public void ReadHighScoresFromFile()
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
    public void WriteHighScoresToFile()
    {
        try
        {
            using (StreamWriter outputFile = new StreamWriter("HighScores.txt"))
            {
                foreach (int line in topTenScores)
                    outputFile.WriteLine(line);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Could not write to file: ");
            Debug.Log(e);
        }
    }
    public void DebugHighScores()
    {
        for (int i = 0; i < topTenScores.Length; i++)
        {
            Debug.Log(topTenScores[i]);
        }
    }
    public void Return()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
