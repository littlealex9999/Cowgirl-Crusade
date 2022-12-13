using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public int sceneToLoad = 2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Return pressed");
            LoadScene(sceneToLoad);
        }
    }


    public void LoadScene(int sceneNumber)
    {
        // Play sound effect
        Debug.Log("Play");
        SceneManager.LoadScene(sceneNumber);
    }
}
