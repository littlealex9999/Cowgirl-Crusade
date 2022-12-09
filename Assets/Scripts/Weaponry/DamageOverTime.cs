using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    public float damagePerSecond;
    public Character.TEAMS team;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy") {
            Character chara = other.GetComponent<Character>();
            if (chara.getTeam != team) {
                chara.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
