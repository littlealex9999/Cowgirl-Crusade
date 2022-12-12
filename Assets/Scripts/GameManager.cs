using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    static public GameManager instance { get; private set; }

    [SerializeField] Player player;

    GameOver gameOver;

    bool paused = false;
    bool canPause = true;

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

        gameOver = GetComponent<GameOver>();

    }

    void Update()
    {
        if (canPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (paused)
                {
                    ResumeGame();
                }
                else
                {
                    paused = true;
                    SuspendGame();
                }

            }
        }
        
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

    public void GameOver()
    {
        player.gameObject.SetActive(false);
        canPause = false;
        SuspendGame();
        gameOver.enabled = true;
        gameOver.PlayerDied();

    }

    void SuspendGame()
    {
        Time.timeScale = 0;

    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;

    }

    public void Respawn()
    {
        Debug.Log("Respawn");
        gameOver.enabled = false;
        player.gameObject.SetActive(true);
        player.GiveHealth(player.GetHealthMax(), true);
        
        Time.timeScale = 1;
        canPause = true;
    }

}
