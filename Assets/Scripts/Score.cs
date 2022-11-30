using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoresObject", menuName = "Score/ScoreObject")]
public class Score : ScriptableObject
{

    [SerializeField] int[] scores = new int[10];

    [SerializeField] float maxMultiplier = 2.0f;


    int points = 0;
    float multiplier = 1.0f;

    public int GetPoints { get { return points; } }

    public float GetMultiplier { get { return multiplier; } }

    public void AddMultiplier(float increase)
    {
        if (multiplier < maxMultiplier)
        {
            multiplier += increase;
            GameManager.instance.UpdateMultiplier();
        }
    }

    public void ResetMultiplier()
    {
        multiplier = 1;
        GameManager.instance.UpdateMultiplier();

    }



    public void AddPoints(int amount)
    {
        points += (int)(amount * multiplier);
        GameManager.instance.UpdateScore();

    }


    public void ResetPoints()
    {
        AddHighscore(points);
        points = 0;
        GameManager.instance.UpdateScore();
    }

    void SaveHighscoresToFile()
    {

    }


    void AddHighscore(int score)
    {

    }

    public void OnGameQuit()
    {
        ResetPoints();
        SaveHighscoresToFile();
    }
}
