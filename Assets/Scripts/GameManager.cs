using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance { get; private set; }

    [SerializeField] GameObject player;
    
    [SerializeField] Score scoreObject;

    [SerializeField] Text scoreText;
    [SerializeField] Text multiplierText;

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


}
