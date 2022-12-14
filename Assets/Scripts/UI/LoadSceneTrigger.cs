using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTrigger : MonoBehaviour
{
    public int sceneToLoad;
    public bool onlyOnPlayerEnter;

    void OnTriggerEnter(Collider other)
    {
        if (onlyOnPlayerEnter && other.tag != "Player")
            return;

        SceneManager.LoadScene(sceneToLoad);
    }

}
