using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Meter : MonoBehaviour
{
    Player player;
    
    Image meter;

    [SerializeField] WeaponHeat weaponHeat;
    bool triggerOverheat = false;

    Character owner;

    Color defaultColor;

    private enum MeterType { Health, Shield, WeaponHeat };

    [SerializeField] private MeterType meterType;

    [SerializeField] Color negativeColor, positiveColor;

    bool changeColor = true;

    bool animating = false;
    bool triggerDeath = false;

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
                if (changeColor)
                {
                    ResetMeterColor(0.1f);
                }

                if (triggerDeath)
                {
                    GameManager.instance.GameOver();
                    triggerDeath = false;
                }


                if (weaponHeat != null)
                {
                    if (triggerOverheat)
                    {
                        weaponHeat.Overheat();
                        triggerOverheat = false;
                    }

                    weaponHeat.coolingDown = true;

                }
                
                animating = false;

            }
        }

    }

    public void SetOwner(Character owner) {
        this.owner = owner;
        player = GameManager.instance.GetPlayer;

    }

    public void UpdateMeter(float currentValue, float maxValue, float change, bool changeColor = true, float duration = 0.5f)
    {

        float percentage = Mathf.Clamp(currentValue / maxValue, 0, 1);

        timer = 0f;
        this.duration = duration;
        this.changeColor = changeColor;

        animating = true;
        

        if (changeColor)
        {
            if (change < 0)
            {
                SetMeterColor(negativeColor, duration);
            }
            else
            {
                SetMeterColor(positiveColor, duration);
            }
        }
        

        meter.DOFillAmount(percentage, duration);

        CheckConditions(percentage);
        
    }

    void CheckConditions(float percentage)
    {
        if (weaponHeat != null)
        {
            if (percentage >= 1)
            {
                triggerOverheat = true;
            }

        }
        else
        {
            if (meterType == MeterType.Health)
            {
                if (percentage <= 0)
                {
                    if (owner == player)
                    {
                        triggerDeath = true;
                    }
                }
            }
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
