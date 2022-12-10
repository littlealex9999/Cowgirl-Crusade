using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShowImage : MonoBehaviour
{
    Image image;

    [SerializeField] float duration = 0.2f;

    float timer = 0f;
    float animationEnterTime, animationExitTime;

    [SerializeField] bool fade = false;
    [SerializeField] bool scale = false;

    bool showing = false;
    bool exiting;

    float timeToTriggerExitAnimation, overallTime;



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

            if (timer >= overallTime)
            {
                HideImage();

            }else if (!exiting)
            {
                if(timer >= timeToTriggerExitAnimation)
                {
                    ExitAnimation();
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

        timeToTriggerExitAnimation = duration + animationEnterTime;
        overallTime = timeToTriggerExitAnimation + animationExitTime;

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


    void ExitAnimation()
    {
        if (fade)
        {
            image.DOFade(0f, animationExitTime);

        }

        if (scale)
        {
            transform.DOScale(0f, animationExitTime);

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
