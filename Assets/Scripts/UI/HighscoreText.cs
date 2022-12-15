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
    public TextMeshProUGUI countdown;

    public float timeToReturn = 10;

    float timer;

    void Start()
    {
        highscores.text = "Highscores \n";

        for (int i = 0; i < scoreObject.GetHighscores.Length; ++i) {
            highscores.text += scoreObject.GetHighscores[i].ToString() + "\n";
        }

        currentScore.text = "Your Score \n" + scoreObject.GetPoints.ToString();

        if (timeToReturn != 0) {
            Invoke("ReturnToMainMenu", timeToReturn);
            timer = (int) timeToReturn;
            countdown.SetText(timer.ToString("N0"));
        }
    }

    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            countdown.SetText(timer.ToString("N0"));
        }
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
