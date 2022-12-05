using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{
    float health;
    float shield;
    [SerializeField, InspectorName("Max Health")] float hpmax = 50;
    [SerializeField, InspectorName("Max Shield")] float sdmax = 20;

    float invincibleTime;

    [SerializeField] protected Bullet bullet;
    [SerializeField] float deleteBulletsAfterSeconds = 10;
    [SerializeField] float shootCooldown = 1;
    [SerializeField, InspectorName("Spawn Shoot Cooldown")] float cldtimer;
    [SerializeField] int maxSpecialProjectiles = 3;
    SpecialProjectile[] specialProjectiles;

    [SerializeField] Vector2 turnMult = new Vector2(100, 100);
    [SerializeField] float resetRotationStrength = 4f;

    [SerializeField] int pointsGiven;

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

    private Vector3 posLastFrame;

    protected void Start()
    {
        health = hpmax;
        shield = sdmax;

        posLastFrame = transform.position;
        specialProjectiles = new SpecialProjectile[maxSpecialProjectiles];
    }

    protected virtual void Update()
    {
        cldtimer -= Time.deltaTime;
        invincibleTime -= Time.deltaTime;

        for (int i = 0; i < powerups.Count; ++i) {
            if (!powerups[i].permanent) {
                powerups[i].duration -= Time.deltaTime;
                if (powerups[i].duration <= 0) {
                    powerups.RemoveAt(i);
                    --i;
                }
            }
        }


        // ROTATION
        if (transform.parent != null) {
            if (posLastFrame != transform.position) {
                Vector3 direction = (transform.position - posLastFrame).normalized;

                transform.Rotate(transform.forward, Vector3.Dot(direction, -transform.right) * turnMult.x * Time.deltaTime, Space.World);
                transform.Rotate(transform.parent.right, Vector3.Dot(direction, transform.parent.up) * turnMult.y * -1 * Time.deltaTime, Space.World);

                posLastFrame = transform.position;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.parent.rotation, resetRotationStrength * Time.deltaTime);
        }
    }

    protected virtual void OnDestroy()
    {
        GameManager.instance.GetScore.AddPoints(pointsGiven);
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

    public float getCurrentHealth { get { return health; } }

    public float GetShieldMax()
    {
        float output = sdmax;
        foreach (PowerupStats pus in powerups) {
            output += pus.shield;
        }

        return output;
    }

    public float getCurrentShield { get { return shield; } }

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
    public virtual Bullet Shoot(Vector3 shootToPoint, Transform parentOverride = null)
    {
        if (cldtimer <= 0) {
            // create bullet & set stats
            if (parentOverride == null) {
                parentOverride = transform.parent;
            }
            GameObject bulletRef = Instantiate(bullet.gameObject, parentOverride);
            Destroy(bulletRef, deleteBulletsAfterSeconds);
            bulletRef.transform.position = transform.position;
            bulletRef.transform.LookAt(shootToPoint);
            Bullet firedScript = bulletRef.GetComponent<Bullet>();

            firedScript.SetDamage(GetBulletDamageAdd(), GetBulletDamageMult());
            firedScript.SetSpeed(GetBulletSpeedAdd(), GetBulletSpeedMult());
            firedScript.SetTeam(team);

            cldtimer = shootCooldown;

            return firedScript;
        }

        return null;
    }

    public virtual void SpawnSpecialProjectile()
    {
        if (cldtimer <= 0 && specialProjectiles != null && specialProjectiles[0] != null) {
            Instantiate(specialProjectiles[0], transform);
            cldtimer = specialProjectiles[0].cooldownInduced;

            for (int i = 1; i < specialProjectiles.Length; ++i) {
                specialProjectiles[i - 1] = specialProjectiles[i];
            }
            specialProjectiles[specialProjectiles.Length - 1] = null;

        }
    }

    public virtual bool TakeDamage(float damage, float setInvincibleTime = 0)
    {
        if (invincibleTime > 0) {
            return false;
        } else {
            invincibleTime = setInvincibleTime;
        }

        if (shield >= damage) {
            shield -= damage;
            return true;
        } else {
            damage -= shield;
            shield = 0;
            health -= damage;
        }

        if (health <= 0) {
            Destroy(gameObject);
        }

        return true;
    }

    public virtual void AddPowerup(PowerupStats stats)
    {
        PowerupStats pus = Instantiate(stats);

        powerups.Add(pus);
        if (pus.health > 0) {
            health += pus.health;
        }
        if (pus.shield > 0) {
            shield += pus.shield;
        }

        if (pus.specialSpawn != null) {
            for (int i = 0; i < specialProjectiles.Length; ++i) {
                if (specialProjectiles[i] == null) {
                    specialProjectiles[i] = pus.specialSpawn;
                    break;
                }
            }
        }
    }
    #endregion
}
