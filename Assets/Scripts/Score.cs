using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int points = 0;
    float multiplier = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
        points = 0;
    }

    private void UpdateDisplay()
    {

    }



}
