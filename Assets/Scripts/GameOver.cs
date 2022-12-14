using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Image = UnityEngine.UI.Image;

public class GameOver : MonoBehaviour
{
    public Image fadeTo;

    public float respawnDelay = 2;
    Stopwatch stopwatch = new Stopwatch();
    public int sceneToLoad = 2;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameManager.instance.Respawn();
            if (fadeTo != null) {
                Color c = fadeTo.color;
                c.a = 0;
                fadeTo.color = c;
                stopwatch.Stop();
            }
        }
#endif
        if (fadeTo != null) {
            Color c = fadeTo.color;
            c.a = Mathf.Lerp(0, 1, stopwatch.ElapsedMilliseconds / 1000 / respawnDelay);
            fadeTo.color = c;

            if (stopwatch.ElapsedMilliseconds >= respawnDelay * 1000) {
                Time.timeScale = 1;
                SceneManager.LoadScene(sceneToLoad);
            }
        } else {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void PlayerDied()
    {
        Debug.Log("Player died");

        stopwatch.Reset();
        stopwatch.Start();
    }


}
