using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoresObject", menuName = "Score/ScoreObject")]
public class Score : ScriptableObject
{

    [SerializeField] int[] scores = new int[10];

    [SerializeField] float maxMultiplier = 2.0f;


    [SerializeField] int points = 0;
    [SerializeField] float multiplier = 1.0f;

    public int GetPoints { get { return points; } }

    public float GetMultiplier { get { return multiplier; } }

    bool loaded = false;
    public bool LoadedScores { get { return loaded; } }

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

    public void SaveHighscoresToFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;
        if (File.Exists(destination)) {
            file = File.OpenWrite(destination);
        } else {
            file = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, scores);
        file.Close();
    }

    public void LoadHighscoresFromFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;
        if (File.Exists(destination)) {
            file = File.OpenRead(destination);
        } else {
            // no file found
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        int[] scoreArray = (int[])bf.Deserialize(file);
        file.Close();

        loaded = true;
    }


    void AddHighscore(int score)
    {
        for (int i = 0; i < scores.Length; ++i) {
            if (score > scores[i]) {
                int temp = scores[i];
                scores[i] = score;
                if (i < scores.Length - 1) {
                    scores[i + 1] = temp;
                }
            }
        }
    }

    public void OnGameQuit()
    {
        ResetPoints();
        SaveHighscoresToFile();
    }
}
