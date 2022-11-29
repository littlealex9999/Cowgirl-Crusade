using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoresObject", menuName = "Score/ScoreObject")]
public class Score : ScriptableObject
{

    [SerializeField] int[] scores = new int[10];

    int points = 0;
    float multiplier = 1.0f;

    public void AddMultiplier(float increase)
    {
        multiplier += increase;
    }

    public void ResetMultiplier()
    {
        multiplier = 1;
    }



    public void AddPoints(int amount)
    {
        points += (int)(amount * multiplier);

    }


    public void ResetPoints()
    {
        AddHighscore(points);
        points = 0;
    }

    void SaveHighscoresToFile()
    {

    }

    void UpdateDisplay()
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
