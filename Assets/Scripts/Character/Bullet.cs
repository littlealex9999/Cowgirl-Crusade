using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float baseDamage = 5;
    [SerializeField] private float baseSpeed = 5;

    [SerializeField] GameObject impact;
    [SerializeField] bool collideWithEnvironment = false;
    bool fakeHit;
    Character hitscanChara;

    float dmg;
    float spd;

    Character.TEAMS team;

    GameObject homingTarget;

    private void Start()
    {
        //Debug.Log("Created Bullet");
    }

    void Update()
    {
        if (homingTarget != null) {
            transform.LookAt(homingTarget.transform.position);
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

    //public void SetHomingSpeed(float speed)
    //{
    //    homingRotationSpeed = speed;
    //}
    #endregion

    #region getters
    public float getDamage { get { return dmg; } }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (fakeHit)
            return;

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

                    DestroyBullet(true);
                }
            }

        }

        if (collideWithEnvironment) {
            ShootableProp prop = other.gameObject.GetComponent<ShootableProp>();

            if (prop != null) {
                prop.ShotByBullet(transform.position, transform.rotation);
                DestroyBullet(false);
            }

        }

    }

    void DestroyBullet(bool playEffect)
    {
        if (impact != null && playEffect)
        {
            Instantiate(impact, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    void PlayerHitscan()
    {
        GameManager.instance.HitEnemy();
        hitscanChara.TakeDamage(dmg);
        DestroyBullet(true);
    }

    public void DoFakeHit(Character chara)
    {
        fakeHit = true;
        hitscanChara = chara;
        Invoke("PlayerHitscan", (homingTarget.transform.position - transform.position).magnitude / spd);
    }
}