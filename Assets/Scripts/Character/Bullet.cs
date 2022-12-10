using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float baseDamage = 5;
    [SerializeField] private float baseSpeed = 5;

    [SerializeField] GameObject impact;
    
    float dmg;
    float spd;

    Character.TEAMS team;

    GameObject homingTarget;
    float homingRotationSpeed = 5;

    private void Start()
    {
        //Debug.Log("Created Bullet");
    }

    void Update()
    {
        if (homingTarget != null) {
            transform.rotation = 
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(homingTarget.transform.position - transform.position), homingRotationSpeed * Time.deltaTime);
        }

        transform.position += transform.forward * spd * Time.deltaTime;
    }

    #region set value methods
    public void SetDamage(float additional = 0, float multiplier = 1)
    {
        dmg = baseDamage * multiplier + additional;
    }

    public void SetSpeed(float additional = 0, float multiplier = 1)
    {
        spd = baseSpeed * multiplier + additional;
    }

    public void SetTeam(Character.TEAMS t)
    {
        team = t;
    }

    public void SetTarget(GameObject target)
    {
        homingTarget = target;
    }

    public void SetHomingSpeed(float speed)
    {
        homingRotationSpeed = speed;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.gameObject.GetComponent<Character>();

        if (character != null) {
            if(character.getTeam != team)
            {
                if (character.TakeDamage(dmg))
                {
                    if (character.getTeam == Character.TEAMS.PLAYER)
                    {
                        // I have moved the ScreenShake() call to the TakeDamage() override in the Player script

                    }
                    else
                    {
                        GameManager.instance.HitEnemy();
                    }

                    DestroyBullet();
                }
            }

        } else {
            ShootableProp prop = other.gameObject.GetComponent<ShootableProp>();

            if(prop != null)
            {
                prop.ShotByBullet(transform.position, transform.rotation);
                DestroyBullet();
            }


        }

    }

    void DestroyBullet()
    {
        if (impact != null)
        {
            Object.Instantiate(impact, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }


}
