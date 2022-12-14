using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreText : MonoBehaviour
{
    public Score scoreObject;
    public TextMeshProUGUI highscores;
    public TextMeshProUGUI currentScore;

    public float timeToReturn = 10;

    void Start()
    {
        highscores.text = "Highscores \n";

        for (int i = 0; i < scoreObject.GetHighscores.Length; ++i) {
            highscores.text += scoreObject.GetHighscores[i].ToString() + "\n";
        }

        currentScore.text = "Your Score \n" + scoreObject.GetPoints.ToString();

        Invoke("ReturnToMainMenu", timeToReturn);
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnApplicationQuit()
    {
        // just incase the game exits on the game over screen
        scoreObject.SaveHighscoresToFile();
    }
}
