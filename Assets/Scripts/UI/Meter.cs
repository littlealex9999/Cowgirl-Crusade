using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Meter : MonoBehaviour
{
    Image meter;

    Color defaultColor;

    [SerializeField] Color negativeColor, positiveColor;

    bool animating = false;

    float timer = 0f;
    float duration = 0.5f;

    private void Start()
    {
        meter = GetComponent<Image>();
        
        defaultColor = meter.color;
    }


    void Update()
    {
        if (animating)
        {
            timer += Time.deltaTime;

            if (timer >= duration)
            {
                ResetMeterColor(0.1f);

            }
        }
    }


    public void UpdateMeter(float currentValue, float maxValue, float change, float duration = 0.5f)
    {
        
        float percentage = Mathf.Clamp(currentValue / maxValue, 0, 1);

        timer = 0f;
        this.duration = duration;

        animating = true;
        
        if (change < 0)
        {
            meter.DOColor(negativeColor, duration);
            
        }
        else
        {
            meter.DOColor(positiveColor, duration);
        }

        Debug.Log(percentage);
        meter.DOFillAmount(percentage, duration);
        
    }


    void ResetMeterColor(float duration)
    {
        animating = false;
        meter.DOColor(defaultColor, duration);
    }


}
