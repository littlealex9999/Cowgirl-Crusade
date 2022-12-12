using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInCanvas : MonoBehaviour
{
    CanvasGroup canvas;

    [SerializeField] bool fading = false;

    [SerializeField] float duration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            if (canvas.alpha < 1)
            {
                canvas.alpha += (Time.deltaTime/duration);
            }
            else
            {
                fading = false;
            }
            
        }

    }


    public void Fade(float fadeDuration)
    {
        fading = true;
        duration = fadeDuration;
    }

}
