using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    static public GameManager instance { get; private set; }

    [SerializeField] Player player;

    [SerializeField] Score scoreObject;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text multiplierText;

    [SerializeField] ShowImage hitmarker;

    public Score GetScore { get { return scoreObject; } }

    public Player GetPlayer { get { return player; } }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Cursor.visible = false;


        if (scoreObject != null && scoreObject.LoadedScores) {
            scoreObject.LoadHighscoresFromFile();
        }
    }

    void Update()
    {
        
    }

    public void UpdateScore()
    {
        scoreText.text = ("Score: " + scoreObject.GetPoints);
    }

    public void UpdateMultiplier()
    {
        multiplierText.text = ("X " + scoreObject.GetMultiplier);
    }

    public float DistanceFromPlayer(Transform target)
    {
        float distance = Vector3.Distance(player.transform.position, target.position);

        return distance;
    }

    public void HitEnemy()
    {
        hitmarker.DisplayImage(0.3f, 0.3f, 0.3f);
    }

    private void OnApplicationQuit()
    {
        if (scoreObject != null) {
            scoreObject.SaveHighscoresToFile();
        }
    }
}
