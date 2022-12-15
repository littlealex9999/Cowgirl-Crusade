using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class WeaponHeat : MonoBehaviour
{
    Player player;
    CrosshairColors crosshair;

    [Header("Weapon Heat"), Space]
    [SerializeField] Meter weaponMeter;
    [SerializeField] float heatLimit = 100f;
    [SerializeField] float shotCost = 5f;
    [SerializeField] float standardCooldown = 10f;
    [SerializeField] float overheatedCooldown = 10f;
    [SerializeField] float delayCooldown = 0.5f;
    float delayTimer;

    float cooldown;

    [SerializeField] Color overheatColor = Color.red;

    [SerializeField] TMP_Text overheat_Text;

    [SerializeField] HideInactiveUI uiHider;


    float weaponHeat = 0f;
    bool overheated = false;
    public bool coolingDown = false;

    bool changeColor = true;

    public bool Overheated { get { return overheated; } set { overheated = value; } }


    // Start is called before the first frame update
    void Start()
    {
        cooldown = standardCooldown;

        if (weaponMeter != null) {
            weaponMeter.runOnFullMeter += Overheat;
            weaponMeter.runOnEmptyMeter += EmptyMeter;
            weaponMeter.runOnAnimationEnd += SetCoolingTrue;
        }

        player = GetComponent<Player>();
        crosshair = GetComponent<CrosshairColors>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTimer <= 0) {
            coolingDown = true;

            //if (!overheated)
               //weaponMeter.SetMeterColor(weaponMeter.negativeColor);
        } else {
            delayTimer -= Time.deltaTime;
        }

        if (coolingDown) {
            WeaponCooldown();
        }
    }


    public void WeaponCooldown()
    {
        

        weaponHeat -= (cooldown * Time.deltaTime);
        weaponHeat = Mathf.Clamp(weaponHeat, 0, heatLimit);

        if (overheated) {
            weaponMeter.UpdateMeter(weaponHeat, heatLimit, -cooldown, false, true, 0);
            if (weaponHeat <= 0) {
                FinishedOverheating();
            }
        } else {
            weaponMeter.UpdateMeter(weaponHeat, heatLimit, -cooldown, false, false, 0);
        }

    }

    public void AddWeaponHeat()
    {
        if (uiHider != null) {
            uiHider.Activate();
        }

        weaponHeat += shotCost;

        if (coolingDown) {
            coolingDown = false;
        }

        weaponHeat = Mathf.Clamp(weaponHeat, 0, heatLimit);

        weaponMeter.UpdateMeter(weaponHeat, heatLimit, shotCost, false, false, player.getCooldown);

    }

    public void Overheat()
    {
        cooldown = overheatedCooldown;
        overheated = true;

        overheat_Text.DOFade(1, 0.5f);
        overheat_Text.transform.DOScale(1, 0.5f);
        overheat_Text.transform.DORotate(new Vector3(0, 0, 720), 0.5f, RotateMode.LocalAxisAdd);

        GameManager.instance.ScreenShake(15, 3, 0.8f);

        crosshair.AbleToShoot(false);

        weaponMeter.SetMeterColor(overheatColor);
    }

    void FinishedOverheating()
    {
        overheated = false;
        cooldown = standardCooldown;

        overheat_Text.DOFade(0, 0.5f);
        overheat_Text.transform.DOScale(0, 0.5f);
        overheat_Text.transform.DORotate(new Vector3(0, 0, -720), 2f, RotateMode.LocalAxisAdd);

        crosshair.AbleToShoot(true);

        weaponMeter.ResetMeterColor();
    }

    void SetCoolingTrue()
    {
        delayTimer = delayCooldown;
    }

    void EmptyMeter()
    {
        uiHider.Idle();
    }
}