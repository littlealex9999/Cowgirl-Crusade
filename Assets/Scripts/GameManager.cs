using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    static public GameManager instance { get; private set; }

    [SerializeField] Player player;
    VirtualCamera virtualCam;

    GameOver gameOver;

    bool paused = false;
    bool canPause = true;

    [SerializeField] Score scoreObject;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text multiplierText;

    [SerializeField] Image crosshair;
    [SerializeField] TweenableImage hitmarker;

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
        Cursor.lockState = CursorLockMode.Confined;

        if (scoreObject != null) {
            if (!scoreObject.LoadedScores)
                scoreObject.LoadHighscoresFromFile();

            scoreObject.ResetPoints(false);
        }

        virtualCam = player.GetVirtualCamera;

        gameOver = GetComponent<GameOver>();
    }

    void Update()
    {
        if (canPause)
        {
            if (Input.GetKeyDown(KeyCode.PageUp))
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

    public void ScreenShake(float amplitude, float intensity, float duration, bool fade = true)
    {
        virtualCam.ScreenShake(amplitude, intensity, duration, fade);
    }

    private void OnApplicationQuit()
    {
        if (scoreObject != null) {
            scoreObject.SaveHighscoresToFile();
        }
    }

    public void GameOver()
    {
        player.ControlsEnabled = false;
        player.gameObject.SetActive(false);
        canPause = false;
        //SuspendGame();
        gameOver.enabled = true;
        gameOver.PlayerDied();

        scoreObject.AddHighscore(scoreObject.GetPoints);
    }

    void SuspendGame()
    {
        Time.timeScale = 0;
        player.ControlsEnabled = false;

    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
        EnablePlayerControls();

    }

    public void Respawn()
    {
        Debug.Log("Respawn");
        gameOver.enabled = false;
        player.gameObject.SetActive(true);
        player.GiveHealth(player.GetHealthMax(), true);
        player.SetInvincibilityTime(1);
        EnablePlayerControls();
        
        Time.timeScale = 1;
        canPause = true;
    }

    void EnablePlayerControls()
    {
        player.ControlsEnabled = true;
    }
}
