using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour
{
    Image image;
    FadeInCanvas fadeIn;
    FadeOutCanvas fadeOut;

    [SerializeField] float duration = 0.2f;

    float time = 0f;

    [SerializeField] bool fade = false;


    void Start()
    {
        image = GetComponent<Image>();

        if (fade)
        {
            fadeOut = GetComponent<FadeOutCanvas>();
        }

    }


    void Update()
    {
        if (!fade)
        {
            if (image.enabled)
            {
                time += Time.deltaTime;

                if (time >= duration)
                {
                    RemoveImage();
                }
            }
        }
    }

    public void DisplayImage(float duration)
    {
        this.duration = duration;
        time = 0f;

        image.enabled = true;

        if (fade)
        {
            fadeOut.enabled = true;
            fadeOut.Fade(duration);
        }
        
    }

    public void RemoveImage()
    {
        if (image.enabled) {
            if (fade) {
                fadeOut.enabled = false;
            }
            
            image.enabled = false;

        }
        
    }


}
