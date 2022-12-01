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

    [SerializeField] float turnMult = 100;
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
        if (posLastFrame != transform.localPosition) {
            Vector3 direction = (transform.localPosition - posLastFrame).normalized;

            if (direction.x != 0) {
                transform.Rotate(transform.parent.forward, turnMult * Time.deltaTime * -direction.x, Space.World);
            }
            if (direction.y != 0) {
                transform.Rotate(transform.parent.right, turnMult * Time.deltaTime * -direction.y, Space.World);
            }

            posLastFrame = transform.localPosition;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.parent.rotation, resetRotationStrength * Time.deltaTime);

        //if (posLastFrame != transform.position) {
        //    Vector3 direction = (transform.position - posLastFrame).normalized;
        //    Vector3 crossdir = Vector3.Cross(direction, transform.up);
        //    Debug.DrawLine(transform.position, transform.position + crossdir * 5, Color.blue, 10);
        //    Debug.Log(Vector3.Dot(crossdir, transform.forward));

        //    if (direction.x != 0) {

        //    }
        //    if (direction.y != 0) {

        //    }

        //    posLastFrame = transform.position;
        //}

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
    public virtual Bullet Shoot(Vector3 shootToPoint)
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

            return firedScript;
        }

        return null;
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
