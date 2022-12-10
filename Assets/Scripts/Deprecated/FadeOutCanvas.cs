using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeOutCanvas : MonoBehaviour
{
    CanvasGroup canvas;

    public bool fading = false;

    [SerializeField] float duration = 1f;

    ShowImage showImage;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        showImage = GetComponent<ShowImage>();

    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            if (canvas.alpha > 0)
            {
                canvas.alpha -= (Time.deltaTime / duration);
            }
            else
            {
                fading = false;

                if (showImage != null)
                {
                    showImage.HideImage();
                }
            }

        }
        
    }


    public void Fade(float fadeDuration)
    {
        canvas.alpha = 1;
        fading = true;
        duration = fadeDuration;
    }

}