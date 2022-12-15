using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusicOnLoad : MonoBehaviour
{
    public static DontDestroyMusicOnLoad instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}
