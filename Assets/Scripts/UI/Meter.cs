using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Meter : MonoBehaviour
{
    Player player;

    Image meter;

    Character owner;

    Color defaultColor;

    private enum MeterType { Health, Shield, WeaponHeat };

    [SerializeField] private MeterType meterType;

    public Color negativeColor, positiveColor;

    [SerializeField] bool animating = false;

    float timer = 0f;
    float duration = 0.5f;
    float change = 0f;

    public delegate void MeterEvent();
    public MeterEvent runOnFullMeter;
    public MeterEvent runOnEmptyMeter;
    public MeterEvent runOnAnimationEnd;
    public MeterEvent runOnChange;
    public MeterEvent runOnPositiveChange;
    public MeterEvent runOnNegativeChange;

    bool fullMeter;
    bool emptyMeter;

    private void Start()
    {
        meter = GetComponent<Image>();


        defaultColor = meter.color;

    }


    void Update()
    {
        if (animating) {
            timer += Time.deltaTime;

            if (timer >= duration) { // completed animation
                if (fullMeter && runOnFullMeter != null) {
                    runOnFullMeter.Invoke();
                    fullMeter = false;
                } else if (emptyMeter && runOnEmptyMeter != null) {
                    runOnEmptyMeter.Invoke();
                    emptyMeter = false;
                }

                if (runOnAnimationEnd != null)
                    runOnAnimationEnd.Invoke();
                animating = false;
            } else { // still animating
                if (runOnChange != null)
                    runOnChange.Invoke();
                if (change > 0) {
                    if (runOnPositiveChange != null)
                        runOnPositiveChange.Invoke();
                } else {
                    if (runOnNegativeChange != null)
                        runOnNegativeChange.Invoke();
                }
            }
        }
    }

    public void SetOwner(Character owner)
    {
        this.owner = owner;
        player = GameManager.instance.GetPlayer;

    }

    public void UpdateMeter(float currentValue, float maxValue, float change, bool changeColor = true, float duration = 0.5f)
    {

        float percentage = Mathf.Clamp(currentValue / maxValue, 0, 1);

        timer = 0f;
        this.duration = duration;
        this.change = change;

        animating = true;


        if (changeColor) {
            if (change < 0) {
                SetMeterColor(negativeColor, duration);
            } else {
                SetMeterColor(positiveColor, duration);
            }
        }


        meter.DOFillAmount(percentage, duration);

        CheckConditions(percentage);

    }

    void CheckConditions(float percentage)
    {
        if (percentage >= 1) {
            fullMeter = true;
        } else if (percentage <= 0) {
            emptyMeter = true;
        }
    }


    public void SetMeterColor(Color color, float duration = 0.5f)
    {
        meter.DOColor(color, duration);
    }

    public void ResetMeterColor(float duration = 0.5f)
    {
        meter.DOColor(defaultColor, duration);
    }
}
