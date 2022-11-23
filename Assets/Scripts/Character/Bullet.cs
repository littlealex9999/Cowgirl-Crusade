using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float baseDamage = 5;
    [SerializeField] private float baseSpeed = 5;

    [SerializeField] private int team;

    float dmg;
    float spd;

    void Update()
    {
        transform.position += transform.forward * spd * Time.deltaTime;
    }

    public void SetDamage(float additional = 0, float multiplier = 1)
    {
        dmg = baseDamage * multiplier + additional;
    }

    public void SetSpeed(float additional = 0, float multiplier = 1)
    {
        spd = baseSpeed * multiplier + additional;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.gameObject.GetComponent<Character>();

        if (character != null)
        {
            if(team != 1)
            character.TakeDamage(dmg);

            Destroy(gameObject);
        }
    }




}
