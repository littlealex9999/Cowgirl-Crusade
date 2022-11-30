using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance { get; private set; }
    
    [SerializeField] Score scoreObject;

    [SerializeField] Text scoreText;
    [SerializeField] Text multiplierText;

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
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

}
