using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    float health;
    float shield;
    [SerializeField] float hpmax = 50;
    [SerializeField] float sdmax = 20;

    [SerializeField] protected Bullet bullet;
    [SerializeField] float deleteBulletsAfterSeconds = 10;
    [SerializeField] float shootCooldown = 1;
    float cldtimer;

    float ShootCooldownMultiplier;

    [SerializeField, Header("Bullet Attributes"), Space] float bulletDamageAddition = 0;
    [SerializeField] float bulletDamageMultiplier = 1;
    [SerializeField] float bulletSpeedAddition = 0;
    [SerializeField] float bulletSpeedMultiplier = 1;

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

    public virtual void Shoot(Vector3 shootToPoint)
    {
        if (cldtimer <= 0) {
            // create bullet
            GameObject bulletRef = Instantiate(bullet.gameObject, transform.parent);
            Destroy(bulletRef, deleteBulletsAfterSeconds);
            bulletRef.transform.position = transform.position;
            bulletRef.transform.LookAt(shootToPoint);
            Bullet firedScript = bulletRef.GetComponent<Bullet>();

            firedScript.SetDamage(bulletDamageAddition, bulletDamageMultiplier);
            firedScript.SetSpeed(bulletSpeedAddition, bulletSpeedMultiplier);

            cldtimer = shootCooldown;
        }
    }
}
