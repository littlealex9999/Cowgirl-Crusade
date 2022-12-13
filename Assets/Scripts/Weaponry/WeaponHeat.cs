using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class WeaponHeat : MonoBehaviour
{
    [Header("Weapon Heat"), Space]
    [SerializeField] Meter weaponMeter;
    [SerializeField] float heatLimit = 100f;
    [SerializeField] float shotCost = 5f;
    [SerializeField] float standardCooldown = 10f;
    [SerializeField] float overheatedCooldown = 10f;
    // [SerializeField] float delayCooldown = 1f;

    float cooldown;

    [SerializeField] Color overheatColor = Color.red;

    [SerializeField] TMP_Text overheat_Text;

    float weaponHeat = 0f;
    bool overheated = false;
    public bool coolingDown = false;

    bool changeColor = true;

    public bool Overheated { get { return overheated; } set { overheated = value; } }

    
    // Start is called before the first frame update
    void Start()
    {
        cooldown = standardCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (coolingDown)
        {
            WeaponCooldown();
        }
    }


    public void WeaponCooldown()
    {
        weaponHeat -= (cooldown * Time.deltaTime);
        weaponHeat = Mathf.Clamp(weaponHeat, 0, heatLimit);

        if (overheated)
        {
            if (weaponHeat <= 0)
            {
                FinishedOverheating();
            }
        }

        weaponMeter.UpdateMeter(weaponHeat, heatLimit, -cooldown, false, 0);

    }

    public void AddWeaponHeat()
    {
        weaponHeat += shotCost;

        weaponHeat = Mathf.Clamp(weaponHeat, 0, heatLimit);

        weaponMeter.UpdateMeter(weaponHeat, heatLimit, shotCost, true);

    }

    public void Overheat()
    {
        cooldown = overheatedCooldown;
        overheated = true;

        overheat_Text.DOFade(1, 0.5f);
        overheat_Text.transform.DOScale(1, 0.5f);

        weaponMeter.SetMeterColor(Color.red);

    }

    void FinishedOverheating()
    {
        overheated = false;
        cooldown = standardCooldown;

        overheat_Text.DOFade(0, 0.5f);
        overheat_Text.transform.DOScale(0, 0.5f);

        weaponMeter.ResetMeterColor();
    }

}
