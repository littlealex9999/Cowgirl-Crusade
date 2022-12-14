using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TweenableImage : MonoBehaviour
{
    Image image;

    [SerializeField] float duration = 0.2f;

    float timer = 0f;
    float animationEnterTime, animationExitTime;

    [SerializeField] bool fade = false;
    [SerializeField] bool scale = false;

    bool showing = false;
    bool exiting;

    float timeToExit, totalTime;

    void Start()
    {
        image = GetComponent<Image>();

        if (scale)
        {
            transform.DOScale(0f, 0);
        }

        if (fade)
        {
            image.DOFade(0f, 0);
        }


    }

    void Update()
    {
        if (showing)
        {
            timer += Time.deltaTime;

            if (timer >= totalTime)
            {
                HideImage();

            }else if (!exiting)
            {
                if(timer >= timeToExit)
                {
                    ExitAnimation(animationExitTime);
                }
                
            }
        }
    }

    public void DisplayImage(float duration, float animationEnterTime = 0.2f, float animationExitTime = 0.2f)
    {
        showing = true;
        this.duration = duration;
        this.animationEnterTime = animationEnterTime;
        this.animationExitTime = animationExitTime;

        timeToExit = duration + animationEnterTime;
        totalTime = timeToExit + animationExitTime;

        if (timer > 0)
        {
            ExitAnimation(0f);

        }

        timer = 0f;

        image.enabled = true;

        EnterAnimation();
        
    }


    void EnterAnimation()
    {
        if (fade)
        {
            image.DOFade(1f, animationEnterTime);
        }

        if (scale)
        {
            transform.DOScale(1f, animationEnterTime);
        }
    }


    void ExitAnimation(float exitTime)
    {
        if (fade)
        {
            image.DOFade(0f, exitTime);

        }

        if (scale)
        {
            transform.DOScale(0f, exitTime);

        }

    }

   
    public void HideImage()
    {
        if (image.enabled)
        {
            image.enabled = false;
            showing = false;
        }

    }


}
