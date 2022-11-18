using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    float health;
    float shield;
    [SerializeField] float hpmax = 50;
    [SerializeField] float sdmax = 20;

    Bullet bullet;
    [SerializeField] float shootCooldown = 1;
    float cldtimer;

    float ShootCooldownMultiplier;

    [SerializeField] float damageAddition = 0;
    [SerializeField] float damageMultiplier = 1;

    void Start()
    {
        health = hpmax;
        shield = sdmax;
    }

    protected virtual void Update()
    {
        cldtimer -= Time.deltaTime;
    }

    public void SetMaxHealth(float value)
    {
        hpmax = value;
        if (health > hpmax) {
            health = hpmax;
        }
    }

    public void SetMaxShield(float value)
    {
        sdmax = value;
        if (shield > sdmax) {
            shield = sdmax;
        }
    }

    public void SetShootCooldown(float value)
    {
        shootCooldown = value;
    }

    public virtual void Shoot()
    {
        if (cldtimer <= 0) {
            // create bullet
            if (Input.GetKeyDown(KeyCode.Space)) {
                bullet.SetDamage(damageAddition, damageMultiplier);
                bullet.SetSpeed();
                Instantiate(bullet.gameObject);

                cldtimer = shootCooldown;
            }
        }
    }
}
