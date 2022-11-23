using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    List<PowerupStats> powerups = new List<PowerupStats>();

    public enum TEAMS
    {
        NONE,
        PLAYER,
        ENEMY
    }
    [SerializeField] TEAMS team;

    public TEAMS getTeam { get { return team; } }

    void Start()
    {
        health = hpmax;
        shield = sdmax;
    }

    protected virtual void Update()
    {
        cldtimer -= Time.deltaTime;

        for (int i = 0; i < powerups.Count; ++i) {
            if (!powerups[i].permanent) {
                powerups[i].duration -= Time.deltaTime;
                if (powerups[i].duration <= 0) {
                    powerups.RemoveAt(i);
                    --i;
                }
            }
        }
    }

    #region stat setting
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
    #endregion

    #region stat getting
    public float GetHealthMax()
    {
        float output = hpmax;
        foreach (PowerupStats pus in powerups) {
            output += pus.health;
        }

        return output;
    }

    public float GetShieldMax()
    {
        float output = sdmax;
        foreach (PowerupStats pus in powerups) {
            output += pus.shield;
        }

        return output;
    }

    public float GetBulletDamageAdd()
    {
        float output = bulletDamageAddition;
        foreach (PowerupStats pus in powerups) {
            output += pus.bulletDamageAddition;
        }

        return output;
    }

    public float GetBulletDamageMult()
    {
        float output = bulletDamageMultiplier;
        foreach (PowerupStats pus in powerups) {
            output += pus.bulletDamageMultiplier;
        }

        return output;
    }

    public float GetBulletSpeedAdd()
    {
        float output = bulletSpeedAddition;
        foreach (PowerupStats pus in powerups) {
            output += pus.bulletSpeedAddition;
        }

        return output;
    }

    public float GetBulletSpeedMult()
    {
        float output = bulletSpeedMultiplier;
        foreach (PowerupStats pus in powerups) {
            output += pus.bulletSpeedMultiplier;
        }

        return output;
    }
    #endregion

    #region action methods
    public virtual void Shoot(Vector3 shootToPoint)
    {
        if (cldtimer <= 0) {
            // create bullet & set stats
            GameObject bulletRef = Instantiate(bullet.gameObject, transform.parent);
            Destroy(bulletRef, deleteBulletsAfterSeconds);
            bulletRef.transform.position = transform.position;
            bulletRef.transform.LookAt(shootToPoint);
            Bullet firedScript = bulletRef.GetComponent<Bullet>();

            firedScript.SetDamage(GetBulletDamageAdd(), GetBulletDamageMult());
            firedScript.SetSpeed(GetBulletSpeedAdd(), GetBulletSpeedMult());
            firedScript.SetTeam(team);

            cldtimer = shootCooldown;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (shield > damage) {
            shield -= damage;
            return;
        } else {
            damage -= shield;
            health -= damage;
        }

        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public virtual void AddPowerup(PowerupStats pus)
    {
        powerups.Add(pus);
        if (pus.health > 0) {
            health += pus.health;
        }
        if (pus.shield > 0) {
            shield += pus.shield;
        }
    }
    #endregion
}
