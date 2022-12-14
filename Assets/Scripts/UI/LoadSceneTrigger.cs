using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTrigger : MonoBehaviour
{
    public int sceneToLoad;
    public bool onlyOnPlayerEnter;

    public bool saveCurrentScore;

    void OnTriggerEnter(Collider other)
    {
        if (onlyOnPlayerEnter && other.tag != "Player")
            return;

        if (saveCurrentScore && GameManager.instance.GetScore != null)
            GameManager.instance.GetScore.AddHighscore(GameManager.instance.GetScore.GetPoints);

        SceneManager.LoadScene(sceneToLoad);
    }

}
