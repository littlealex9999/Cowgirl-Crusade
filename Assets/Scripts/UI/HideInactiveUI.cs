using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HideInactiveUI : MonoBehaviour
{
    [SerializeField] Image[] images;

    [SerializeField] float delay = 1.5f;
    [SerializeField] float fadeTime = 0.25f;
    [SerializeField] bool displaying = false;

    Tween show, hide;

    float timer = 0f;
    bool idle = false;

    private void Start()
    {
        if (!displaying) {
            HideElements(0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (idle && displaying) {
            if (timer > 0) {
                Debug.Log(timer);
                timer -= Time.deltaTime;
            } else {
                HideElements(fadeTime);
            }
        }
    }

    public void Activate()
    {
        if (idle) {
            foreach (Image i in images) {
                
                if (hide.IsActive()) {
                    hide.Restart();
                    hide.Kill();
                }
                
                show = i.DOFade(1f, fadeTime);
                idle = false;
                displaying = true;
            }
        }
    }

    public void Idle()
    {
        if (!idle) {
            timer = delay;
            idle = true;
        }
    }

    public void HideElements(float duration)
    {
        displaying = false;
        Debug.Log("Hide UI");
        foreach(Image i in images) {

            if (show.IsActive()) {
                show.Restart();
                show.Kill();
            }

            hide = i.DOFade(0f, duration);
        }
    }

}
