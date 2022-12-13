using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    bool canResume = false;

    int frameCount; // WaitForSeconds doesn't work if Time.timeScale = 0
    int delay = 120;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        frameCount++;
        Debug.Log(frameCount);

        if (canResume)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.instance.Respawn();
            }
        }
        else
        {
            frameCount++;

            if (frameCount >= delay)
            {
                canResume = true;
            }
        }

    }

    public void PlayerDied()
    {
        Debug.Log("Player died");
        
        canResume = false;
        frameCount = delay;
    }


}
