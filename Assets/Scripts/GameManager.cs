using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    static public GameManager instance { get; private set; }

    [SerializeField] GameObject player;

    // [SerializeField] CinemachineVirtualCamera virtualCamera;

    [SerializeField] CinemachineVirtualCamera virtualCamera;

    [SerializeField] Score scoreObject;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text multiplierText;

    [SerializeField] Hitmarker hitmarker;

    public Score GetScore { get { return scoreObject; } }

    public GameObject GetPlayer { get { return player; } }

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

    public void ScreenShake(float intensity, float duration)
    {
        virtualCamera.GetComponent<CameraShake>().Shake(intensity, duration);
    }

    public void HitEnemy()
    {
        hitmarker.HitEnemy();
    }

}
