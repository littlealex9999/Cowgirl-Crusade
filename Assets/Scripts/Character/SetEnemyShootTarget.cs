using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyShootTarget : MonoBehaviour
{
    [SerializeField] Character target;

    private void OnTriggerEnter(Collider other)
    {
        Character enemy = other.GetComponent<Character>();
        if (enemy != null) {
            enemy.SetTarget(target);
        }
    }
}
