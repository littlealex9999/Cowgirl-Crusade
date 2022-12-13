using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    public float damagePerSecond;
    public Character.TEAMS team;

    Dictionary<Collider, Character> collidingWith = new Dictionary<Collider, Character>();

    private void OnTriggerEnter(Collider other)
    {
        Character chara = other.GetComponent<Character>();
        if (chara != null && !collidingWith.ContainsKey(other)) {
            collidingWith.Add(other, chara);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (collidingWith.ContainsKey(other)) {
            collidingWith.Remove(other);
        }
    }

    private void Update()
    {
        Debug.Log("Something hit");
        foreach (Character chara in collidingWith.Values) {
            if (chara.getTeam != team) {
                chara.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
