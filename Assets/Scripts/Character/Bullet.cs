using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float baseDamage = 5;
    [SerializeField] private float baseSpeed = 5;
    [SerializeField] private float homingRotationSpeed = 5;

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

        if (character != null && character.getTeam != team) {
            if (character.TakeDamage(dmg)) {
                Destroy(gameObject);
            }
        }
    }




}
